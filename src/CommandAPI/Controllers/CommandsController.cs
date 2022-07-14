using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get() =>
        new string[] { "Dieser", "Text", "ist", "vorgecodet" };
}