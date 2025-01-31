namespace DomainValidation.UnitTests;

public class ResultGenericTests
{
    [Fact]
    public void ResultWithValue_Success_NoError_ReturnsValue()
    {
        var result = new Result<int>(42, true, Error.None);
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
    }

    [Fact]
    public void ResultWithValue_Failure_ThrowsInvalidOperationException()
    {
        var result = new Result<int>(0, false, new Error("Some error code", "Something went wrong"));
        Assert.False(result.IsSuccess);
        Assert.Throws<InvalidOperationException>(() => result.Value);
    }
    
    [Fact]
    public void ResultWithValue_SuccessWithNullValue_ThrowsNullReferenceException()
    {
        var result = new Result<string>(null, true, Error.None);
        Assert.True(result.IsSuccess);
        Assert.Throws<NullReferenceException>(() => result.Value);
    }
    
    [Fact]
    public void ImplicitConversionToResult_Success_ReturnsResult()
    {
        Result<int> result = 42;
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
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
        Assert.False(result.Errors.Any());
    }
}
