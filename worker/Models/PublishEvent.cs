namespace worker.Models;

public struct PublishEvent
{
    public Guid WorkerID { get; set; }
    public string Event { get; set; }
}