using System.Text.Json;
using Models;

namespace Services;

public class ReportingService
{
    private readonly ILogger<ReportingService> logger;

    public ReportingService(ILogger<ReportingService> logger)
    {
        this.logger = logger;
    }

    public async Task AddReport(Report report)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        logger.LogInformation($"Report added {JsonSerializer.Serialize(report)}");
    }
}