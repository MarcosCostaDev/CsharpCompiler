using System.Text.Json;
using System.Text.Json.Serialization;

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
                        functionExpression.Parameters[i].Value = value.Run();
                    }
                    else if (arg is BinaryExpression binaryValue)
                    {
                        var valueProcessed = binaryValue.Run() as ValueExpression;
                        valueProcessed.Scope = null;
                        functionExpression.Parameters[i].Value = valueProcessed.Run();
                    }
                }
                functionExpression.Scope = null!;
                var func = functionExpression.DeepClone<FunctionExpression>();
                return func.Run();
            }
        }
        return Callee.Run();
    }
}
