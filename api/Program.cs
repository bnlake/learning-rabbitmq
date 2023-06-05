using api.Hubs;
using api.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<WorkerService>();
builder.Services.AddSingleton<IConnectionFactory>(provider => new ConnectionFactory { HostName = Environment.GetEnvironmentVariable("RABBITMQ_URL"), DispatchConsumersAsync = true });
builder.Services.AddHostedService<WorkerEventListener>();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(builder => builder.AllowAnyHeader().WithOrigins("http://localhost*").AllowCredentials().AllowAnyMethod().SetIsOriginAllowed(x => true));
app.MapHub<WorkerHub>("/workerhub");

app.MapControllers();

app.Run();
