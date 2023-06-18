using api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;

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
    public async Task<IActionResult> Assign([FromBody] Assignment assignment)
    {
        var contentId = assignment.ContentId;
        var patientId = assignment.PatientId;

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