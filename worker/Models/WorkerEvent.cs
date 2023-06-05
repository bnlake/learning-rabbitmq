namespace worker.Models;

public class WorkerEvent
{
    public Guid WorkerId { get; set; }
    public string Event { get; set; } = String.Empty;
}