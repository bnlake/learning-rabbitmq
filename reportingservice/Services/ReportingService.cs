using System.Text.Json;
using Models;

namespace Services;

public class ReportingService
{
    private readonly ILogger<ReportingService> logger;
    private readonly Random random = new Random();

    public ReportingService(ILogger<ReportingService> logger)
    {
        this.logger = logger;
    }

    public async Task AddReport(Report report)
    {
        await Task.Delay(TimeSpan.FromSeconds(random.NextInt64(1,4)));
        logger.LogInformation($"Report added {JsonSerializer.Serialize(report)}");
    }
}
