using Newtonsoft.Json;
using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Json;

public sealed class JsonFileWriter : IFileWriter<object>
{
    private readonly JsonSerializer _serializer;

    public JsonFileWriter(JsonSerializer serializer)
    {
        _serializer = serializer;
    }

    /// <inheritdoc/>
    public Task Write(string path, object value)
    {
        using StreamWriter writer = new(path, false);
        _serializer.Serialize(writer, value);
        return Task.CompletedTask;
    }
}
