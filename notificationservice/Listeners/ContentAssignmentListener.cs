using Domain.Events;
using EasyNetQ;
using Models;
using Services;

namespace Listeners;

public class ContentAssignmentListener : BackgroundService
{
    private readonly ILogger<ContentAssignmentListener> logger;
    private readonly IBus bus;
    private readonly NotificationService service;

    public ContentAssignmentListener(ILogger<ContentAssignmentListener> logger, IBus bus, NotificationService service)
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
        return bus.PubSub.SubscribeAsync<ContentAssignedEvent>(Guid.NewGuid().ToString(), HandleMessage, stoppingToken);
    }

    private async Task HandleMessage(ContentAssignedEvent e)
    {
        Notification notification = new() { Recipient = e.PatientId.ToString(), Event = "Content assigned", EventTimestamp = e.Timestamp };
        await service.SendNotification(notification);
    }
}