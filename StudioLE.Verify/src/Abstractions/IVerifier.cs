namespace StudioLE.Verify.Abstractions;

/// <summary>
/// A lightweight alternative to <see href="https://github.com/VerifyTests/Verify"/> developed specifically with Elements in mind.
/// </summary>
/// <remarks>
/// Verifier is completely engine agnostic. It has no dependency on NUnit.
/// </remarks>
public interface IVerifier<in TValue>
{
    public string FileExtension { get; }

    public IDiffer Differ { get; }

    public IFileWriter<TValue> Writer { get; }
}
