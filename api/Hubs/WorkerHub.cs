using api.Services;
using Microsoft.AspNetCore.SignalR;

namespace api.Hubs;

public class WorkerHub : Hub
{
    private readonly ILogger<WorkerHub> Logger;
    private readonly WorkerService Service;

    public WorkerHub(ILogger<WorkerHub> logger, WorkerService service)
    {
        this.Logger = logger;
        this.Service = service;
    }

    public async Task JoinGroup(string workerId)
    {
        Logger.LogInformation($"Adding {workerId} to signalr hub group");
        await Groups.AddToGroupAsync(Context.ConnectionId, workerId);
    }

    public async Task LeaveGroup(string workerId)
    {
        Logger.LogInformation($"Removing {workerId} from signalr hub group");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, workerId);
    }

    public Task StartWorker(Guid workerId)
    {
        return this.Service.StartWorker(workerId);
    }

    public Task StopWorker(Guid workerId)
    {
        return this.Service.StartWorker(workerId);
    }
}