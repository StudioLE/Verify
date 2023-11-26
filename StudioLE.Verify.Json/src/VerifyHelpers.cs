using Newtonsoft.Json;
using StudioLE.Diagnostics;
using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Json;

/// <summary>
/// Methods to help context test results using <see cref="IVerifier{TValue}"/>.
/// </summary>
public static class VerifyHelpers
{
    /// <summary>
    /// Verify <paramref name="actual"/> as JSON.
    /// </summary>
    public static async Task VerifyAsJson(this IContext context, object actual, JsonSerializer? serializer = null)
    {
        JsonVerifier verifier = serializer is null
            ? new()
            : new(serializer);
        await verifier.Execute(context, actual);
    }

    /// <summary>
    /// Verify <paramref name="actual"/> as JSON.
    /// </summary>
    public static async Task VerifyAsJson(this IContext context, object expected, object actual, JsonSerializer? serializer = null)
    {
        JsonVerifier verifier = serializer is null
            ? new()
            : new(serializer);
        await verifier.Execute(context, expected, actual);
    }
}
