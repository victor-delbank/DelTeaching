namespace DelTeaching.Application.Config;

public class MessageBusConfig
{
    public string Host { get; set; } = Environment.GetEnvironmentVariable("RABBIT_HOST");
    public int Port { get; set; } = int.TryParse(Environment.GetEnvironmentVariable("RABBIT_PORT"), out int port) ? port : 5672;
    public string UserName { get; set; } = Environment.GetEnvironmentVariable("RABBIT_USERNAME");
    public string Password { get; set; } = Environment.GetEnvironmentVariable("RABBIT_PASSWORD");
    public string Exchange { get; set; } = Environment.GetEnvironmentVariable("RABBIT_EXCHANGE");
}