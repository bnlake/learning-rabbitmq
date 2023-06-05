using domain.Models;

namespace api.Models;

public class WorkerBuilder
{
    private Worker worker;

    public WorkerBuilder()
    {
        this.worker = new Worker { };
    }
    public Worker Build() { return worker; }

    public WorkerBuilder AddId(Guid id)
    {
        worker.Id = id;
        return this;
    }

    public WorkerBuilder AddName(string name)
    {
        worker.Name = name;
        return this;
    }
}