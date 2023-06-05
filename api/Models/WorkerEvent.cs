namespace api.Models;

public abstract class WorkerEvent
{
    public Guid WorkerID { get; set; }
    public abstract string Event { get; }
}