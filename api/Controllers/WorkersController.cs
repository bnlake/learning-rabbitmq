using api.Models;
using api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[EnableCors]
public class WorkersController : Controller
{
    private readonly ILogger<WorkersController> Logger;
    private readonly WorkerService service;

    public WorkersController(ILogger<WorkersController> logger, WorkerService service)
    {
        this.Logger = logger;
        this.service = service;
    }

    [HttpGet("workers/")]
    public async Task<IList<Worker>> GetAll()
    {
        return await service.GetAllAsync();
    }

    [HttpGet("workers/{id}/start")]
    public IActionResult StartWorker(Guid id)
    {
        if (service.Exists(id))
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("workers/{id}/stop")]
    public IActionResult StopWorker(Guid id)
    {
        if (service.Exists(id))
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}