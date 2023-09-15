namespace RinhaCompiler.Interpreter;

public sealed class LetExpression : Expression
{
    public VarExpression Name { get; set; }
    public Expression Next { get; set; }
    public Expression Value { get; set; }
    public override object Run()
    {
        Next.Scope = this;
        Value.Scope = this;
        var variables = this.FindScopedVariables();

        if (!variables.ContainsKey(Name.Text))
        {
            variables.Add(Name.Text, Value is BinaryExpression ? Value.Run() as ValueExpression : Value);
        }


        return Next.Run();
    }
}
