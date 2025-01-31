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
    /// <param name="errors">The errors that occurred, if any.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <paramref name="isSuccess"/> is true and any error is not <see cref="Error.None"/>,
    /// or when <paramref name="isSuccess"/> is false and any error is <see cref="Error.None"/>.
    /// </exception>
    public Result(bool isSuccess, params Error[] errors)
    {
        switch (isSuccess)
        {
            case true when errors.Any(e => e != Error.None):
                throw new InvalidOperationException();
            case false when errors.Any(e => e == Error.None):
                throw new InvalidOperationException();
            default:
                IsSuccess = isSuccess;
                Errors = errors.All(e => !string.IsNullOrEmpty(e.Code) && !string.IsNullOrEmpty(e.Message))
                    ? errors
                    : [];
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">A value indicating whether the operation succeeded.</param>
    /// <param name="message">The message associated with the result.</param>
    public Result(bool isSuccess, string? message)
    {
        switch (isSuccess)
        {
            case true:
                IsSuccess = isSuccess;
                Errors = [];
                break;
            case false:
                IsSuccess = isSuccess;
                Errors = !string.IsNullOrEmpty(message) ? [new Error(message)] : [];
                break;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the errors that occurred, if any.
    /// </summary>
    public IEnumerable<Error> Errors { get; }

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
    /// <param name="errors">The errors that occurred.</param>
    /// <returns>A new instance of the <see cref="Result"/> class that indicates failure.</returns>
    public static Result Failure(params Error[] errors) => new(false, errors);

    public static Result Failure(string message) => new(false, message);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class that indicates failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="errors">The errors that occurred.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class that indicates failure.</returns>
    public static Result<TValue> Failure<TValue>(params Error[] errors) => new(default, false, errors);
}