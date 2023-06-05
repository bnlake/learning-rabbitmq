using api.Models;
using domain.Models;
using EasyNetQ;

namespace api.Services;

public class WorkerService
{
    private readonly ILogger<WorkerService> Logger;
    private readonly IDictionary<Guid, Worker> workers = new Dictionary<Guid, Worker>();
    private IBus Bus;

    public WorkerService(ILogger<WorkerService> logger, IBus bus)
    {
        this.Logger = logger;
        this.Bus = bus;
        this.RegisterSeedWorkers();
        Logger.LogDebug($"Worker Services dictionary instantiated with {workers.Count} workers");
    }

    public Task CreateAsync(Worker worker)
    {
        workers.Add(worker.Id, worker);
        return Task.CompletedTask;
    }

    public Task<IList<Worker>> GetAllAsync()
    {
        return Task.FromResult(workers.Values.ToList() as IList<Worker>);
    }

    public bool Exists(Guid id)
    {
        return workers.ContainsKey(id);
    }

    public Task StartWorker(Guid id)
    {
        var e = new WorkerEvent { WorkerId = id, Event = "start" };
        return Bus.SendReceive.SendAsync<WorkerEvent>("worker.event", e);
    }

    public Task StopWorker(Guid id)
    {
        var e = new WorkerEvent { WorkerId = id, Event = "stop" };
        return Bus.SendReceive.SendAsync<WorkerEvent>("worker.event", e);
    }

    private void RegisterSeedWorkers()
    {
        Guid guid = Guid.NewGuid();
        this.workers.Add(guid, new WorkerBuilder().AddId(guid).AddName("Headache").Build());
        guid = Guid.NewGuid();
        this.workers.Add(guid, new WorkerBuilder().AddId(guid).AddName("Heart-Failure").Build());
        guid = Guid.NewGuid();
        this.workers.Add(guid, new WorkerBuilder().AddId(guid).AddName("Stress").Build());
    }
}