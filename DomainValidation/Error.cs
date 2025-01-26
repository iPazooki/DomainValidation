using System.Runtime.CompilerServices;

namespace DomainValidation;

/// <summary>
/// Represents an error that occurred during the execution of a program.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Message">The error message.</param>
/// <param name="LineNumber">The line number where the error occurred. Default is 0.</param>
/// <param name="MemberName">The name of the member where the error occurred. Default is an empty string.</param>
/// <param name="FilePath">The file path where the error occurred. Default is an empty string.</param>
public record Error(
    string Code,
    string Message,
    [CallerLineNumber] int LineNumber = 0,
    [CallerMemberName] string MemberName = "",
    [CallerFilePath] string FilePath = "")
{
    /// <summary>
    /// Represents an instance of the <see cref="Error"/> class that indicates no error occurred.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);
}