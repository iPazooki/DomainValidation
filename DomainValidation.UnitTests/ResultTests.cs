namespace DomainValidation.UnitTests;

public class ResultTests
{
    [Fact]
    public void Result_Success_ReturnsIsSuccessTrue()
    {
        var result = Result.Success();
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Errors);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Result_Failure_ReturnsIsSuccessFalse()
    {
        var error = new Error("Something went wrong", "ErrorCode");
        var result = Result.Failure(error);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Errors);
        Assert.Contains(error, result.Errors);
    }

    [Fact]
    public void Result_SuccessWithErrors_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(
            () => new Result(true, new Error("Something went wrong", "ErrorCode")));
    }

    [Fact]
    public void Result_ConstructorWithMessage_Success_ReturnsIsSuccessTrue()
    {
        var result = new Result(true, "Operation successful");
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Errors);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void Result_ConstructorErrorNone_Success_ReturnsIsSuccessTrue()
    {
        var result = new Result(true);
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
        Assert.NotNull(result.Errors);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }
    
    [Fact]
    public void Result_ConstructorWithError_Failure_ReturnsIsSuccessFalse()
    {
        var result = new Result(false, new Error("Operation failed"));
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Errors);
        Assert.Single(result.Errors);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }

    [Fact]
    public void Result_ConstructorWithoutMessage_Failure_ReturnsIsSuccessFalse()
    {
        var result = new Result(false);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Errors);
        Assert.Single(result.Errors);
        Assert.Equal(result.Errors.First(), Error.Default);
    }
    
    [Fact]
    public void ResultWithErrorMessage_Failure_ReturnsIsSuccessFalse()
    {
        var result = Result.Failure("Operation failed");
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Errors);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }
}