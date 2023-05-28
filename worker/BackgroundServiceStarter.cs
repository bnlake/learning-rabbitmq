/// https://stackoverflow.com/questions/51254053/how-to-inject-a-reference-to-a-specific-ihostedservice-implementation
public class BackgroundServiceStarter<T> : IHostedService where T : IHostedService
{
    public readonly T service;

    public BackgroundServiceStarter(T service)
    {
        this.service = service;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return this.service.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return this.service.StopAsync(cancellationToken);
    }
}