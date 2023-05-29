using RabbitMQ.Client;
using worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IConnectionFactory>(provider => new ConnectionFactory { HostName = Environment.GetEnvironmentVariable("RABBITMQ_URL"), DispatchConsumersAsync = true });
        // https://stackoverflow.com/questions/51254053/how-to-inject-a-reference-to-a-specific-ihostedservice-implementation
        // to allow access between background services
        services.AddSingleton<QueuePublisher>();
        services.AddHostedService<BackgroundServiceStarter<QueuePublisher>>();
        services.AddSingleton<WorkerStartQueueListener>();
        services.AddHostedService<BackgroundServiceStarter<WorkerStartQueueListener>>();
    })
    .Build();

host.Run();
