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
            var value = Parameters[i].GetValue(this);

            ScopedVariables.Add(Parameters[i].Text, value);
        }
    }
}
