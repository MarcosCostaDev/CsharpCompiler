using RinhaCompiler.Test.Managers;
using Xunit.Abstractions;

namespace RinhaCompiler.Test.Abstracts;

public abstract class AbstractTest
{
    public AbstractTest(ITestOutputHelper output)
    {
        Console.SetOut(new RedirectOutput(output));
        Output = output;
    }

    protected ITestOutputHelper Output { get; private set; }
}
