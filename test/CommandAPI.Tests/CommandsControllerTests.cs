using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using CommandAPI.Models;
using CommandAPI.Data;
using CommandAPI.Profiles;
using CommandAPI.Controllers;
using CommandAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Tests;

#pragma warning disable 8603, 8625

public class CommandsControllerTests : IDisposable
{
    Mock<ICommandAPIRepo> mockRepo;
    CommandsProfile realProfile;
    MapperConfiguration configs;
    IMapper mapper;

    public CommandsControllerTests()
    {
        mockRepo = new();
        realProfile = new();
        configs = new(cfg => cfg.AddProfile(realProfile));
        mapper = new Mapper(configs);
    }

    public void Dispose()
    {
        mockRepo = null;
        mapper = null;
        configs = null;
        realProfile = null;
    }

    [Fact]
    public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
    {
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.GetAllCommands();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
    {
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.GetAllCommands();
        var okResult = result.Result as OkObjectResult;
        var commands = okResult!.Value as List<CommandReadDto>;
        
        Assert.Single(commands);
    }

    [Fact]
    public void GetAllCommands_Returns200OK_WhenDBHasOneResource()
    {
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.GetAllCommands();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
    {
        mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.GetAllCommands();

        Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
    }

    [Fact]
    public void GetCommandById_Returns404NotFound_WhenNonExistentIDProvided()
    {
        mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.GetCommandById(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void GetCommandById_Returns200OK_WhenValidIDProvided()
    {
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command {
            Id = 1,
            HowTo = "Mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });

        CommandsController contra = new(mockRepo.Object, mapper);
        var result = contra.GetCommandById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetCommandById_Returns200OK__WhenValidIDProvided()
    {
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command {
            Id = 1,
            HowTo = "Mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.GetCommandById(1);

        Assert.IsType<ActionResult<CommandReadDto>>(result);
    }

    [Fact]
    public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
    {
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command {
            Id = 1,
            HowTo = "Mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.CreateCommand(new());

        Assert.IsType<ActionResult<CommandReadDto>>(result);
    }

    [Fact]
    public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
    {
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command {
            Id = 1,
            HowTo = "Mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.CreateCommand(new());

        Assert.IsType<CreatedAtRouteResult>(result.Result);
    }

    [Fact]
    public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
    {
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command {
            Id = 1,
            HowTo = "Mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });

        CommandsController contra = new(mockRepo.Object, mapper);
        
        var result = contra.UpdateCommand(1, new());

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
    {
        mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.UpdateCommand(82, new());

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void PartialUpdateCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
    {
        mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.PartialUpdateCommand(82, new());

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteCommand_Returns204NoContent_WhenValidResourceIDSubmitted()
    {
        mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command {
            Id = 1,
            HowTo = "Mock",
            Platform = "Mock",
            CommandLine = "Mock"
        });

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.DeleteCommand(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
    {
        mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);

        CommandsController contra = new(mockRepo.Object, mapper);

        var result = contra.DeleteCommand(0);

        Assert.IsType<OkResult>(result);
    }

    private List<Command> GetCommands(int num)
    {
        List<Command> commands = new();

        if (num > 0)
            commands.Add(new() {
                Id = 0,
                HowTo = "How to generate a migration",
                Platform = ".NET Core EF",
                CommandLine = "dotnet ef migrations add <Name of migration>"
            });

        return commands;
    }
}