namespace DelTeaching.Application.Interfaces;

public interface IMessageBus
{
    Task Publish<T>(string queue, T message);
    void Consume<T>(string queue, Action<T> onMessage);
}