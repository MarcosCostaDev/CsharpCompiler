using RinhaCompiler.Interpreter;

namespace RinhaCompiler.Test.Interpreter;

public class RunUtilTest
{
    [Fact]
    public void ConvertToValueExpression_IntValue_ShouldReturnCorrectType()
    {
        // Arrange
        Expression scope = new FunctionExpression() { Location = new Location() };
        int value = 42;

        // Act
        var result = RunUtil.ConvertToValueExpression<ValueExpression<int>>(scope, value);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ValueExpression<int>>(result);
        Assert.Equal(value, ((ValueExpression<int>)result).Value);
        Assert.Equal(scope, result.Scope);
        Assert.Equal(scope.Location, result.Location);
    }

    [Fact]
    public void ConvertToValueExpression_BoolValue_ShouldReturnCorrectType()
    {
        // Arrange
        Expression scope = new FunctionExpression() { Location = new Location() };
        bool value = true;

        // Act
        var result = RunUtil.ConvertToValueExpression<ValueExpression<bool>>(scope, value);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ValueExpression<bool>>(result);
        Assert.Equal(value, ((ValueExpression<bool>)result).Value);
        Assert.Equal(scope, result.Scope);
        Assert.Equal(scope.Location, result.Location);
    }

    [Fact]
    public void ConvertToValueExpression_StringValue_ShouldReturnCorrectType()
    {
        // Arrange
        Expression scope = new FunctionExpression() { Location = new Location() };
        string value = "Hello, World!";

        // Act
        var result = RunUtil.ConvertToValueExpression<ValueExpression<string>>(scope, value);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ValueExpression<string>>(result);
        Assert.Equal(value, ((ValueExpression<string>)result).Value);
        Assert.Equal(scope, result.Scope);
        Assert.Equal(scope.Location, result.Location);
    }

    [Fact]
    public void ConvertToValueExpression_InvalidType_ShouldThrowArgumentException()
    {
        // Arrange
        Expression scope = new FunctionExpression() { Location = new Location() };
        object value = new object(); // Invalid type

        // Act & Assert
        Assert.Throws<ArgumentException>(() => RunUtil.ConvertToValueExpression<ValueExpression<int>>(scope, value));
    }
}
