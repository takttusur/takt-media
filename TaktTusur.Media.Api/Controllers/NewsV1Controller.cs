using Microsoft.AspNetCore.Mvc;

namespace TaktTusur.Media.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class NewsController: ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("test");
    }
}