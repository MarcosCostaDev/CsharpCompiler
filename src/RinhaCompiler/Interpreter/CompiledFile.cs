namespace RinhaCompiler.Interpreter;

public class CompiledFile : FileLocator, ICommandExecute
{
    public string Name { get; set; }

    public Expression Expression { get; set; }

    public object Run()
    {
        return Expression.Run();
    }
}
