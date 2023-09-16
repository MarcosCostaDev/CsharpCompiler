using System.Text.Json;

namespace Rinha.Interpreter;

public sealed class ParameterExpression : FileLocator, ICommandExecute
{
    public string Text { get; set; }
    public object Value { get; set; }
    public object Run()
    {
        return Text;
    }

    public ValueExpression GetValue(Expression scope)
    {
        if (Value is ValueExpression value) return value;
        else if (Value is int intValue) return new ValueExpression<int> { Value = intValue, Scope = scope, Location = Location };
        else if (Value is string strValue) return new ValueExpression<string> { Value = strValue, Scope = scope, Location = Location };
        else if (Value is bool boolValue) return new ValueExpression<bool> { Value = boolValue, Scope = scope, Location = Location };
        else if (Value is JsonElement jsonValue)
        {
            switch (jsonValue.ValueKind)
            {
                case JsonValueKind.String:
                    return new ValueExpression<string> { Value = jsonValue.ToString(), Scope = scope, Location = Location };
                case JsonValueKind.Number:
                    return new ValueExpression<int> { Value = Convert.ToInt32(jsonValue.ToString()), Scope = scope, Location = Location };
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return new ValueExpression<bool> { Value = Convert.ToBoolean(jsonValue.ToString()), Scope = scope, Location = Location };
            }
        }
        return null!;
    }

}
