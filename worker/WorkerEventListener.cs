using domain.Models;
using EasyNetQ;
using worker.Handlers;

namespace worker;

public class WorkerEventListener : BackgroundService
{
    private readonly ILogger<WorkerEventListener> Logger;
    private readonly QueuePublisher Publisher;
    private readonly IBus Bus;
    private readonly EventHandlerFactory HandlerFactory;
    public WorkerEventListener(ILogger<WorkerEventListener> logger, IBus bus, QueuePublisher publisher, EventHandlerFactory factory)
    {
        Logger = logger;
        Bus = bus;
        Publisher = publisher;
        HandlerFactory = factory;
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
        var handler = HandlerFactory.CreateHandler(e.Event);
        await handler.Execute(e);
    }

}
