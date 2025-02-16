namespace DomainValidation;

/// <summary>
/// Represents the result of a domain validation that can either succeed or fail, with an optional value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public sealed class Result<TValue> : Result
{
    // Parameterless constructor for serialization
    public Result() { }
    
    /// <summary>
    /// Gets the value associated with the result.
    /// </summary>
    public TValue? Value { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="isSuccess">A value indicating whether the operation succeeded.</param>
    /// <param name="errors">The errors that occurred, if any.</param>
    public Result(TValue? value, bool isSuccess, params Error[]? errors) : base(isSuccess, errors) => Value = value;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="isSuccess">A value indicating whether the operation succeeded.</param>
    public Result(TValue? value, bool isSuccess) : base(isSuccess, string.Empty) => Value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="isSuccess">A value indicating whether the operation succeeded.</param>
    /// <param name="message">The message associated with the result.</param>
    public Result(TValue? value, bool isSuccess, string message) : base(isSuccess, message) => Value = value;

    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class that indicates success.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class that indicates success.</returns>
    public static Result<TValue> Success(TValue value) => new(value, true);
    
    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class that indicates failure.
    /// </summary>
    /// <param name="message">The error message associated with the failure.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class that indicates failure.</returns>
    public new static Result<TValue> Failure(string message) => new(default, false, message);
    
    /// <summary>
    /// Creates a new instance of the <see cref="Result{TValue}"/> class that indicates failure.
    /// </summary>
    /// <param name="errors">The errors that occurred.</param>
    /// <returns>A new instance of the <see cref="Result{TValue}"/> class that indicates failure.</returns>
    public new static Result<TValue> Failure(params Error[]? errors) => new(default, false, errors);

    /// <summary>
    /// Implicitly converts a value to a successful result.
    /// </summary>
    /// <param name="value">The value.</param>
    public static implicit operator Result<TValue>(TValue? value) => new(value, true);
    
    /// <summary>
    /// Implicitly converts a <see cref="Result{TValue}"/> to its value.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    /// <returns>The value of the result.</returns>
    public static implicit operator TValue?(Result<TValue> result) => result.Value;
}