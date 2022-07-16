using CommandAPI.Models;

namespace CommandAPI.Data;

#pragma warning disable 8603

public class SqlCommandAPIRepo : ICommandAPIRepo
{
    private readonly CommandContext context;

    public SqlCommandAPIRepo(CommandContext ctx) => context = ctx; 

    public void CreateCommand(Command cmd)
    {
        if (cmd == null) 
            throw new ArgumentNullException(nameof(cmd));

        context.CommandItems.Add(cmd);
    }

    public void DeleteCommand(Command cmd)
    {
        if (cmd == null)
            throw new ArgumentNullException(nameof(cmd));

        context.CommandItems.Remove(cmd);
    }

    public IEnumerable<Command> GetAllCommands() =>
        context.CommandItems.ToList();

    public Command GetCommandById(int id) =>
        context.CommandItems.FirstOrDefault(z => z.Id == id);

    public bool SaveChanges() => context.SaveChanges() >= 0;

    public void UpdateCommand(Command cmd) { }
}