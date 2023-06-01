using StudioLE.Core.Results;
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
    /// Verify <paramref name="actual"/>.
    /// </summary>
    public static async Task<IResult> Execute<T>(
        this IVerify verify,
        IVerifier<T> verifier,
        T actual,
        string fileExtension = ".txt")
    {
        string receivedPath = verify.GetFilePath(".received" + fileExtension);
        string verifiedPath = verify.GetFilePath(".verified" + fileExtension);
        await verifier.Writer.Write(receivedPath, actual);
        VerifyFile[] files =
        {
            new("Verified", verifiedPath),
            new("Received", receivedPath)
        };
        IResult result = await verifier.Differ.Execute(files);
        verify.OnResult(result, receivedPath, verifiedPath);
        return result;
    }

    /// <summary>
    /// Verify <paramref name="actual"/> matches <paramref name="expected"/>.
    /// </summary>
    public static async Task<IResult> Execute<T>(
        this IVerify verify,
        IVerifier<T> verifier,
        T expected,
        T actual,
        string fileExtension = ".txt")
    {
        string expectedPath = Path.GetTempFileName() + ".expected" + fileExtension;
        string actualPath = Path.GetTempFileName() + ".actual" + fileExtension;
        await verifier.Writer.Write(expectedPath, expected);
        await verifier.Writer.Write(actualPath, actual);
        VerifyFile[] files =
        {
            new("Expected", expectedPath),
            new("Actual", actualPath)
        };
        IResult result = await verifier.Differ.Execute(files);
        verify.OnResult(result, actualPath, expectedPath);
        return result;
    }

    /// <summary>
    /// Verify a string.
    /// </summary>
    public static async Task String(this IVerify verify, string actual)
    {
        StringVerifier verifier = new();
        IResult result = await verify.Execute(verifier, actual);
    }

    /// <summary>
    /// Verify a string.
    /// </summary>
    public static async Task String(this IVerify verify, string expected, string actual)
    {
        StringVerifier verifier = new();
        IResult result = await verify.Execute(verifier, expected, actual);
    }

    /// <summary>
    /// Verify a file.
    /// </summary>
    public static async Task File(this IVerify verify, FileInfo actual)
    {
        FileVerifier verifier = new();
        IResult result = await verify.Execute(verifier, actual, actual.Extension);
    }
}
