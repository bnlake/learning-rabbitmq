namespace worker.Models;

public struct PublishState
{
    public Guid WorkerId { get; set; }
    public string State { get; set; }
}