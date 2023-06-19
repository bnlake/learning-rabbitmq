using Domain.Events;
using EasyNetQ;
using Models;
using Services;

namespace Listeners;

public class ContentAssignmentListener : BackgroundService
{
    private readonly ILogger<ContentAssignmentListener> logger;
    private readonly IBus bus;
    private readonly FeedbackService service;

    public ContentAssignmentListener(ILogger<ContentAssignmentListener> logger, IBus bus, FeedbackService service)
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
        EducationAssignment assignment = new() { ContentId = e.Payload.Id, PatientId = e.PatientId, AssignmentTimestamp = e.Timestamp };
        await service.RecordAssignment(assignment);
    }
}