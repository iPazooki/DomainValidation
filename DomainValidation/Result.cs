namespace DomainValidation;

/// <summary>
/// Represents the result of a domain validation that can either succeed or fail.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">A value indicating whether the operation succeeded.</param>
    /// <param name="error">The error that occurred, if any.</param>
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;

        Error = error;
    }

    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets the error that occurred, if any.
    /// </summary>
    public Error Error { get; init; }

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class that indicates success.
    /// </summary>
    /// <returns>A new instance of the <see cref="Result"/> class that indicates success.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class that indicates success.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class that indicates success.</returns>
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class that indicates failure.
    /// </summary>
    /// <param name="error">The error that occurred.</param>
    /// <returns>A new instance of the <see cref="Result"/> class that indicates failure.</returns>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class that indicates failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="error">The error that occurred.</param>
    /// <param name="value">The value.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class that indicates failure.</returns>
    public static Result<TValue> Failure<TValue>(Error error, TValue? value = default) => new(value, false, error);
}
