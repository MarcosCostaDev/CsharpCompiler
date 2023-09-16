using Rinha.Test.Abstracts;
using Rinha.Test.Managers;

namespace Rinha.Test.Commands;
[Collection(nameof(CommandsCollection))]
public class InterpreterCommandTest : AbstractTest
{
    public InterpreterCommandTest(ITestOutputHelper outputTestLog) : base(outputTestLog)
    {
    }

    [Fact]
    public void HelloWorldMessage()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("helloworld.json"));

        command.Execute();

        GetLogMessages().Should().Be("Hello, world!");
    }

    [Fact]
    public void FibonacciResult_eq_55()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("fib.json"));

        command.Execute();

        Convert.ToInt32(GetLogMessages()).Should().Be(55);
    }

    [Fact]
    public void LetSum_eq_3()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("let.json"));

        command.Execute();

        Convert.ToInt32(GetLogMessages()).Should().Be(3);
    }

    [Fact]
    public void If_eq_verdadeiro()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("if.json"));

        command.Execute();

        GetLogMessages().Should().Be("Verdadeiro");
    }

    [Fact]
    public void Add_eq_formula_str()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("add.json"));

        command.Execute();

        GetLogMessages().Should().Be("1 + 2 = 3");
    }

    [Fact]
    public void Function_variable_not_exist_should_throw_exception()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("function.json"));

        Assert.Throws<ArgumentException>(() => command.Execute());
    }

    [Fact]
    public void Combination_eq_45()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("combination.json"));

        command.Execute();

        Convert.ToInt32(GetLogMessages()).Should().Be(45);
    }

    [Fact]
    public void Sum_eq_15()
    {
        var command = new InterpreterCommand(FileManager.GetFullPath("sum.json"));

        command.Execute();

        Convert.ToInt32(GetLogMessages()).Should().Be(15);
    }
}
