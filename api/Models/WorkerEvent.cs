namespace api.Models;

public struct WorkerEvent
{
    public Guid WorkerID { get; set; }
    public string Event { get; set; }
}