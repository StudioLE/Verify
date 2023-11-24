using StudioLE.Results;
using StudioLE.Serialization;
using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Serialization;

/// <summary>
/// Methods to help verify test results using <see cref="IVerifier{TValue}"/>.
/// </summary>
public static class VerifyHelpers
{
    /// <summary>
    /// Verify <paramref name="actual"/> serialized with <paramref name="serializer"/>.
    /// </summary>
    public static async Task AsSerialized(this IVerify verify, object actual, ISerializer serializer)
    {
        SerializerVerifier verifier = new(serializer);
        IResult result = await verify.Execute(verifier, actual);
    }

    /// <summary>
    /// Verify <paramref name="actual"/> serialized with <paramref name="serializer"/>.
    /// </summary>
    public static async Task AsSerialized(this IVerify verify, object expected, object actual, ISerializer serializer)
    {
        SerializerVerifier verifier = new(serializer);
        IResult result = await verify.Execute(verifier, expected, actual);
    }
}
