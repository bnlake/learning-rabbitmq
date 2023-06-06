using System.Collections.Concurrent;

namespace worker.Services;

public class WorkerService
{
    private readonly ILogger<WorkerService> Logger;
    private readonly static ConcurrentDictionary<Guid, Tuple<Task, CancellationTokenSource>> workers = new ConcurrentDictionary<Guid, Tuple<Task, CancellationTokenSource>>();

    public WorkerService(ILogger<WorkerService> logger)
    {
        Logger = logger;
    }

    public Task StartWorker(Guid workerId, Func<Guid, CancellationToken, Task> task)
    {
        var cancellationSource = new CancellationTokenSource();

        var t = Task.Run(async () => await task(workerId, cancellationSource.Token), cancellationSource.Token);
        
        workers.TryAdd(workerId, Tuple.Create(t, cancellationSource));

        return t;
    }

    public Task StopWorker(Guid workerId)
    {
        if (workers.TryGetValue(workerId, out var val))
        {
            var (worker, cancellationSource) = val;
            Logger.LogInformation($"Attempting to stop worker {workerId}");
            cancellationSource.Cancel();
            workers.TryRemove(workerId, out var x);

            return Task.CompletedTask;
        }
        else
        {
            throw new ArgumentNullException(nameof(workerId));
        }
    }
}