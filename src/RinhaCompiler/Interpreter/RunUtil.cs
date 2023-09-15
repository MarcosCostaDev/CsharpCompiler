using System.Text.Json;
using System.Text.Json.Serialization;

namespace RinhaCompiler.Interpreter;

public static class RunUtil
{
    public static TExpression Find<TExpression>(Expression expression) where TExpression : Expression
    {
        if (expression is TExpression tExpression) return tExpression;
        var expResult = expression.Run();
        if (expResult is Expression exp) return Find<TExpression>(exp);
        if (typeof(TExpression) == typeof(ValueExpression))
        {
            return ConvertToValueExpression<TExpression>(expression, expResult);
        }
        throw new ArgumentException($"Error on {nameof(RunUtil)}.{nameof(Find)} from {expression.GetType()} to {typeof(TExpression)} - {expression.Location.GetLog()}");
    }

    public static FunctionExpression FindScopedFunction(this Expression expression)
    {

        if (expression is FunctionExpression tExpression) return tExpression;
        if (expression.Scope != null)
        {
            if (expression.Scope is FunctionExpression parentExpression) return parentExpression;
            else return FindScopedFunction(expression.Scope);
        }
        return null!;
    }

    public static Expression FindVariableValue(this Expression expression, string variableName)
    {
        var scopedVariables = FindScopedVariables(expression);
        if (scopedVariables.ContainsKey(variableName) == true) return scopedVariables[variableName];
        if (CompiledFile.GlobalVariables.ContainsKey(variableName) == true) return CompiledFile.GlobalVariables[variableName];
        throw new ArgumentException($"Variable {variableName} dont exist in current scope.");
    }

    public static IDictionary<string, Expression> FindScopedVariables(this Expression expression)
    {
        var functionExpression = FindScopedFunction(expression);
        if (functionExpression != null)
        {
            return functionExpression.ScopedVariables;
        }
        return CompiledFile.GlobalVariables;
    }

    public static TValueExpression? ConvertToValueExpression<TValueExpression>(Expression scope, object value) where TValueExpression : Expression
    {
        if (value is int)
        {
            return new ValueExpression<int>
            {
                Value = (int)value,
                Scope = scope,
                Location = scope.Location
            } as TValueExpression;
        }
        else if (value is bool)
        {
            return new ValueExpression<bool>
            {
                Value = (bool)value,
                Scope = scope,
                Location = scope.Location
            } as TValueExpression;
        }
        else if (value is string)
        {
            return new ValueExpression<string>
            {
                Value = (string)value,
                Scope = scope,
                Location = scope.Location
            } as TValueExpression;
        }
        throw new ArgumentException($"Error on {nameof(RunUtil)}.{nameof(ConvertToValueExpression)} from {scope.GetType()}");
    }

    public static TExpression CreateNewScope<TExpression>(this TExpression expression) where TExpression : Expression
    {
        expression.Scope = null!;
        var json = JsonSerializer.Serialize(expression, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles });
        return JsonSerializer.Deserialize<TExpression>(json, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles });
    }

}
