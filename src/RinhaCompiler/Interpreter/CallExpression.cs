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
                functionExpression = functionExpression.CreateNewScope();
                for (int i = 0; i < functionExpression.Parameters.Length; i++)
                {
                    var arg = Arguments.ElementAt(i);
                    arg.Scope = this;
                    if (arg is BinaryExpression binaryValue)
                    {
                        var valueProcessed = binaryValue.Run() as ValueExpression;
                        functionExpression.Parameters[i].Value = valueProcessed.Run();
                    }
                    else
                    {
                        functionExpression.Parameters[i].Value = arg.Run();
                    }
                }

                return functionExpression.Run();
            }
        }
        return Callee.Run();
    }
}
