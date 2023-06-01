using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Files;

public sealed class FileInfoFileWriter : IFileWriter<FileInfo>
{
    /// <inheritdoc/>
    public Task Write(string path, FileInfo file)
    {
        file.CopyTo(path, overwrite: true);
        return Task.CompletedTask;
    }
}
