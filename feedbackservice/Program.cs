using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EasyNetQ;
using Listeners;
using Services;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog((ctx, services, configuration) => configuration.ReadFrom.Configuration(ctx.Configuration))
    .ConfigureServices(services =>
    {
        services.AddSingleton<FeedbackService>();
        services.AddSingleton<IBus>(provider => RabbitHutch.CreateBus($"host={Environment.GetEnvironmentVariable("RABBITMQ_URL")}"));
        services.AddHostedService<ContentAssignmentListener>();
    })
    .Build();

host.Run();
