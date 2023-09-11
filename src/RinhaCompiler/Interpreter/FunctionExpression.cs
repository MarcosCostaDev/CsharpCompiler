using System.Collections.Generic;

namespace RinhaCompiler.Interpreter;

public sealed class FunctionExpression : ValueExpression<Expression>
{
    public ParameterExpression[] Parameters { get; set; }

    public override object Run()
    {
        for (int i = 0; i < Parameters.Length; i++)
        {
            CompiledFile.GlobalVariables.TryAdd(Parameters[i].Text, Parameters[i].Value);
        }
        return Value.Run();
    }
}
