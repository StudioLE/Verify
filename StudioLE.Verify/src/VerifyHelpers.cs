using StudioLE.Diagnostics;
using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Files;
using StudioLE.Verify.Strings;

namespace StudioLE.Verify;

/// <summary>
/// Methods to help verify test results using <see cref="IVerifier{TValue}"/>.
/// </summary>
public static class VerifyHelpers
{
    /// <summary>
    /// Verify a string.
    /// </summary>
    public static async Task Verify(this IContext context, string actual)
    {
        StringVerifier verifier = new();
        await verifier.Execute(context, actual);
    }

    /// <summary>
    /// Verify a string.
    /// </summary>
    public static async Task Verify(this IContext context, string expected, string actual)
    {
        StringVerifier verifier = new();
        await verifier.Execute(context, expected, actual);
    }

    /// <summary>
    /// Verify a file.
    /// </summary>
    public static async Task Verify(this IContext context, FileInfo actual)
    {
        FileVerifier verifier = new()
        {
            FileExtension = actual.Extension
        };
        await verifier.Execute(context, actual);
    }
}
