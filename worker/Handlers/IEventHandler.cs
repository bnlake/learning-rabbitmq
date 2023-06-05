using domain.Models;

namespace worker.Handlers;

public interface IEventHandler
{
    public Task Execute(WorkerEvent e);
}