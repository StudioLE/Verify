using YamlDotNet.Serialization;

namespace StudioLE.Verify.Yaml;

public static class YamlHelpers
{
    public static IDeserializer CreateDeserializer()
    {
        return new DeserializerBuilder()
            .WithNodeTypeResolver(new ReadOnlyCollectionNodeTypeResolver())
            .IgnoreUnmatchedProperties()
            .Build();
    }

    public static ISerializer CreateSerializer()
    {
        return new SerializerBuilder()
            .DisableAliases()
            .Build();
    }
}
