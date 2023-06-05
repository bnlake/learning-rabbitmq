using api.Hubs;
using domain.Models;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR;

namespace api.Services;

public class WorkerEventListener : BackgroundService
{
    private readonly ILogger<WorkerEventListener> Logger;
    private readonly WorkerService Service;
    private readonly IHubContext<WorkerHub> HubContext;
    private readonly IBus Bus;

    public WorkerEventListener(ILogger<WorkerEventListener> logger, IBus bus, WorkerService service, IHubContext<WorkerHub> hubContext)
    {
        this.Logger = logger;
        this.Bus = bus;
        this.Service = service;
        this.HubContext = hubContext;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        Bus.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Bus.SendReceive.ReceiveAsync<WorkerState>("worker.state", HandleMessage);
    }

    private Task HandleMessage(WorkerState state)
    {

        Logger.LogInformation($"Handled event {state.State} for worker {state.WorkerId}");
        return HubContext.Clients.Group(state.WorkerId.ToString()).SendAsync("ReceiveWorkerState", state.State);
    }
}