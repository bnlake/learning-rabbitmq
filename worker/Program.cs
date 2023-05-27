using RabbitMQ.Client;
using worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IConnectionFactory>(provider => new ConnectionFactory { HostName = "localhost", DispatchConsumersAsync = true });
        services.AddHostedService<QueueListener>();
    })
    .Build();

host.Run();
