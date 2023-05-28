namespace worker.Models;

public struct PublishEvent
{
    public Guid WorkerID { get; set; }
    public WorkerEvent Event { get; set; }
}