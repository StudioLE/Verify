using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace StudioLE.Verify.Yaml;

internal class YamlStringConverter : IYamlTypeConverter
{
    /// <summary>
    /// Should <c>CRLF</c> be replaced with <c>LF</c> in multiline strings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public bool ReplaceCRLFWithLF { get; set; } = true;

    /// <summary>
    /// The default scalar style for multiline strings.
    /// </summary>
    public ScalarStyle DefaultScalarStyleForMultilineStrings { get; set; } = ScalarStyle.Literal;

    /// <inheritdoc />
    public bool Accepts(Type type)
    {
        return type == typeof(string);
    }

    /// <inheritdoc />
    public object ReadYaml(IParser parser, Type type)
    {
        Scalar scalar = parser.Consume<Scalar>();
        return scalar.Value;
    }

    /// <inheritdoc />
    public void WriteYaml(IEmitter emitter, object? obj, Type type)
    {
        if (obj is not string value)
            throw new($"Failed to write YAML. Expected value to be a {typeof(string)}.");
        if(ReplaceCRLFWithLF)
            value = value.Replace("\r\n", "\n");
        ScalarStyle style = ScalarStyle.Any;
        if (IsMultiLineString(value))
            style = DefaultScalarStyleForMultilineStrings;
        Scalar scalar = CreateScalar(value, style);
        emitter.Emit(scalar);
    }

    private static bool IsMultiLineString(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return false;
        char[] lineBreakCharacters = ['\r', '\n', '\x85', '\x2028', '\x2029'];
        return value.IndexOfAny(lineBreakCharacters) >= 0;
    }

    private static Scalar CreateScalar(string value, ScalarStyle style)
    {
        return new(AnchorName.Empty, TagName.Empty, value, style, true, true, Mark.Empty, Mark.Empty);
    }
}
