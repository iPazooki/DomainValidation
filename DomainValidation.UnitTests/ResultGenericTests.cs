namespace DomainValidation.UnitTests;

public class ResultGenericTests
{
    [Fact]
    public void ResultWithValue_Success_NoError_ReturnsValue()
    {
        var result = new Result<int>(42, true, Error.None);
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ImplicitConversionToResult_Success_ReturnsResult()
    {
        Result<int> result = 42;
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
        Assert.Equal(42, (int)result);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ResultWithValueAndMessage_Success_ReturnsValue()
    {
        var result = new Result<int>(42, true, "Operation successful");
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ResultWithValueAndMessage_Failure_ReturnsErrors()
    {
        var result = new Result<int>(0, false, "Operation failed");
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }

    [Fact]
    public void ResultWithValue_Success_ReturnsValue()
    {
        var result = new Result<int>(42, true);
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ResultWithValue_Failure_ReturnsErrors()
    {
        var result = new Result<int>(0, false);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void ImplicitConversionToIntResult_ReturnsResult()
    {
        var result = Result<int>.Success(1);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, (int)result);
    }

    [Fact]
    public void ResultWithErrorMessage_Failure_ReturnsIsSuccessFalse()
    {
        var result = Result<int>.Failure("Operation failed");
        Assert.False(result.IsSuccess);
        Assert.Equal(0, result.Value);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }
}