using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using StudioLE.Extensions.System.Reflection;

namespace StudioLE.Diagnostics.NUnit;

public class NUnitContext : IContext
{
    private string? _id;
    private static string _typeName = null!;
    private static string _methodName = null!;
    private static Dictionary<string, object?> _parameterDictionary = null!;

    /// <inheritdoc />
    public bool IsDebugBuild { get; } = AssemblyHelpers
        .GetCallingAssemblies()
        .ElementAt(1)
        .IsDebugBuild();

    private void UpdateIfRequired()
    {
        if (_id != TestContext.CurrentContext.Test.ID)
            Update();
    }

    private void Update()
    {
        ITest test = NUnitContextHelpers.GetTest(TestContext.CurrentContext);
        Type type = test.TypeInfo?.Type ?? throw new($"Failed to update {nameof(NUnitContext)}. Test.TypeInfo was null.");
        MethodInfo method = test.Method?.MethodInfo ?? throw new($"Failed to update {nameof(NUnitContext)}. Test.Method was null.");
        _id = test.Id;
        _typeName = NUnitContextHelpers.GetTypeName(type);
        _methodName = method.Name;
        _parameterDictionary = NUnitContextHelpers.GetParameterDictionary(method, test);
    }

    /// <inheritdoc/>
    public string GetShortName()
    {
        UpdateIfRequired();
        if (!_parameterDictionary.Any())
            return _methodName;
        string parameters = string.Join(", ", _parameterDictionary.Values);
        return $"{_methodName} ({parameters})";
    }

    /// <inheritdoc/>
    public string GetLongName()
    {
        UpdateIfRequired();
        if (!_parameterDictionary.Any())
            return $"{_typeName} {_methodName}";
        string parameters = string.Join(", ", _parameterDictionary.Select(pair => $"{pair.Key}: {pair.Value}"));
        return $"{_typeName} {_methodName} {parameters}";
    }

    /// <inheritdoc/>
    public string GetDescription()
    {
        UpdateIfRequired();
        string parameters = string.Join(Environment.NewLine, _parameterDictionary.Select(pair => $"{pair.Key}: {pair.Value}"));
        string[] items = {
            _typeName,
            _methodName,
            parameters
        };
        return string.Join(Environment.NewLine, items);
    }

    /// <inheritdoc/>
    public string GetEscapedName()
    {
        UpdateIfRequired();
        if (!_parameterDictionary.Any())
            return $"{_typeName}.{_methodName}";
        string parameters = string.Join("_", _parameterDictionary.Select(pair => $"{pair.Key}={pair.Value}"));
        return $"{_typeName}.{_methodName}_{parameters}";
    }

    /// <inheritdoc/>
    public Metadata GetMetadata()
    {
        UpdateIfRequired();
        string title = GetShortName();
        string description = GetDescription();
        string summary = NUnitContextHelpers.CreateResultsSummary(TestContext.CurrentContext);
        return new()
        {
            Title = title,
            Description = description + Environment.NewLine + summary
        };
    }

    /// <inheritdoc />
    public void OnFailure(string message)
    {
        Assert.Fail(message);
    }
}
