using StudioLE.Diagnostics;
using StudioLE.Serialization;
using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Serialization;

/// <summary>
/// Methods to help context test results using <see cref="IVerifier{TValue}"/>.
/// </summary>
public static class VerifyHelpers
{
    /// <summary>
    /// Verify <paramref name="actual"/> serialized with <paramref name="serializer"/>.
    /// </summary>
    public static async Task VerifyAsSerialized(this IContext context, object actual, ISerializer serializer)
    {
        SerializerVerifier verifier = new(serializer);
        await verifier.Execute(context, actual);
    }

    /// <summary>
    /// Verify <paramref name="actual"/> serialized with <paramref name="serializer"/>.
    /// </summary>
    public static async Task VerifyAsSerialized(this IContext context, object expected, object actual, ISerializer serializer)
    {
        SerializerVerifier verifier = new(serializer);
        await verifier.Execute(context, expected, actual);
    }
}
