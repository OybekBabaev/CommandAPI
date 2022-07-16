using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI.Data;

#pragma warning disable 8618

public class CommandContext : DbContext
{
    public CommandContext(DbContextOptions<CommandContext> options) : base(options) {}

    public DbSet<Command> CommandItems {get;set;}
}