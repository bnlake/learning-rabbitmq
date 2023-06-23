using Domain.Events;
using EasyNetQ;
using Models;
using Services;
using System.Reflection;

namespace Listeners;

public class ContentAssignmentListener : BackgroundService
{
    private readonly ILogger<ContentAssignmentListener> logger;
    private readonly IBus bus;
    private readonly ReportingService service;

    public ContentAssignmentListener(ILogger<ContentAssignmentListener> logger, IBus bus, ReportingService service)
    {
        this.logger = logger;
        this.bus = bus;
        this.service = service;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        bus.Dispose();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return bus.PubSub.SubscribeAsync<ContentAssignedEvent>(Assembly.GetExecutingAssembly().FullName, HandleMessage, stoppingToken);
    }

    private async Task HandleMessage(ContentAssignedEvent e)
    {
        Report report = new() { Event = "Content Assigned To Patient" };
        await service.AddReport(report);
    }
}