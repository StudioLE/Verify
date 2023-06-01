using Newtonsoft.Json;
using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Json;

public sealed class JsonFileWriter : IFileWriter<object>
{
    /// <inheritdoc/>
    public Task Write(string path, object value)
    {
        JsonSerializerSettings settings = new()
        {
            Formatting = Formatting.Indented,
            Converters = JsonVerifier.Converters
        };
        using StreamWriter writer = new(path, false);
        JsonSerializer serializer = JsonSerializer.Create(settings);
        serializer.Serialize(writer, value);
        return Task.CompletedTask;
    }
}
