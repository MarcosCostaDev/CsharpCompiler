namespace RinhaCompiler.Interpreter;

public sealed class PrintExpression : ValueExpression<Expression>
{
    public override object Run()
    {
        Value.Parent = this;
        var runResult = Value.Run();
        if (runResult is bool boolValue)
        {
            Console.WriteLine(boolValue);
        }
        else if (runResult is string strValue)
        {
            Console.WriteLine(strValue);
        }
        else if (runResult is int intValue)
        {
            Console.WriteLine(intValue);
        }
        else if (runResult is Expression expressionValue)
        {
            if (expressionValue is PrintExpression) throw new ArgumentException($"Error on {nameof(PrintExpression)}.{nameof(Run)} -  {Location.GetLog()}");
            if (expressionValue is TupleExpression tupleExpression)
            {
                Console.WriteLine($"({tupleExpression.First.Run()}, {tupleExpression.Second.Run()})");
                return null!;
            }
            expressionValue.Parent = this;
            Console.WriteLine(expressionValue.Run());
        }
        return null!;
    }
}
