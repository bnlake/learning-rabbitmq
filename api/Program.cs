using api.Services;
using EasyNetQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AssignmentService>();
builder.Services.AddSingleton<ContentService>();
builder.Services.AddSingleton<IBus>(_ => RabbitHutch.CreateBus($"host={Environment.GetEnvironmentVariable("RABBITMQ_URL")}"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(builder => builder.AllowAnyHeader().WithOrigins("http://localhost*").AllowCredentials().AllowAnyMethod().SetIsOriginAllowed(x => true));

app.MapControllers();

app.Run();
