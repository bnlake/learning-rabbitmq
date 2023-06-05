using worker.Services;
using domain.Models;

namespace worker.Handlers;

public class StopEventHandler : IEventHandler
{
    private readonly QueuePublisher Publisher;

    public StopEventHandler(QueuePublisher publisher)
    {
        Publisher = publisher;
    }

    public Task Execute(WorkerEvent e)
    {
        throw new NotImplementedException();
    }
}