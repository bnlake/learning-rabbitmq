namespace domain.Models;

public class WorkerState
{
    public Guid WorkerId { get; set; }
    public string State { get; set; } = String.Empty;
}