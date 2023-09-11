namespace RinhaCompiler.Interpreter;

public class RunUntil
{
    public static TExpression Find<TExpression>(Expression expression)
    {
        if (expression is TExpression tExpression) return tExpression;
        var expResult = expression.Run();
        if (expResult is Expression exp) return Find<TExpression>(exp);
        throw new ArgumentException($"Error on processing {expression.Location.GetLog()}");
    }
}
