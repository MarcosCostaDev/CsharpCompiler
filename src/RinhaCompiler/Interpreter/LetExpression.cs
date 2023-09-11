using System.Linq.Expressions;

namespace RinhaCompiler.Interpreter;

public sealed class LetExpression : Expression
{
    public VarExpression Name { get; set; }
    public Expression Next { get; set; }
    public Expression Value { get; set; }
    public override object Run()
    {
        CompiledFile.GlobalVariables.TryAdd(Name.Text, Value);

        return Next?.Run();
    }
}
