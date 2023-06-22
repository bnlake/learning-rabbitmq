using EasyNetQ;
using Listeners;
using Services;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices(services =>
    {
        services.AddSingleton<NotificationService>();
        services.AddSingleton<IBus>(provider => RabbitHutch.CreateBus($"host={Environment.GetEnvironmentVariable("RABBITMQ_URL")}"));
        services.AddHostedService<ContentAssignmentListener>();
    })
    .Build();

host.Run();
