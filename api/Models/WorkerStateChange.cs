namespace api.Models;

public class WorkerStateChange
{
    public Guid WorkerId { get; set; }
    public string State { get; set; } = String.Empty;
}