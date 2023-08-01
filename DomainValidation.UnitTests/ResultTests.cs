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

        Assert.Equal(Error.None, result.Errors.Single());
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

        Assert.Equal(Error.None, result.Errors.Single());

        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void Failure_Should_Return_Failed_Result_With_Error()
    {
        // Arrange
        var error = new Error("Error.Code", "Something went wrong");

        // Act
        var result = Result.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);

        Assert.Equal(error, result.Errors.Single());

        Assert.Equal(41, result.Errors.First().LineNumber);

        Assert.Equal("Failure_Should_Return_Failed_Result_With_Error", result.Errors.Single().MemberName);
    }

    [Fact]
    public void Failure_Should_Return_Failed_Results_With_Error()
    {
        // Arrange
        var errors = new List<Error>(){
            new Error("Error.Code.1", "Something went wrong 1") ,
            new Error("Error.Code.2", "Something went wrong 2")
        };

        // Act
        var result = Result.Failure(errors.ToArray());

        // Assert
        Assert.False(result.IsSuccess);

        Assert.Equal(errors.Count, result.Errors.Count);

        Assert.Equal(errors, result.Errors);
    }
}
