using api.Services;
using Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[EnableCors]
public class ContentController : Controller
{
    private readonly ContentService service;

    public ContentController(ContentService service)
    {
        this.service = service;
    }

    [HttpGet("content")]
    public ActionResult<ICollection<Content>> GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("content/{id}")]
    public ActionResult<Content> Get(Guid id)
    {
        if (service.Exists(id))
        {
            return Ok(service.Get(id));
        }
        else
        {
            return BadRequest("Content not found");
        }
    }
}