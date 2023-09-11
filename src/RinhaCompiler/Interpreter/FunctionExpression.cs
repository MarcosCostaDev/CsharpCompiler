using System.Collections.Generic;

namespace RinhaCompiler.Interpreter;

public sealed class FunctionExpression : ValueExpression<Expression>
{
    public Dictionary<string, Expression> ScopedVariables = new();
    public ParameterExpression[] Parameters { get; set; }

    public override object Run()
    {
        for (int i = 0; i < Parameters.Length; i++)
        {
            if (ScopedVariables.ContainsKey(Parameters[i].Text))
                ScopedVariables[Parameters[i].Text] = Parameters[i].Value;
            else
                ScopedVariables.Add(Parameters[i].Text, Parameters[i].Value);
        }
        Value.Parent = this;
        return Value.Run();
    }
}
