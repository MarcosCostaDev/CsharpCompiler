using System.CommandLine;

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

        ValueExpression result = null;
        if (Value is BinaryExpression binaryExpression)
        {
            var runResult = binaryExpression.Run();
            result = runResult as ValueExpression;
            RunUntil.FindAndCreateOrUpdateScopedVariableValue(this, Name.Text, result);
        }
        else
        {

            RunUntil.FindAndCreateOrUpdateScopedVariableValue(this, Name.Text, Value);
        }

        return Next.Run();
    }
}
