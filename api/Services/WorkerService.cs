using api.Models;

namespace api.Services;

public class WorkerService
{
    private readonly ILogger<WorkerService> Logger;
    private readonly IDictionary<Guid, Worker> workers = new Dictionary<Guid, Worker>();

    public WorkerService(ILogger<WorkerService> logger)
    {
        this.Logger = logger;
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