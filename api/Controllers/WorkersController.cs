using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("[controller]")]
public class WorkersController
{
    private readonly ILogger<WorkersController> Logger;
    private readonly WorkerService service;

    public WorkersController(ILogger<WorkersController> logger, WorkerService service)
    {
        this.Logger = logger;
        this.service = service;
    }

    [HttpGet]
    public async Task<IList<Worker>> GetAll()
    {
        return await service.GetAllAsync();
    }
}