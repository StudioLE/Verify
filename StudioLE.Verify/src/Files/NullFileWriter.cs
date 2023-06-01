using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Files;

public sealed class NullFileWriter<TValue> : IFileWriter<TValue>
{
    /// <inheritdoc/>
    public Task Write(string path, TValue value)
    {
        return Task.CompletedTask;
    }
}
