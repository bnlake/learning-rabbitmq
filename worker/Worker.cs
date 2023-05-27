using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace worker;

public class QueueListener : BackgroundService
{
    private readonly ILogger<QueueListener> Logger;
    private IConnectionFactory Factory;
    private IConnection Connection;
    private IModel Channel;
    private const string QueueName = "worker.start";

    public QueueListener(ILogger<QueueListener> logger, IConnectionFactory factory)
    {
        Logger = logger;
        Factory = factory;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        this.Connection = Factory.CreateConnection();
        Logger.LogInformation($"Created connection to RabbitMQ");

        this.Channel = Connection.CreateModel();
        Logger.LogInformation($"Created channel to RabbitMQ");

        Channel.QueueDeclarePassive(QueueName);
        Logger.LogInformation($"Declared passive queue {QueueName}");

        return base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);

        Channel.Close();
        Logger.LogInformation($"Closed channel to RabbitMQ");

        Connection.Close();
        Logger.LogInformation($"Closed connection to RabbitMQ");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation($"Executing queue listener");
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(Channel);

        consumer.Received += HandleMessage;
        Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

        await Task.CompletedTask;
    }

    private async Task HandleMessage(object Model, BasicDeliverEventArgs Args)
    {
        var body = Args.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [x] Received {message}");
    }
}
