namespace RinhaCompiler.Interpreter;

public sealed class CallExpression : Expression
{
    public Expression Callee { get; set; }

    public Expression[] Arguments { get; set; }

    public override object Run()
    {
        return Callee.Run();
    }
}
