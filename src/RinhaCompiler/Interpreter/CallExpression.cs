namespace RinhaCompiler.Interpreter;

public sealed class CallExpression : Expression
{
    public Expression Callee { get; set; }

    public IEnumerable<Expression> Arguments { get; set; }

    public override object Run()
    {
        Callee.Parent = this;
        if (Callee is VarExpression varExpression)
        {
            if (varExpression.GetValue() is FunctionExpression functionExpression)
            {
                for (int i = 0; i < functionExpression.Parameters.Length; i++)
                {
                    if(Arguments.ElementAt(i) is ValueExpression value)
                    {
                        value.Parent = this;
                        functionExpression.Parameters[i].Value = value;
                    }
                    else if(Arguments.ElementAt(i) is BinaryExpression binaryValue)
                    {
                        binaryValue.Parent = this;
                        functionExpression.Parameters[i].Value = binaryValue.Run() as ValueExpression;
                    }
                   
                }
                functionExpression.Parent = this;
                return functionExpression.Run();
            }
        }
        return Callee.Run();
    }
}
