using RinhaCompiler.Test.Abstracts;
using RinhaCompiler.Test.Managers;
using System.Runtime.CompilerServices;

namespace RinhaCompiler.Test.Commands;

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
}
