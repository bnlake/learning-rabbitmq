using api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[EnableCors]
public class AssignmentController : Controller
{
    private readonly ILogger<AssignmentController> Logger;
    private readonly AssignmentService assignmentService;
    private readonly ContentService contentService;

    public AssignmentController(ILogger<AssignmentController> logger, AssignmentService assignmentService, ContentService contentService)
    {
        Logger = logger;
        this.assignmentService = assignmentService;
        this.contentService = contentService;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> Assign(IFormCollection keyValuePairs)
    {
        if (!keyValuePairs.TryGetValue("patientId", out var rawPatientId)) return BadRequest("Missing Patient ID");
        if (!keyValuePairs.TryGetValue("contentId", out var rawContentId)) return BadRequest("Missing Content ID");

        Guid.TryParse(rawPatientId, out var patientId);
        Guid.TryParse(rawContentId, out var contentId);

        Logger.LogInformation($"Attempting to assign {contentId} to {patientId}");
        if (contentService.Exists(contentId))
        {
            await assignmentService.AssignContent(patientId, contentService.Get(contentId));
            return Ok();
        }
        else
        {
            return BadRequest($"Content not found");
        }
    }
}