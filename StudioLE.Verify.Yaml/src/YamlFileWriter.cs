using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Strings;
using YamlDotNet.Serialization;

namespace StudioLE.Verify.Yaml;

public sealed class YamlFileWriter : IFileWriter<object>
{
    private readonly ISerializer _serializer;

    public YamlFileWriter(ISerializer serializer)
    {
        _serializer = serializer;
    }

    /// <inheritdoc/>
    public Task Write(string path, object value)
    {
        string yaml = _serializer.Serialize(value);
        StringFileWriter writer = new();
        return writer.Write(path, yaml);
    }
}
