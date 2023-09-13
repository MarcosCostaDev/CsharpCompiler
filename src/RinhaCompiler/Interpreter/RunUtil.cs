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
        var functionExpression = FindScopedFunction(expression);
        if (functionExpression?.ScopedVariables.ContainsKey(variableName) == true) return functionExpression.ScopedVariables[variableName];
        else if (CompiledFile.GlobalVariables.ContainsKey(variableName)) return CompiledFile.GlobalVariables[variableName];
        throw new ArgumentException($"Variable {variableName} dont exist in current scope.");
    }

    public static Expression FindAndCreateOrUpdateScopedVariableValue(this Expression expression, string variableName, Expression updatedValue)
    {
        var functionExpression = FindScopedFunction(expression);
        if (functionExpression != null)
        {

            if (functionExpression.ScopedVariables.ContainsKey(variableName) == true)
            {
                functionExpression.ScopedVariables[variableName] = updatedValue;
                return functionExpression.ScopedVariables[variableName];
            }
            else
            {

                functionExpression.ScopedVariables.Add(variableName, updatedValue);
                return updatedValue;
            }
        }
        else if (CompiledFile.GlobalVariables.ContainsKey(variableName))
        {
            CompiledFile.GlobalVariables[variableName] = updatedValue;
            return CompiledFile.GlobalVariables[variableName];
        }
        else
        {
            CompiledFile.GlobalVariables.TryAdd(variableName, updatedValue);
            return CompiledFile.GlobalVariables[variableName];
        }
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
            } as TValueExpression ;
        }
        throw new ArgumentException($"Error on {nameof(RunUtil)}.{nameof(ConvertToValueExpression)} from {scope.GetType()}");
    }
}
