using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using worker.Models;

public class QueuePublisher : BackgroundService
{
    private readonly ILogger<QueuePublisher> Logger;
    private IConnectionFactory Factory;
    private IConnection Connection;
    private IModel Channel;
    private const string QueueName = "worker.events";

    public QueuePublisher(ILogger<QueuePublisher> logger, IConnectionFactory factory)
    {
        Logger = logger;
        Factory = factory;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        this.Connection = Factory.CreateConnection();
        this.Channel = Connection.CreateModel();

        Channel.QueueDeclare(queue: QueueName,
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);

        return base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);

        Channel.Close();
        Connection.Close();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.CompletedTask;
    }

    public Task Publish(PublishEvent e)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(e));

        Channel.BasicPublish(exchange: string.Empty,
        routingKey: QueueName,
        basicProperties: null,
        body: body);

        Logger.LogInformation($"Published {e.Event} for {e.WorkerID}");

        return Task.CompletedTask;
    }
}