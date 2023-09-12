using System.Text.Json.Serialization;

namespace RinhaCompiler.Interpreter;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Term
{
    Int,
    Str,
    Call,
    Binary,
    Function,
    Let,
    If,
    Print,
    First,
    Second,
    Bool,
    Tuple,
    Var
}


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
    public Expression Scope { get; set; }
    public abstract object Run();
}
