using StudioLE.Core.Results;
using StudioLE.Verify.Abstractions;
using YamlDotNet.Serialization;

namespace StudioLE.Verify.Yaml;

/// <summary>
/// Methods to help verify test results using <see cref="IVerifier{TValue}"/>.
/// </summary>
public static class VerifyHelpers
{
    /// <summary>
    /// Verify <paramref name="actual"/> as YAML.
    /// </summary>
    public static async Task AsYaml(this IVerify verify, object actual, ISerializer? serializer = null)
    {
        YamlVerifier verifier = serializer is null
            ? new()
            : new(serializer);
        IResult result = await verify.Execute(verifier, actual);
    }

    /// <summary>
    /// Verify <paramref name="actual"/> as YAML.
    /// </summary>
    public static async Task AsYaml(this IVerify verify, object expected, object actual, ISerializer? serializer = null)
    {
        YamlVerifier verifier = new();
        IResult result = await verify.Execute(verifier, expected, actual);
    }
}
