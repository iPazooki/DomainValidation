namespace DomainValidation.UnitTests;

public class ResultTests
{
    [Fact]
    public void Result_Success_ReturnsIsSuccessTrue()
    {
        var result = Result.Success();
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Result_Failure_ReturnsIsSuccessFalse()
    {
        var error = new Error("Some error", "Something went wrong");
        var result = Result.Failure(error);
        Assert.False(result.IsSuccess);
        Assert.Contains(error, result.Errors);
    }

    [Fact]
    public void Result_SuccessWithErrors_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(
            () => new Result(true, new Error("Some error", "Something went wrong")));
    }

    [Fact]
    public void Result_FailureWithNoErrors_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new Result(false, Error.None));
    }

    [Fact]
    public void Result_ConstructorWithMessage_Success_ReturnsIsSuccessTrue()
    {
        var result = new Result(true, "Operation successful");
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Result_ConstructorWithoutMessage_Success_ReturnsIsSuccessTrue()
    {
        var result = new Result(true);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Result_ConstructorWithMessage_Failure_ReturnsIsSuccessFalse()
    {
        var result = new Result(false, "Operation failed");
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }

    [Fact]
    public void Result_ConstructorWithoutMessage_Failure_ReturnsIsSuccessFalse()
    {
        var result = new Result(false);
        Assert.False(result.IsSuccess);
        Assert.False(result.Errors.Any());
    }
    
    [Fact]
    public void ResultWithErrorMessage_Failure_ReturnsIsSuccessFalse()
    {
        var result = Result.Failure("Operation failed");
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }
}