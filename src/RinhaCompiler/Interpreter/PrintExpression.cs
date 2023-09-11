namespace RinhaCompiler.Interpreter;

public sealed class PrintExpression : ValueExpression<Expression>
{
    public override object Run()
    {
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
            if (expressionValue is PrintExpression) throw new ArgumentException($"Error on {Location.GetLog()}");
            if (expressionValue is TupleExpression tupleExpression)
            {
                Console.WriteLine($"({tupleExpression.First.Run()}, {tupleExpression.Second.Run()})");
                return null!;
            }

            Console.WriteLine(expressionValue.Run());
        }
        return null!;
    }
}
