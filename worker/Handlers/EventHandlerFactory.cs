namespace worker.Handlers;

public class EventHandlerFactory
{
    private readonly IServiceProvider Provider;

    public EventHandlerFactory(IServiceProvider provider)
    {
        Provider = provider;
    }

    public IEventHandler CreateHandler(string e)
    {
        return e switch
        {
            "start" => Provider.GetRequiredService<StartEventHandler>(),
            "stop" => Provider.GetRequiredService<StopEventHandler>(),
            _ => throw new ArgumentNullException(e)
        };
    }
}