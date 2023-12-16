using DiffEngine;
using StudioLE.Diagnostics;
using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify;

/// <summary>
/// Methods to help verify test results using <see cref="IVerifier{TValue}"/>.
/// </summary>
public static class VerifierHelpers
{
    /// <summary>
    /// Verify <paramref name="actual"/> matches the content of a verified file.
    /// </summary>
    public static async Task<bool> Execute<T>(
        this IVerifier<T> verifier,
        IContext context,
        T actual)
    {
        string receivedPath = verifier.GetVerifyPath(context, ".received");
        string verifiedPath = verifier.GetVerifyPath(context, ".verified");
        await verifier.Writer.Write(receivedPath, actual);
        if (!File.Exists(verifiedPath))
            File.Create(verifiedPath).Dispose();
        VerifyFile[] files =
        {
            new("Verified", verifiedPath),
            new("Received", receivedPath)
        };
        IReadOnlyCollection<string> errors = await verifier.Differ.Execute(files);
        if (!errors.Any())
            return true;
        await Diff(context, verifiedPath, receivedPath);
        string message = string.Join(Environment.NewLine, errors.Prepend("Actual results did not match the verified results:"));
        context.OnFailure(message);
        return false;
    }

    /// <summary>
    /// Verify <paramref name="actual"/> matches <paramref name="expected"/>.
    /// </summary>
    public static async Task<bool> Execute<T>(
        this IVerifier<T> verifier,
        IContext context,
        T expected,
        T actual)
    {
        string expectedPath = verifier.GetTempPath(context, ".expected");
        string actualPath = verifier.GetTempPath(context, ".actual");
        await verifier.Writer.Write(expectedPath, expected);
        await verifier.Writer.Write(actualPath, actual);
        VerifyFile[] files =
        {
            new("Expected", expectedPath),
            new("Actual", actualPath)
        };
        IReadOnlyCollection<string> errors = await verifier.Differ.Execute(files);
        if (!errors.Any())
            return true;
        await Diff(context, expectedPath, actualPath);
        string message = string.Join(Environment.NewLine, errors.Prepend("Actual results did not match the expected results:"));
        context.OnFailure(message);
        return false;
    }

    private static string GetTempPath<T>(this IVerifier<T> verifier, IContext context, string suffix)
    {
        string directory = Path.GetTempPath();
        string fileName = context.GetEscapedName() + suffix + verifier.FileExtension;
        return Path.Combine(directory, fileName);
    }

    private static string GetVerifyPath<T>(this IVerifier<T> verifier, IContext context, string suffix)
    {
        string directory = Path.GetFullPath(Path.Combine("..", "..", "..", "Verify"));
        string fileName = context.GetEscapedName() + suffix + verifier.FileExtension;
        return Path.Combine(directory, fileName);
    }

    private static async Task Diff(IContext context, string expectedPath, string actualPath)
    {
        DiffTools.UseOrder(VerifyHelpers.DiffTools);
        if (context.IsDebugBuild)
            await DiffRunner.LaunchAsync(expectedPath, actualPath);
    }
}
