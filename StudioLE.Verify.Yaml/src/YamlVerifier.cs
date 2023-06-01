using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Strings;
using YamlDotNet.Serialization;

namespace StudioLE.Verify.Yaml;

/// <inheritdoc cref="IVerifier{T}"/>
public sealed class YamlVerifier : IVerifier<object>
{
    private static readonly ISerializer _defaultSerializer = new SerializerBuilder()
        .DisableAliases()
        .Build();

    /// <inheritdoc />
    public string FileExtension => ".yaml";

    /// <inheritdoc/>
    public IDiffer Differ => new StringDiffer();

    /// <inheritdoc/>
    public IFileWriter<object> Writer { get; }

    public YamlVerifier(ISerializer serializer)
    {
        Writer = new YamlFileWriter(serializer);
    }

    public YamlVerifier() : this(_defaultSerializer)
    {
    }
}
