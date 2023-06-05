using domain.Models;
using EasyNetQ;

namespace worker;

public class WorkerStartQueueListener : BackgroundService
{
    private readonly ILogger<WorkerStartQueueListener> Logger;
    private readonly QueuePublisher Publisher;
    private readonly IBus Bus;
    public WorkerStartQueueListener(ILogger<WorkerStartQueueListener> logger, IBus bus, QueuePublisher publisher)
    {
        Logger = logger;
        Bus = bus;
        Publisher = publisher;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        Bus.Dispose();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Bus.SendReceive.ReceiveAsync<WorkerEvent>("worker.event", HandleMessage);
    }

    private async Task HandleMessage(WorkerEvent e)
    {
        await Publisher.Publish(new WorkerState { WorkerId = e.WorkerId, State = "start" });

        await DoWork();

        await Publisher.Publish(new WorkerState { WorkerId = e.WorkerId, State = "finish" });
    }

    private async Task DoWork()
    {
        // Simulate a potential long running task
        await Task.Delay(TimeSpan.FromSeconds(new Random().Next(1, 5)));
    }
}
