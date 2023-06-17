namespace Models;

public class Report
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Event { get; set; } = string.Empty;
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}