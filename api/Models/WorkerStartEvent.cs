namespace api.Models;

public class WorkerStartEvent : WorkerEvent
{
    public override string Event => "start";
}