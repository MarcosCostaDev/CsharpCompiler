using RinhaCompiler.Test.Abstracts;
using RinhaCompiler.Test.Managers;

namespace RinhaCompiler.Test.Commands;
[Collection(nameof(CommandsCollection))]
public class InterpreterCommandTest : AbstractTest
{
    public InterpreterCommandTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task HelloWorldTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("helloworld.json"));

        await command.ExecuteAsync();
    }

    [Fact]
    public async Task FibTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("fib.json"));

        await command.ExecuteAsync();
    }

    [Fact]
    public async Task LetTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("let.json"));

        await command.ExecuteAsync();
    }

    [Fact]
    public async Task IfTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("if.json"));

        await command.ExecuteAsync();
    }

    [Fact]
    public async Task AddTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("add.json"));

        await command.ExecuteAsync();
    }

    [Fact]
    public async Task FunctionTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("function.json"));

        await command.ExecuteAsync();
    }

    [Fact]
    public async Task CombinationTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("combination.json"));

        await command.ExecuteAsync();
    }

    [Fact]
    public async Task SumTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("sum.json"));

        await command.ExecuteAsync();
    }
}
