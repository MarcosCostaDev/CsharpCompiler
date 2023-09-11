namespace RinhaCompiler.Interpreter;

public sealed class ConditionExpression : Expression
{
    public Expression Condition { get; set; }
    public Expression Then { get; set; }
    public Expression Otherwise { get; set; }

    public override object Run()
    {
        Condition.Parent = this;

        if (Condition.Run() is ValueExpression<bool> conditionResult)
        {
            if (conditionResult.Value)
            {
                Then.Parent = this;
                return Then.Run();
            }
            else if (Otherwise != null)
            {
                Otherwise.Parent = this;
                return Otherwise.Run();
            }
        }
        throw new ArgumentException($"Error on {nameof(ConditionExpression)} - {Location.GetLog()}");
    }
}
