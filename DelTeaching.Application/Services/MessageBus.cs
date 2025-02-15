using System.Text;
using System.Text.Json;
using DelTeaching.Application.Config;
using DelTeaching.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DelTeaching.Application.Services;

public class MessageBus : IMessageBus
{
    private readonly MessageBusConfig _config;

    public MessageBus(MessageBusConfig config)
    {
        _config = config;
    }

    private ConnectionFactory CreateFactory()
    {
        return new()
        {
            HostName = _config.Host,
            Port = _config.Port,
            UserName = _config.UserName,
            Password = _config.Password
        };
    }

    private IConnection CreateConnection()
    {
        return CreateFactory().CreateConnection();
    }

    private static IModel CreateChannel(IConnection connection)
    {
        return connection.CreateModel();
    }

    private void DeclareQueue(IModel channel, string queue)
    {
        channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    public void Consume<T>(string queue, Action<T> onMessage)
    {
        var connection = CreateConnection();
        var channel = CreateChannel(connection);

        DeclareQueue(channel, queue);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));
            onMessage?.Invoke(message);
            await Task.Yield();
        };

        channel.BasicConsume(queue, autoAck: true, consumer: consumer);
    }

    public async Task Publish<T>(string queue, T message)
    {
        using var connection = CreateConnection();
        using var channel = CreateChannel(connection);

        channel.ExchangeDeclare(_config.Exchange, ExchangeType.Direct, durable: true);
        DeclareQueue(channel, queue);
        channel.QueueBind(queue, _config.Exchange, queue);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(
            exchange: _config.Exchange,
            routingKey: queue,
            basicProperties: null,
            body: body);

        await Task.CompletedTask;
    }
}

