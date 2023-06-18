using Domain.Models;

namespace Domain.Events;

public class ContentAssignedEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;
    public string PatientId { get; set; }
    public Content? Payload { get; set; }
}