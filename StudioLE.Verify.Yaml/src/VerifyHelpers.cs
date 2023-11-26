using StudioLE.Diagnostics;
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
    public static async Task VerifyAsYaml(this IContext context, object actual, ISerializer? serializer = null)
    {
        YamlVerifier verifier = serializer is null
            ? new()
            : new(serializer);
        await verifier.Execute(context, actual);
    }

    /// <summary>
    /// Verify <paramref name="actual"/> as YAML.
    /// </summary>
    public static async Task VerifyAsYaml(this IContext context, object expected, object actual, ISerializer? serializer = null)
    {
        YamlVerifier verifier = serializer is null
            ? new()
            : new(serializer);
        await verifier.Execute(context, expected, actual);
    }
}
