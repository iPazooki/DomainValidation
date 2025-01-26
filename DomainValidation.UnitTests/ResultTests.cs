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
        Assert.Throws<InvalidOperationException>(() => new Result(true, new Error("Some error", "Something went wrong")));
    }

    [Fact]
    public void Result_FailureWithNoErrors_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => new Result(false, Error.None));
    }

    [Fact]
    public void ResultGeneric_Success_ReturnsValue()
    {
        var result = Result.Success(42);
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ResultGeneric_Failure_ThrowsInvalidOperationException()
    {
        var result = Result.Failure<int>(new Error("Some error", "Something went wrong"));
        Assert.False(result.IsSuccess);
        Assert.Throws<InvalidOperationException>(() => result.Value);
    }

    [Fact]
    public void ResultGeneric_SuccessWithNullValue_ThrowsNullReferenceException()
    {
        var result = Result.Success<string?>(null);
        
        Assert.True(result.IsSuccess);
        Assert.Throws<NullReferenceException>(() => result.Value);
    }

    [Fact]
    public void ResultGeneric_ImplicitConversionToResult_Success_ReturnsResult()
    {
        Result<int> result = 42;
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }
}
