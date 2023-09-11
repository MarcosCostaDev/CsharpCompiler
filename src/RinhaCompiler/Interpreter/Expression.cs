using System.Text.Json.Serialization;

namespace RinhaCompiler.Interpreter;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(VarExpression), typeDiscriminator: nameof(Term.Var))]
[JsonDerivedType(typeof(FunctionExpression), typeDiscriminator: nameof(Term.Function))]
[JsonDerivedType(typeof(CallExpression), typeDiscriminator: nameof(Term.Call))]
[JsonDerivedType(typeof(LetExpression), typeDiscriminator: nameof(Term.Let))]
[JsonDerivedType(typeof(ValueExpression<string>), typeDiscriminator: nameof(Term.Str))]
[JsonDerivedType(typeof(ValueExpression<int>), typeDiscriminator: nameof(Term.Int))]
[JsonDerivedType(typeof(ValueExpression<bool>), typeDiscriminator: nameof(Term.Bool))]
[JsonDerivedType(typeof(BinaryExpression), typeDiscriminator: nameof(Term.Binary))]
[JsonDerivedType(typeof(ConditionExpression), typeDiscriminator: nameof(Term.If))]
[JsonDerivedType(typeof(TupleExpression), typeDiscriminator: nameof(Term.Tuple))]
[JsonDerivedType(typeof(FirstExpression), typeDiscriminator: nameof(Term.First))]
[JsonDerivedType(typeof(SecondExpression), typeDiscriminator: nameof(Term.Second))]
[JsonDerivedType(typeof(PrintExpression), typeDiscriminator: nameof(Term.Print))]
public abstract class Expression : FileLocator, ICommandExecute
{
    public abstract object Run();
}
