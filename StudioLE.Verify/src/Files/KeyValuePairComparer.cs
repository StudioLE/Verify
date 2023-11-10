namespace StudioLE.Verify.Files;

/// <summary>
/// Compare the <typeparamref name="TValue"/>.
/// </summary>
public sealed class KeyValuePairValueComparer<TKey, TValue> : IEqualityComparer<KeyValuePair<TKey, TValue>>
{
    /// <inheritdoc cref="KeyValuePairValueComparer{TKey,TValue}"/>
    public bool Equals(KeyValuePair<TKey, TValue> first, KeyValuePair<TKey, TValue> second)
    {
        if (first.GetType() != second.GetType())
            return false;
        return first.Value?.Equals(second.Value) ?? false;
    }

    /// <inheritdoc cref="KeyValuePairValueComparer{TKey,TValue}"/>
    public int GetHashCode(KeyValuePair<TKey, TValue> obj)
    {
        return obj.Value?.GetHashCode() ?? obj.GetHashCode();
    }
}
