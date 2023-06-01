using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Files;

/// <inheritdoc cref="IVerifier{T}"/>
public sealed class FileVerifier : IVerifier<FileInfo>
{
    /// <inheritdoc />
    public IDiffer Differ => new FileDiffer();

    /// <inheritdoc />
    public IFileWriter<FileInfo> Writer => new FileInfoFileWriter();
}
