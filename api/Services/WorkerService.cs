using RabbitMQ.Client;
using api.Models;
using System.Text;
using System.Text.Json;

namespace api.Services;

public class WorkerService
{
    private readonly ILogger<WorkerService> Logger;
    private readonly IDictionary<Guid, Worker> workers = new Dictionary<Guid, Worker>();
    private IConnectionFactory Factory;
    private IConnection Connection;
    private IModel Channel;
    private const string QueueName = "worker.start";

    public WorkerService(ILogger<WorkerService> logger, IConnectionFactory factory)
    {
        this.Logger = logger;
        this.Factory = factory;
        this.RegisterSeedWorkers();
        Logger.LogDebug($"Worker Services dictionary instantiated with {workers.Count} workers");
        this.InitializeClient();
    }

    private void InitializeClient()
    {
        this.Connection = Factory.CreateConnection();
        this.Channel = Connection.CreateModel();

        Channel.QueueDeclare(queue: QueueName,
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);
    }

    public Task CreateAsync(Worker worker)
    {
        workers.Add(worker.Id, worker);
        return Task.CompletedTask;
    }

    public Task<IList<Worker>> GetAllAsync()
    {
        return Task.FromResult(workers.Values.ToList() as IList<Worker>);
    }

    public bool Exists(Guid id)
    {
        return workers.ContainsKey(id);
    }

    public Task StartWorker(Guid id)
    {
        var x = new { WorkerID = id };
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(x));

        Channel.BasicPublish(exchange: String.Empty,
        routingKey: QueueName,
        basicProperties: null,
        body: body);

        return Task.CompletedTask;
    }

    private void RegisterSeedWorkers()
    {
        Guid guid = Guid.NewGuid();
        this.workers.Add(guid, new WorkerBuilder().AddId(guid).AddName("Headache").Build());
        guid = Guid.NewGuid();
        this.workers.Add(guid, new WorkerBuilder().AddId(guid).AddName("Heart-Failure").Build());
        guid = Guid.NewGuid();
        this.workers.Add(guid, new WorkerBuilder().AddId(guid).AddName("Stress").Build());
    }
}