using Rinha.Interpreter;

namespace Rinha.Test.Interpreter;

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
        result.Should().NotBeNull()
              .And.BeOfType<ValueExpression<int>>()
              .And.Match<ValueExpression<int>>(v => v.Value == value && v.Scope == scope && v.Location == scope.Location);

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
        result.Should().NotBeNull()
              .And.BeOfType<ValueExpression<bool>>()
              .And.Match<ValueExpression<bool>>(v => v.Value == value && v.Scope == scope && v.Location == scope.Location);
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
        result.Should().NotBeNull()
            .And.BeOfType<ValueExpression<string>>()
            .And.Match<ValueExpression<string>>(v => v.Value == value && v.Scope == scope && v.Location == scope.Location);

    }

    [Fact]
    public void ConvertToValueExpression_InvalidType_ShouldThrowArgumentException()
    {
        // Arrange
        Expression scope = new FunctionExpression() { Location = new Location() };
        object value = new object(); // Invalid type

        // Act & Assert
        Action action = () => RunUtil.ConvertToValueExpression<ValueExpression<int>>(scope, value);
        action.Should().Throw<ArgumentException>();
    }



    [Fact]
    public void FindVariableValue_VariableExistsInScopedFunction_ReturnsValue()
    {
        // Arrange
        var scopedExpression = new FunctionExpression();
        scopedExpression.ScopedVariables["x"] = new ValueExpression<int> { Value = 42 };

        // Act
        var result = scopedExpression.FindVariableValue("x") as ValueExpression<int>;

        // Assert
        result.Value.Should().Be(42);
    }

    [Fact]
    public void FindVariableValue_VariableExistsInGlobalVariables_ReturnsValue()
    {
        // Arrange
        CompiledFile.GlobalVariables["y"] = new ValueExpression<int> { Value = 99 };
        var scopedExpression = new FunctionExpression();
        scopedExpression.ScopedVariables["x"] = new ValueExpression<int> { Value = 42 };

        // Act
        var result = scopedExpression.FindVariableValue("y") as ValueExpression<int>;


        // Assert
        result.Value.Should().Be(99);

    }

    [Fact]
    public void FindVariableValue_VariableDoesNotExist_ThrowsArgumentException()
    {
        // Arrange
        CompiledFile.GlobalVariables["y"] = new ValueExpression<int> { Value = 99 };
        var scopedExpression = new FunctionExpression();
        scopedExpression.ScopedVariables["x"] = new ValueExpression<int> { Value = 42 };


        // Act & Assert
        Action action = () => scopedExpression.FindVariableValue("z");
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void FindVariableValue_VariableExistsInScopedFunctionAndGlobal_ReturnsScopedValue()
    {
        // Arrange
        CompiledFile.GlobalVariables["x"] = new ValueExpression<int> { Value = 99 };
        var scopedExpression = new FunctionExpression();
        scopedExpression.ScopedVariables["x"] = new ValueExpression<int> { Value = 42 };

        // Act
        var result = scopedExpression.FindVariableValue("x") as ValueExpression<int>;

        // Assert
        result.Value.Should().Be(42);
    }

    [Fact]
    public void Find_ValueExpression_Returns_ValueExpression()
    {
        // Arrange
        Expression expression = new ValueExpression<int>();

        // Act
        var result = RunUtil.Find<ValueExpression<int>>(expression);

        // Assert
        Assert.IsType<ValueExpression<int>>(result);
    }

    [Fact]
    public void Find_NestedExpression_Returns_ValueExpression()
    {
        // Arrange
        BinaryExpression innerExpression = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            RightHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            Operation = BinaryOperator.And
        };
        Expression expression = new FunctionExpression { Value = innerExpression, Parameters = Array.Empty<ParameterExpression>() };

        // Act
        var result = RunUtil.Find<ValueExpression>(expression);

        // Assert
        Assert.IsType<ValueExpression<bool>>(result);
    }

    [Fact]
    public void Find_NestedExpression_Returns_Exception()
    {
        // Arrange
        Expression innerExpression = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<bool>
            {
                Value = false,
                Location = new Location { End = 1, Start = 2, FileName = "test.json" }
            },
            RightHandSide = new ValueExpression<bool>
            {
                Value = false,
                Location = new Location { End = 1, Start = 2, FileName = "test.json" }
            },
            Operation = BinaryOperator.And,
            Location = new Location { End = 1, Start = 2, FileName = "test.json" }
        };
        Expression expression = new FunctionExpression
        {
            Value = innerExpression,
            Parameters = Array.Empty<ParameterExpression>(),
            Location = new Location { End = 1, Start = 2, FileName = "test.json" }
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => RunUtil.Find<VarExpression>(expression));
        Assert.Contains("Error on RunUtil.Find", ex.Message);
    }

    [Fact]
    public void Find_NestedExpressionWithConversion_Returns_ValueExpression()
    {
        // Arrange
        Expression childExpression = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            RightHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            Operation = BinaryOperator.And
        };
        ValueExpression innerExpression = new ValueExpression<Expression> { Value = childExpression };

        Expression functionExpression = new FunctionExpression { Value = innerExpression, Parameters = Array.Empty<ParameterExpression>() };

        // Act
        var result = RunUtil.Find<ValueExpression>(functionExpression); // Requesting a base class type

        // Assert
        Assert.IsType<ValueExpression<bool>>(result);
    }
}
