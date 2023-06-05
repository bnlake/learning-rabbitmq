using worker.Services;
using domain.Models;

namespace worker.Handlers;

public class StartEventHandler : IEventHandler
{
    private readonly QueuePublisher Publisher;

    public StartEventHandler(QueuePublisher publisher)
    {
        Publisher = publisher;
    }

    public async Task Execute(WorkerEvent e)
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