using Domain.Models;
using Domain.Events;
using EasyNetQ;

namespace api.Services;

public class AssignmentService
{
    private readonly ILogger<AssignmentService> Logger;
    private readonly IBus Bus;

    public AssignmentService(ILogger<AssignmentService> logger, IBus bus)
    {
        Logger = logger;
        Bus = bus;
    }

    public Task AssignContent(Guid patientId, Content content)
    {
        Logger.LogInformation($"Publishing that {content.Id} was assigned to {patientId}");
        var e = new ContentAssignedEvent { PatientId = patientId, Payload = content };
        return Bus.PubSub.PublishAsync(e);
    }
}