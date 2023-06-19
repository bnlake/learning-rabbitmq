using System.Text.Json;
using Models;

namespace Services;

public class NotificationService
{
    private readonly ILogger<NotificationService> logger;
    private readonly Random random = new Random();

    public NotificationService(ILogger<NotificationService> logger)
    {
        this.logger = logger;
    }

    public async Task SendNotification(Notification notification)
    {
        await Task.Delay(TimeSpan.FromSeconds(random.NextInt64(1,4)));
        logger.LogInformation($"Notification sent {JsonSerializer.Serialize(notification)}");
    }
}
