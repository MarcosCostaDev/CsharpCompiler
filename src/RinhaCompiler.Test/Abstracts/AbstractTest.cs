using RinhaCompiler.Test.Managers;
using Xunit.Sdk;

namespace RinhaCompiler.Test.Abstracts;

public abstract class AbstractTest
{
    public AbstractTest(ITestOutputHelper output)
    {
        Console.SetOut(new RedirectOutput(output));
        OutputTestlog = output;
    }

    protected string GetLogMessages()
    {
        if (OutputTestlog is TestOutputHelper outputHelper)
        {
            return outputHelper.Output.Trim();
        }
        return string.Empty;
    }

    protected ITestOutputHelper OutputTestlog { get; private set; }
}
