namespace worker.Models;

public struct WorkerEvent
{
    public const string Start = "start";
    public const string Stop = "stop";
    public const string Finish = "finish";
    public const string Error = "error";
}