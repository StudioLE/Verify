using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Strings;

namespace StudioLE.Verify.Json;

/// <inheritdoc cref="IVerifier{T}"/>
public sealed class JsonVerifier : IVerifier<object>
{
    private const int DecimalPlaces = 5;
    public static readonly JsonConverter[] Converters =
    {
        new StringEnumConverter(),
        new DoubleConverter(DecimalPlaces)
    };

    /// <inheritdoc />
    public string FileExtension => ".json";

    /// <inheritdoc/>
    public IDiffer Differ => new StringDiffer();

    /// <inheritdoc/>
    public IFileWriter<object> Writer => new JsonFileWriter();
}
