using CommandAPI.Models;

namespace CommandAPI.Data;

public class SqlCommandAPIRepo : ICommandAPIRepo
{
    private readonly CommandContext context;

    public SqlCommandAPIRepo(CommandContext ctx) => context = ctx; 

    public void CreateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void DeleteCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands() =>
        context.CommandItems.ToList();

    public Command GetCommandById(int id) =>
        context.CommandItems.FirstOrDefault(z => z.Id == id);

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }
}