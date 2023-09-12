using System.Text.Json;

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
            Expression value = null!;
            if (Parameters[i].Value is JsonElement jsonValue)
            {
                switch (jsonValue.ValueKind)
                {
                    case JsonValueKind.String:
                        value = new ValueExpression<string> { Value = jsonValue.ToString(), Scope = this, Location = Location };
                        break;
                    case JsonValueKind.Number:
                        value = new ValueExpression<int> { Value = Convert.ToInt32(jsonValue.ToString()), Scope = this, Location = Location };
                        break;
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        value = new ValueExpression<bool> { Value = Convert.ToBoolean(jsonValue.ToString()), Scope = this, Location = Location };
                        break;
                }
            }

            if (ScopedVariables.ContainsKey(Parameters[i].Text))
            {
                ScopedVariables[Parameters[i].Text] = value;
            }
            else
            {
                ScopedVariables.Add(Parameters[i].Text, value);
            }
        }
    }
}
