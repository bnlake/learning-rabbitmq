using System.Text.Json;
using Models;

namespace Services;

public class NotificationService
{
    private readonly ILogger<NotificationService> logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        this.logger = logger;
    }

    public async Task SendNotification(Notification notification)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        logger.LogInformation($"Notification sent {JsonSerializer.Serialize(notification)}");
    }
}