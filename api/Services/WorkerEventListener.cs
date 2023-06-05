using System.Text;
using System.Text.Json;
using api.Hubs;
using api.Models;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace api.Services;

public class WorkerEventListener : BackgroundService
{
    private readonly ILogger<WorkerEventListener> Logger;
    private readonly WorkerService Service;
    private readonly IHubContext<WorkerHub> HubContext;
    private IConnectionFactory Factory;
    private IConnection Connection;
    private IModel Channel;
    private const string QueueName = "worker.events";

    public WorkerEventListener(ILogger<WorkerEventListener> logger, IConnectionFactory factory, WorkerService service, IHubContext<WorkerHub> hubContext)
    {
        this.Logger = logger;
        this.Factory = factory;
        this.Service = service;
        this.HubContext = hubContext;
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

        this.Channel.Close();
        this.Connection.Close();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(Channel);

        consumer.Received += HandleMessage;
        Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

        await Task.CompletedTask;
    }

    private Task HandleMessage(object Model, BasicDeliverEventArgs Args)
    {
        var body = Args.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        WorkerEvent workerEvent = JsonSerializer.Deserialize<WorkerEvent>(message);

        Logger.LogInformation($"Handled event {workerEvent.Event} for worker {workerEvent.WorkerID}");
        return HubContext.Clients.Group(workerEvent.WorkerID.ToString()).SendAsync("ReceiveWorkerState", workerEvent.Event);
    }
}