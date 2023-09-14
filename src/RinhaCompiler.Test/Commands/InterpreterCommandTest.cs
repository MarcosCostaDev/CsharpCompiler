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
    public async Task HelloWorldMessage()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("helloworld.json"));

        await command.ExecuteAsync();

        GetLogMessages().Should().Be("Hello, world!");
    }

    [Fact]
    public async Task FibonacciResult_eq_55()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("fib.json"));

        await command.ExecuteAsync();

        Convert.ToInt32(GetLogMessages()).Should().Be(55);
    }

    [Fact]
    public async Task LetSum_eq_3()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("let.json"));

        await command.ExecuteAsync();

        Convert.ToInt32(GetLogMessages()).Should().Be(3);
    }

    [Fact]
    public async Task if_eq_verdadeiro()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("if.json"));

        await command.ExecuteAsync();

        GetLogMessages().Should().Be("Verdadeiro");
    }

    [Fact]
    public async Task add_eq_formula_str()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("add.json"));

        await command.ExecuteAsync();

        GetLogMessages().Should().Be("1 + 2 = 3");
    }

    [Fact]
    public async Task Function_variable_not_exist_should_throw_exception()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("function.json"));

       await Assert.ThrowsAsync<ArgumentException>(async () => await command.ExecuteAsync());
    }

    [Fact]
    public async Task Combination_eq_44()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("combination.json"));

        await command.ExecuteAsync();

        Convert.ToInt32(GetLogMessages()).Should().Be(44);
    }

    [Fact]
    public async Task Sum_eq_15()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("sum.json"));

        await command.ExecuteAsync();

        Convert.ToInt32(GetLogMessages()).Should().Be(15);
    }
}
