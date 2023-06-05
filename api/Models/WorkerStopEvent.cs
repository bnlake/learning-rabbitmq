namespace api.Models;

public class WorkerStopEvent : WorkerEvent
{
    public override string Event => "stop";
}