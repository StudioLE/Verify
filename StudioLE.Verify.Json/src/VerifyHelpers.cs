using Newtonsoft.Json;
using StudioLE.Results;
using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Json;

/// <summary>
/// Methods to help verify test results using <see cref="IVerifier{TValue}"/>.
/// </summary>
public static class VerifyHelpers
{
    /// <summary>
    /// Verify <paramref name="actual"/> as JSON.
    /// </summary>
    public static async Task AsJson(this IVerify verify, object actual, JsonSerializer? serializer = null)
    {
        JsonVerifier verifier = serializer is null
            ? new()
            : new(serializer);
        IResult result = await verify.Execute(verifier, actual);
    }

    /// <summary>
    /// Verify <paramref name="actual"/> as JSON.
    /// </summary>
    public static async Task AsJson(this IVerify verify, object expected, object actual, JsonSerializer? serializer = null)
    {
        JsonVerifier verifier = serializer is null
            ? new()
            : new(serializer);
        IResult result = await verify.Execute(verifier, expected, actual);
    }
}
