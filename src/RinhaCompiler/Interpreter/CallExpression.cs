namespace RinhaCompiler.Interpreter;

public sealed class CallExpression : Expression
{
    public Expression Callee { get; set; }

    public IEnumerable<Expression> Arguments { get; set; }

    public override object Run()
    {
        if (Callee is VarExpression varExpression)
        {
            if (CompiledFile.GlobalVariables[varExpression.Text] is FunctionExpression functionExpression)
            {
                for (int i = 0; i < functionExpression.Parameters.Count(); i++)
                {
                    functionExpression.Parameters[i].Value = Arguments.ElementAt(i) as ValueExpression;
                }
               
                return functionExpression.Run();
            }
        }
        return Callee.Run();
    }
}
