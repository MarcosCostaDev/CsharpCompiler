namespace RinhaCompiler.Interpreter;

public sealed class SecondExpression : ValueExpression<Expression>
{
    public override object Run()
    {
        if (Value is TupleExpression tupleExpression)
        {
            return tupleExpression.Second.Run();
        }

        throw new ArgumentException($"Error on  {nameof(SecondExpression)}.{nameof(Run)} - {Location.GetLog()}");
    }
}
