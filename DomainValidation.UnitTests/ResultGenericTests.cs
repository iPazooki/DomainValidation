using DomainValidation;

namespace ExceptionResult.Tests;

public class ResultGenericTests
{
    [Fact]
    public void Implicit_Conversion_Should_Create_Successful_Result_With_Value()
    {
        // Arrange
        var value = 1;

        // Act
        Result<int> result = value;

        // Assert
        Assert.True(result.IsSuccess);

        Assert.Equal(Error.None, result.Error);

        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void Value_Should_Throw_Exception_If_Not_Successful()
    {
        // Arrange
        var error = new Error("Error.Code", "Something went wrong");

        var failedResult = Result.Failure<int>(error);

        // Act & assert
        Assert.Equal(28, failedResult.Error.LineNumber);

        Assert.Equal("Value_Should_Throw_Exception_If_Not_Successful", failedResult.Error.MemberName);

        Assert.Throws<InvalidOperationException>(() => failedResult.Value);
    }
}
