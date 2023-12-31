﻿namespace DomainValidation;

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
    protected internal Result(bool isSuccess, params Error[] errors)
    {
        if (isSuccess && errors.Any(e => e != Error.None))
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && errors.Any(e => e == Error.None))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;

        Errors = errors;
    }

    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets the errors that occurred, if any.
    /// </summary>
    public IReadOnlyCollection<Error> Errors { get; init; }

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

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class that indicates failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="errors">The errors that occurred.</param>
    /// <param name="value">The value.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class that indicates failure.</returns>
    public static Result<TValue> Failure<TValue>(params Error[] errors) => new(default, false, errors);
}
