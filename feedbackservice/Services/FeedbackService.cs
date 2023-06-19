using Models;

namespace Services;

public class FeedbackService
{
    private readonly ILogger<FeedbackService> logger;
    private readonly Random random = new Random();

    public FeedbackService(ILogger<FeedbackService> logger)
    {
        this.logger = logger;
    }

    public async Task RecordAssignment(EducationAssignment assignment)
    {
        // Simulate long running task for feedback loop
        await Task.Delay(TimeSpan.FromSeconds(random.NextInt64(1, 4)));
        logger.LogInformation($"Finished feedback loop for the assignment of content {assignment.ContentId}");
    }
}