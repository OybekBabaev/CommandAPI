using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    //Random change

    private readonly ICommandAPIRepo repository;
    private readonly IMapper mapper;

    public CommandsController(ICommandAPIRepo repo, IMapper map)
    {
        repository = repo;
        mapper = map;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands() =>
       Ok(mapper.Map<IEnumerable<CommandReadDto>>(repository.GetAllCommands()));

    [HttpGet("{id}", Name = "GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
        var commandItem = repository.GetCommandById(id);

        return commandItem == null
            ? NotFound()
            : Ok(mapper.Map<CommandReadDto>(commandItem));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto inputCommand)
    {
        Command commandModel = mapper.Map<Command>(inputCommand);
        repository.CreateCommand(commandModel);
        repository.SaveChanges();

        var commandReadDto = mapper.Map<CommandReadDto>(commandModel);

        return CreatedAtRoute(nameof(GetCommandById),
            new { Id = commandReadDto.Id }, commandReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto updatedCommand)
    {
        var commandModelFromRepo = repository.GetCommandById(id);

        if (commandModelFromRepo == null) return NotFound();

        mapper.Map(updatedCommand, commandModelFromRepo);
        repository.UpdateCommand(commandModelFromRepo);
        repository.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
    {
        var commandModelFromRepo = repository.GetCommandById(id);

        if (commandModelFromRepo == null) return NotFound();

        var commandToPatch = mapper.Map<CommandUpdateDto>(commandModelFromRepo);
        patchDoc.ApplyTo(commandToPatch, ModelState);

        if (!TryValidateModel(commandToPatch)) return ValidationProblem(ModelState);

        mapper.Map(commandToPatch, commandModelFromRepo);
        repository.UpdateCommand(commandModelFromRepo);
        repository.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
        var commandModelFromRepo = repository.GetCommandById(id);

        if (commandModelFromRepo == null) return NotFound();

        repository.DeleteCommand(commandModelFromRepo);
        repository.SaveChanges();

        return NoContent();
    }
}