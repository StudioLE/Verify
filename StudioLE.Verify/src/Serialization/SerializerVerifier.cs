using StudioLE.Serialization;
using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Strings;

namespace StudioLE.Verify.Serialization;

/// <inheritdoc cref="IVerifier{T}"/>
public sealed class SerializerVerifier : IVerifier<object>
{
    private readonly ISerializer _serializer;

    /// <inheritdoc />
    public string FileExtension => _serializer.FileExtension;

    /// <inheritdoc/>
    public IDiffer Differ => new StringDiffer();

    /// <inheritdoc/>
    public IFileWriter<object> Writer { get; }

    /// <summary>
    /// DI constructor for <see cref="SerializerVerifier"/>.
    /// </summary>
    public SerializerVerifier(ISerializer serializer)
    {
        _serializer = serializer;
        Writer = new SerializerWriter(serializer);
    }
}
