using StudioLE.Core.Results;
using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Yaml;

/// <summary>
/// Methods to help verify test results using <see cref="IVerifier{TValue}"/>.
/// </summary>
public static class VerifyHelpers
{
    /// <summary>
    /// Verify <paramref name="actual"/> as YAML.
    /// </summary>
    public static async Task AsYaml(this IVerify verify, object actual)
    {
        YamlVerifier verifier = new();
        IResult result = await verify.Execute(verifier, actual, ".yaml");
    }

    /// <summary>
    /// Verify <paramref name="actual"/> as YAML.
    /// </summary>
    public static async Task AsJson(this IVerify verify, object expected, object actual)
    {
        YamlVerifier verifier = new();
        IResult result = await verify.Execute(verifier, expected, actual, ".yaml");
    }
}
