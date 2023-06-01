using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Strings;

namespace StudioLE.Verify.Yaml;

/// <inheritdoc cref="IVerifier{T}"/>
public sealed class YamlVerifier : IVerifier<object>
{
    /// <inheritdoc/>
    public IDiffer Differ => new StringDiffer();

    /// <inheritdoc/>
    public IFileWriter<object> Writer => new YamlFileWriter();
}
