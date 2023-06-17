namespace Models;

public class Notification {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Event { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public DateTimeOffset EventTimestamp { get; set; } = DateTimeOffset.UtcNow;
}