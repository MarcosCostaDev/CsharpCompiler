namespace RinhaCompiler.Interpreter;

public sealed class FunctionExpression : ValueExpression<Expression>
{
    public ParameterExpression[] Parameters { get; set; }

    public override object Run()
    {
        return Value.Run();
    }
}
