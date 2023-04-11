//
// The majority of the logic in VerifyContext is from Verify v16.7.0
// https://github.com/VerifyTests/Verify
//
// This specific VerifyContext class is therefore under their license:
// https://github.com/VerifyTests/Verify/blob/16.7.0/license.txt

using System.Reflection;
using System.Text;
using DiffEngine;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using StudioLE.Core.Results;
using StudioLE.Core.System;

namespace StudioLE.Verify.NUnit;

/// <summary>
/// The NUnit specific <see cref="IVerifyContext"/>.
/// </summary>
public class NUnitVerifyContext : IVerifyContext
{
    private string? _fileNamePrefix;
    private DirectoryInfo? _directory;

    /// <inheritdoc/>
    public string FileNamePrefix
    {
        get
        {
            if(_fileNamePrefix is null)
                Setup();
            return _fileNamePrefix!;
        }
    }

    /// <inheritdoc/>
    public DirectoryInfo Directory
    {
        get
        {
            if(_directory is null)
                Setup();
            return _directory!;
        }
    }

    public void Setup()
    {
        _fileNamePrefix = BuildFileNamePrefix(TestContext.CurrentContext);
        _directory = new(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "Verify"));
    }

    public void Reset()
    {
        _fileNamePrefix = null;
        _directory = null;
    }

    /// <inheritdoc/>
    public void OnResult(IResult result, FileInfo receivedFile, FileInfo verifiedFile)
    {
        if (result is Success)
            return;
        ITest test = GetTest(TestContext.CurrentContext);
        Assembly testAssembly = test.TypeInfo?.Type.Assembly ?? throw new("Failed to get assembly");
        if (testAssembly.IsDebugBuild())
            DiffRunner.LaunchAsync(receivedFile.FullName, verifiedFile.FullName);
        Assert.Fail(result.Errors.Prepend("Actual results did not match the verified results:").Join());
    }

    /// <summary>
    /// Determine the filename prefix to use for the <see cref="Verify"/> files.
    /// </summary>
    private static string BuildFileNamePrefix(TestContext context)
    {
        ITest test = GetTest(context);
        if (test.TypeInfo is null || test.Method is null)
            throw new($"Failed to {nameof(BuildFileNamePrefix)}. Test.TypeInfo was null.");
        if (test.TypeInfo == null || test.Method is null)
            throw new($"Failed to {nameof(BuildFileNamePrefix)}. Test.Method was null.");

        Type type = test.TypeInfo.Type;
        MethodInfo method = test.Method.MethodInfo;
        object?[] parameterValues = GetParameterValues();
        string uniqueness = "";

        string typeName = GetTypeName(type);
        string methodName = method.Name;
        string parameterText = GetParameterText(method, parameterValues);
        return $"{typeName}.{methodName}{parameterText}{uniqueness}";
    }

    /// <see href="https://github.com/VerifyTests/Verify/blob/16.7.0/src/Verify.NUnit/Verifier.cs#L22-L38"/>
    private static Test GetTest(TestContext context)
    {
        TestContext.TestAdapter adapter = context.Test;
        FieldInfo field = typeof(TestContext.TestAdapter)
                              .GetField("_test", BindingFlags.Instance | BindingFlags.NonPublic)
                          ?? throw new("Could not find field `_test` on TestContext.TestAdapter.");
        Test test = (Test)field.GetValue(adapter);
        return test;
    }

    /// <see href="https://github.com/VerifyTests/Verify/blob/16.7.0/src/Verify/Naming/ReflectionFileNameBuilder.cs"/>
    private static string GetTypeName(Type type)
    {
        return type.IsNested
            ? $"{type.ReflectedType!.Name}.{type.Name}"
            : type.Name;
    }

    /// <see href="https://github.com/VerifyTests/Verify/blob/16.7.0/src/Verify.NUnit/Verifier.cs#L22-L38"/>
    private static object?[] GetParameterValues()
    {
        TestContext context = TestContext.CurrentContext;
        TestContext.TestAdapter adapter = context.Test;
        return adapter.Arguments;
    }

    /// <see href="https://github.com/VerifyTests/Verify/blob/16.7.0/src/Verify/Naming/ReflectionFileNameBuilder.cs"/>
    private static string GetParameterText(MethodInfo method, object?[] parameterValues)
    {
        ParameterInfo[] methodParameters = method.GetParameters();
        if (!methodParameters.Any())
            return "";

        if (methodParameters.Length != parameterValues.Length)
            throw new($"The number of passed in parameters ({parameterValues.Length}) must match the number of parameters for the method ({methodParameters.Length}).");

        Dictionary<string, object?> dictionary = new();
        for (int index = 0; index < methodParameters.Length; index++)
        {
            ParameterInfo parameter = methodParameters[index];
            object? value = parameterValues[index];
            dictionary[parameter.Name!] = value;
        }

        string concat = ConcatParameterDictionary(dictionary);
        return $"_{concat}";
    }

    /// <see href="https://github.com/VerifyTests/Verify/blob/16.7.0/src/Verify/Naming/ParameterBuilder.cs"/>
    private static string ConcatParameterDictionary(Dictionary<string, object?> dictionary)
    {
        StringBuilder builder = new();
        foreach (KeyValuePair<string, object?> pair in dictionary)
            builder.Append($"{pair.Key}={pair.Value}_");
        builder.Length -= 1;
        return builder.ToString();
    }
}
