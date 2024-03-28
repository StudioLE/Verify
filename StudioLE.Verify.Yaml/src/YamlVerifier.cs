using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Strings;
using YamlDotNet.Serialization;

namespace StudioLE.Verify.Yaml;

/// <inheritdoc cref="IVerifier{T}"/>
public sealed class YamlVerifier : IVerifier<object>
{
    private static readonly ISerializer _defaultSerializer = new SerializerBuilder()
        .DisableAliases()
        .WithTypeConverter(new YamlStringConverter())
        .Build();

    /// <inheritdoc/>
    public string FileExtension => ".yaml";

    /// <inheritdoc/>
    public IDiffer Differ => new StringDiffer();

    /// <inheritdoc/>
    public IFileWriter<object> Writer { get; }

    /// <summary>
    /// DI constructor for <see cref="YamlVerifier"/>.
    /// </summary>
    public YamlVerifier(ISerializer serializer)
    {
        Writer = new YamlFileWriter(serializer);
    }

    /// <summary>
    /// Creates a new instance of <see cref="YamlVerifier"/>.
    /// </summary>
    public YamlVerifier() : this(_defaultSerializer)
    {
    }
}
