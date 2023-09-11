using System.Collections.Concurrent;

namespace RinhaCompiler.Interpreter;

public class CompiledFile : FileLocator, ICommandExecute
{
    public static ConcurrentDictionary<string, Expression> GlobalVariables = new();
    public string Name { get; set; }

    public Expression Expression { get; set; }

    public object Run()
    {
        return Expression.Run();
    }
}
