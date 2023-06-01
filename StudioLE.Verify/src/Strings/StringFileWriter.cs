using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Strings;

public sealed class StringFileWriter : IFileWriter<string>
{
    /// <inheritdoc/>
    public async Task Write(string path, string value)
    {
        using StreamWriter writer = new(path, false);
        await writer.WriteAsync(value);
    }
}
