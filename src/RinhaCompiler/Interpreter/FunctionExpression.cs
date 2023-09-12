namespace RinhaCompiler.Interpreter;

public sealed class FunctionExpression : Expression
{
    public Dictionary<string, Expression> ScopedVariables = new();
    public ParameterExpression[] Parameters { get; set; }
    public Expression Value { get; set; }

    public override object Run()
    {
        UpdateScopedVariables();
        Value.Scope = this;
        return Value.Run();
    }

    public void UpdateScopedVariables()
    {
        for (int i = 0; i < Parameters.Length; i++)
        {
            if (ScopedVariables.ContainsKey(Parameters[i].Text))
            {
                ScopedVariables[Parameters[i].Text] = Parameters[i].Value;
            }
            else
            {
                ScopedVariables.Add(Parameters[i].Text, Parameters[i].Value);
            }
        }
    }
}
