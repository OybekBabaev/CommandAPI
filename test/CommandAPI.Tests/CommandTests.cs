using CommandAPI.Models;
using Xunit;

namespace CommandAPI.Tests;

public class CommandTests : System.IDisposable
{
    Command commandToTest;

    public CommandTests()
    {
        commandToTest = new()
        {
            HowTo = "Eins",
            Platform = "Zwei",
            CommandLine = "Drei"
        };
    }

    public void Dispose() => commandToTest = null;

    [Fact]
    public void CanChangeHowTo()
    {
        commandToTest.HowTo = "Vier";

        Assert.Equal("Vier", commandToTest.HowTo);
    }

    [Fact]
    public void CanChangePlatform()
    {
        commandToTest.Platform = "Fuenf";

        Assert.Equal("Fuenf", commandToTest.Platform);
    }

    [Fact]
    public void CanChangeCommandLine()
    {
        commandToTest.CommandLine = "Sechs";

        Assert.Equal("Sechs", commandToTest.CommandLine);
    }
}