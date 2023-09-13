using RinhaCompiler.Test.Abstracts;
using RinhaCompiler.Test.Managers;

namespace RinhaCompiler.Test.Commands;
[Collection(nameof(CommandsCollection))]
public class InterpreterCommandTest : AbstractTest
{
    public InterpreterCommandTest(ITestOutputHelper outputTestLog) : base(outputTestLog)
    {
    }

    [Fact]
    public async Task HelloWorldTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("helloworld.json"));

        await command.ExecuteAsync();

        GetLogMessages().Should().Be("Hello, world!");
    }

    [Fact]
    public async Task FibTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("fib.json"));

        await command.ExecuteAsync();

        Convert.ToInt32(GetLogMessages()).Should().Be(55);
    }

    [Fact]
    public async Task LetTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("let.json"));

        await command.ExecuteAsync();

        Convert.ToInt32(GetLogMessages()).Should().Be(3);
    }

    [Fact]
    public async Task IfTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("if.json"));

        await command.ExecuteAsync();

        GetLogMessages().Should().Be("Verdadeiro");
    }

    [Fact]
    public async Task AddTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("add.json"));

        await command.ExecuteAsync();

        GetLogMessages().Should().Be("1 + 2 = 3");
    }

    [Fact]
    public async Task FunctionTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("function.json"));

        await command.ExecuteAsync();

        Convert.ToInt32(GetLogMessages()).Should().Be(6);
    }

    [Fact]
    public async Task CombinationTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("combination.json"));

        await command.ExecuteAsync();

        Convert.ToInt32(GetLogMessages()).Should().Be(3);
    }

    [Fact]
    public async Task SumTest()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("sum.json"));

        await command.ExecuteAsync();

        Convert.ToInt32(GetLogMessages()).Should().Be(15);
    }
}
