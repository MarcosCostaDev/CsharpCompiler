namespace RinhaCompiler.Interpreter;

public sealed class CallExpression : Expression
{
    public Expression Callee { get; set; }

    public IEnumerable<Expression> Arguments { get; set; }

    public override object Run()
    {
        Callee.Scope = this;
        if (Callee is VarExpression varExpression)
        {
            if (varExpression.GetValue() is FunctionExpression functionExpression)
            {
                for (int i = 0; i < functionExpression.Parameters.Length; i++)
                {
                    var arg = Arguments.ElementAt(i);
                    arg.Scope = this;
                    if (arg is ValueExpression value)
                    {
                        functionExpression.Parameters[i].Value = value;
                    }
                    else if (arg is BinaryExpression binaryValue)
                    {
                        functionExpression.Parameters[i].Value = binaryValue.Run() as ValueExpression;
                    }
                }
                functionExpression.Scope = null!;
                return functionExpression.Run();
            }
        }
        return Callee.Run();
    }
}
