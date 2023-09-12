namespace RinhaCompiler.Interpreter;

public sealed class ParameterExpression : FileLocator, ICommandExecute
{
    public string Text { get; set; }
    public object Value { get; set; }
    public object Run()
    {
        return Text;
    }

}
