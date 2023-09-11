namespace RinhaCompiler.Interpreter;

public sealed class ConditionExpression : Expression
{
    public Expression Condition { get; set; }
    public Expression Then { get; set; }
    public Expression Otherwise { get; set; }

    public override object Run()
    {
        if (Condition.Run() is ValueExpression<bool> conditionResult)
        {
            if (conditionResult.Value)
            {
                return Then.Run();
            }
            return Otherwise?.Run();
        }

        throw new ArgumentException($"Error on {nameof(ConditionExpression)} - {Location.GetLog()}");
    }
}
