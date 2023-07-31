using DomainValidation;

namespace ExceptionResult.Tests;

public class ResultTests
{
    [Fact]
    public void Success_Should_Return_Successful_Result()
    {
        // Arrange
        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccess);

        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void Success_TValue_Should_Return_Successful_Result_With_Value()
    {
        // Arrange
        var value = 1;

        // Act
        var result = Result.Success(value);

        // Assert
        Assert.True(result.IsSuccess);

        Assert.Equal(Error.None, result.Error);

        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void Failure_Should_Return_Failed_Result_With_Error()
    {
        // Arrange
        var error = new Error("Error.Code","Something went wrong");

        // Act
        var result = Result.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);

        Assert.Equal(error, result.Error);

        Assert.Equal(41, result.Error.LineNumber);

        Assert.Equal("Failure_Should_Return_Failed_Result_With_Error", result.Error.MemberName);
    }

    [Fact]
    public void Failure_TValue_Should_Return_Failed_Result_With_Error_And_ExceptionForValue()
    {
        // Arrange
        var error = new Error("Error.Code", "Something went wrong");

        var value = 1;

        // Act
        var result = Result.Failure(error, value);

        // Assert
        Assert.False(result.IsSuccess);

        Assert.Equal(error, result.Error);

        Assert.Throws<InvalidOperationException>(()=> result.Value);
    }
}
