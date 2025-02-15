using Microsoft.AspNetCore.Mvc;

namespace DelTeaching.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok(new
        {
            Message = "Welcome to API",
            AccessDate = DateTime.Now,
        });
    }
}