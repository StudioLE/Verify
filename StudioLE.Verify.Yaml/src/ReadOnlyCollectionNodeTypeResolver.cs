using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace StudioLE.Verify.Yaml;

// https://github.com/aaubry/YamlDotNet/issues/236#issuecomment-632054372
public sealed class ReadOnlyCollectionNodeTypeResolver : INodeTypeResolver
{
    private static readonly IReadOnlyDictionary<Type, Type> _customGenericInterfaceImplementations = new Dictionary<Type, Type>
    {
        {typeof(IReadOnlyCollection<>), typeof(List<>)},
        {typeof(IReadOnlyList<>), typeof(List<>)},
        {typeof(IReadOnlyDictionary<,>), typeof(Dictionary<,>)}
    };

    public bool Resolve(NodeEvent? nodeEvent, ref Type type)
    {
        if (type.IsInterface
            && type.IsGenericType
            && _customGenericInterfaceImplementations.TryGetValue(type.GetGenericTypeDefinition(), out Type? concreteType))
        {
            type = concreteType.MakeGenericType(type.GetGenericArguments());
            return true;
        }
        return false;
    }
}
