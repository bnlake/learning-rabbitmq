using worker.Services;
using domain.Models;

namespace worker.Handlers;

public class StartEventHandler : IEventHandler
{
    private readonly ILogger<StartEventHandler> Logger;
    private readonly QueuePublisher Publisher;
    private readonly WorkerService Service;

    public StartEventHandler(QueuePublisher publisher, WorkerService service, ILogger<StartEventHandler> logger)
    {
        Publisher = publisher;
        Service = service;
        Logger = logger;
    }

    public async Task Execute(WorkerEvent e)
    {
        try
        {
            await Service.StartWorker(e.WorkerId, DoWork);
        }
        catch (OperationCanceledException)
        {
            Logger.LogInformation($"Worker {e.WorkerId} was canceled while running");
        }
    }

    private async Task DoWork(Guid workerId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Publisher.Publish(new WorkerState { WorkerId = workerId, State = "start" });
        // Simulate a potential long running task
        await Task.Delay(TimeSpan.FromSeconds(new Random().Next(1, 5)), cancellationToken);
        Logger.LogInformation($"Was cancellation requested: {cancellationToken.IsCancellationRequested}");
        await Publisher.Publish(new WorkerState { WorkerId = workerId, State = "finish" });
    }
}