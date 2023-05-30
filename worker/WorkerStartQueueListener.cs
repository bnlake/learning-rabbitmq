using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using worker.Models;

namespace worker;

public class WorkerStartQueueListener : BackgroundService
{
    private readonly ILogger<WorkerStartQueueListener> Logger;
    private readonly QueuePublisher Publisher;
    private IConnectionFactory Factory;
    private IConnection Connection;
    private IModel Channel;
    private const string QueueName = "worker.start";

    public WorkerStartQueueListener(ILogger<WorkerStartQueueListener> logger, IConnectionFactory factory, QueuePublisher publisher)
    {
        Logger = logger;
        Publisher = publisher;
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
        WorkerStartEvent startEvent = JsonSerializer.Deserialize<WorkerStartEvent>(message);

        await Publisher.Publish(new PublishEvent { WorkerID = startEvent.WorkerID, Event = WorkerEvent.Start });

        await DoWork();

        await Publisher.Publish(new PublishEvent { WorkerID = startEvent.WorkerID, Event = WorkerEvent.Finish });
    }

    private async Task DoWork()
    {
        // Simulate a potential long running task
        await Task.Delay(TimeSpan.FromSeconds(new Random().Next(1, 5)));
    }
}
