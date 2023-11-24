using StudioLE.Serialization;
using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Strings;

namespace StudioLE.Verify.Serialization;

public sealed class SerializerWriter : IFileWriter<object>
{
    private readonly ISerializer _serializer;

    public SerializerWriter(ISerializer serializer)
    {
        _serializer = serializer;
    }

    /// <inheritdoc/>
    public Task Write(string path, object value)
    {
        using StringWriter stringWriter = new();
        _serializer.Serialize(stringWriter, value);
        string serialized = stringWriter.ToString();
        StringFileWriter fileWriter = new();
        return fileWriter.Write(path, serialized);
    }
}
