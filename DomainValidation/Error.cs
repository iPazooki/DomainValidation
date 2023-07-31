using System.Runtime.CompilerServices;

namespace DomainValidation;

/// <summary>
/// Represents an error that occurred during the execution of a program.
/// </summary>
public record Error(
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    string Code,
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    string Message,
    /// <summary>
    /// Gets or sets the line number where the error occurred.
    /// </summary>
    [CallerLineNumber] int LineNumber = 0,
    /// <summary>
    /// Gets or sets the name of the member (method) where the error occurred.
    /// </summary>
    [CallerMemberName] string MemberName = "",
    /// <summary>
    /// Gets or sets the file path of the source file where the error occurred.
    /// </summary>
    [CallerFilePath] string FilePath = "")
{
    /// <summary>
    /// Represents an instance of the <see cref="Error"/> class that indicates no error occurred.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);
}
