namespace RinhaCompiler.Interpreter;

public static class RunUntil
{
    public static TExpression Find<TExpression>(Expression expression)
    {
        if (expression is TExpression tExpression) return tExpression;
        var expResult = expression.Run();
        if (expResult is Expression exp) return Find<TExpression>(exp);
        throw new ArgumentException($"Error on {nameof(RunUntil)}.{nameof(Find)} {expression.GetType()} >> {typeof(TExpression)}- {expression.Location.GetLog()}");
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
        if (functionExpression?.ScopedVariables.ContainsKey(variableName) == true)
            return functionExpression.ScopedVariables[variableName];
        else if (CompiledFile.GlobalVariables.TryGetValue(variableName, out var value))
            return value;
        throw new ArgumentException($"Variable {variableName} dont exist in current scope.");
    }

    public static Expression FindAndCreateOrUpdateScopedVariableValue(this Expression expression, string variableName, Expression updatedValue)
    {
        var functionExpression = FindScopedFunction(expression);
        if(functionExpression != null)
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
}
