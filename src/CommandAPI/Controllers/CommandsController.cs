using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandAPIRepo repository;

    public CommandsController(ICommandAPIRepo repo) => repository = repo;

    [HttpGet]
    public ActionResult<IEnumerable<Command>> GetAllCommands() =>
       Ok(repository.GetAllCommands());

    [HttpGet("{id}")]
    public ActionResult<Command> GetCommandById(int id)
    {
        var commandItem = repository.GetCommandById(id);

        return commandItem == null
            ? NotFound()
            : Ok(commandItem);
    }
}