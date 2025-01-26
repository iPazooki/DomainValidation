namespace DomainValidation;

/// <summary>
/// Represents the result of a domain validation that can either succeed or fail, with an optional value.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="isSuccess">A value indicating whether the operation succeeded.</param>
    /// <param name="errors">The errors that occurred, if any.</param>
    public Result(TValue? value, bool isSuccess, params Error[] errors) : base(isSuccess, errors) => _value = value;

    /// <summary>
    /// Gets the value of the result.
    /// </summary>
    /// <exception cref="NullReferenceException">Thrown when the value is null and the operation succeeded.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the operation did not succeed.</exception>
    public TValue Value => IsSuccess ? _value ?? throw new NullReferenceException() : throw new InvalidOperationException();

    /// <summary>
    /// Implicitly converts a value to a successful result.
    /// </summary>
    /// <param name="value">The value.</param>
    public static implicit operator Result<TValue>(TValue? value) => new(value, true, Error.None);
}