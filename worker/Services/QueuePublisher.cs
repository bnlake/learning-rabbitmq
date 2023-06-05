using domain.Models;
using EasyNetQ;

namespace worker.Services;

public class QueuePublisher : BackgroundService
{
    private readonly ILogger<QueuePublisher> Logger;
    private readonly IBus Bus;

    public QueuePublisher(ILogger<QueuePublisher> logger, IBus bus)
    {
        Logger = logger;
        Bus = bus;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        Bus.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.CompletedTask;
    }

    public Task Publish(WorkerState e)
    {
        Logger.LogInformation($"Published {e.State} for {e.WorkerId}");
        return Bus.SendReceive.SendAsync<WorkerState>("worker.state", e);
    }
}