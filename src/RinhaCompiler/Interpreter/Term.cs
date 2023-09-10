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
