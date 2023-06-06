using worker.Services;
using domain.Models;

namespace worker.Handlers;

public class StopEventHandler : IEventHandler
{
    private readonly ILogger<StopEventHandler> Logger;
    private readonly WorkerService Service;
    private readonly QueuePublisher Publisher;

    public StopEventHandler(QueuePublisher publisher, WorkerService service, ILogger<StopEventHandler> logger)
    {
        Logger = logger;
        Service = service;
        Publisher = publisher;
    }

    public async Task Execute(WorkerEvent e)
    {
        await Service.StopWorker(e.WorkerId);
        await Publisher.Publish(new WorkerState { WorkerId = e.WorkerId, State = "waiting" });
    }
}