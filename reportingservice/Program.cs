using EasyNetQ;
using Listeners;
using Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ReportingService>();
        services.AddSingleton<IBus>(provider => RabbitHutch.CreateBus($"host={Environment.GetEnvironmentVariable("RABBITMQ_URL")}"));
        services.AddHostedService<ContentAssignmentListener>();
    })
    .Build();

host.Run();
