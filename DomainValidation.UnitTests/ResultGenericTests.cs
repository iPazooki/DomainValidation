using System.Text.Json;

namespace DomainValidation.UnitTests;

public class ResultGenericTests
{
    [Fact]
    public void ResultWithValue_Success_NoError_ReturnsValue()
    {
        Result<int> result = new Result<int>(42, true);
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
        Result<int> result = new Result<int>(42, true, "Operation successful");
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ResultWithValueAndMessage_Failure_ReturnsErrors()
    {
        Result<int> result = new Result<int>(0, false, "Operation failed");
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Errors);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }

    [Fact]
    public void ResultWithValue_Success_ReturnsValue()
    {
        Result<int> result = new Result<int>(42, true);
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ResultWithValue_Failure_ReturnsErrors()
    {
        Result<int> result = new Result<int>(0, false);
        Assert.False(result.IsSuccess);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void ImplicitConversionToIntResult_ReturnsResult()
    {
        Result<int> result = Result<int>.Success(1);
        Assert.True(result.IsSuccess);
        Assert.Equal(1, (int)result);
    }

    [Fact]
    public void ResultWithErrorMessage_Failure_ReturnsIsSuccessFalse()
    {
        Result<int> result = Result<int>.Failure("Operation failed");
        Assert.False(result.IsSuccess);
        Assert.Equal(0, result.Value);
        Assert.NotNull(result.Errors);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }
    
    [Fact]
    public void ResultWithErrorList_Failure_ReturnsIsSuccessFalse()
    {
        List<Error> errors = [new("Operation failed")];
        Result<int> result = Result<int>.Failure(errors.ToArray());
        Assert.False(result.IsSuccess);
        Assert.Equal(0, result.Value);
        Assert.NotNull(result.Errors);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }
    
    [Fact]
    public void SerializeResult_Success_ReturnsJson()
    {
        Result<int> result = new Result<int>(42, true);
        string json = JsonSerializer.Serialize(result);
        Assert.Contains("\"IsSuccess\":true", json);
        Assert.Contains("\"Value\":42", json);
        Assert.Contains("\"Errors\":[]", json);
    }
    
    [Fact]
    public void SerializeResult_SuccessNoError_ReturnsJson()
    {
        Result<int> result = new Result<int>(42, true);
        string json = JsonSerializer.Serialize(result);
        Assert.Contains("\"IsSuccess\":true", json);
        Assert.Contains("\"Value\":42", json);
        Assert.Contains("\"Errors\":[]", json);
    }

    [Fact]
    public void DeserializeResult_Success_ReturnsResult()
    {
        string json = "{\"Value\":42,\"IsSuccess\":true}";
        Result<int>? result = JsonSerializer.Deserialize<Result<int>>(json);
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void SerializeResult_Failure_ReturnsJson()
    {
        Result<int> result = new Result<int>(1, false, new Error("Operation failed"));
        string json = JsonSerializer.Serialize(result);
        Assert.Contains("\"IsSuccess\":false", json);
        Assert.Contains("\"Value\":1", json);
        Assert.Contains("\"Errors\":[{\"Message\":\"Operation failed\"", json);
    }

    [Fact]
    public void DeserializeResult_Failure_ReturnsResult()
    {
        string json = "{\"Value\":1,\"IsSuccess\":false,\"Errors\":[{\"Message\":\"Operation failed\"}]}";
        Result<int>? result = JsonSerializer.Deserialize<Result<int>>(json);
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(1, result.Value);
        Assert.NotNull(result.Errors);
        Assert.Contains(result.Errors, e => e.Message == "Operation failed");
    }
}