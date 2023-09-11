namespace RinhaCompiler.Interpreter;

public sealed class FirstExpression : ValueExpression<Expression>
{
    public override object Run()
    {
        if (Value is TupleExpression tupleExpression)
        {
            return tupleExpression.First.Run();
        }

        throw new ArgumentException($"Error on processing {Location.GetLog()}");
    }
}
