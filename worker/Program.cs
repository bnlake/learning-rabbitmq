using EasyNetQ;
using worker.Services;
using worker.Handlers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IBus>(provider => RabbitHutch.CreateBus($"host={Environment.GetEnvironmentVariable("RABBITMQ_URL")}"));
        services.AddSingleton<WorkerService>();
        services.AddSingleton<EventHandlerFactory>();
        services.AddScoped<StartEventHandler>();
        services.AddScoped<StopEventHandler>();
        // https://stackoverflow.com/questions/51254053/how-to-inject-a-reference-to-a-specific-ihostedservice-implementation
        // to allow access between background services
        services.AddSingleton<QueuePublisher>();
        services.AddHostedService<BackgroundServiceStarter<QueuePublisher>>();
        services.AddSingleton<WorkerEventListener>();
        services.AddHostedService<BackgroundServiceStarter<WorkerEventListener>>();
    })
    .Build();

host.Run();
