using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Strings;

/// <inheritdoc cref="IVerifier{T}"/>
public sealed class StringVerifier : IVerifier<string>
{
    /// <inheritdoc/>
    public IDiffer Differ => new StringDiffer();

    /// <inheritdoc/>
    public IFileWriter<string> Writer => new StringFileWriter();
}
