using System.Reflection;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace StudioLE.Diagnostics.NUnit;

public static class NUnitContextHelpers
{
    internal static string CreateResultsSummary(TestContext context)
    {
        return CreateResultsSummary(context.Result);
    }

    internal static string CreateResultsSummary(TestContext.ResultAdapter results)
    {
        StringBuilder builder = new();
        if (results.FailCount == 0)
            builder.Append($"All {results.PassCount} passed");
        else if (results.PassCount == 0)
            builder.Append($"All {results.FailCount} failed");
        else
            builder.Append($"{results.FailCount} of {results.PassCount + results.FailCount} failed");
        if (results.SkipCount > 0)
            builder.Append($", {results.SkipCount} skipped");
        return builder.ToString();
    }

    /// <see href="https://github.com/VerifyTests/Verify/blob/16.7.0/src/Verify.NUnit/Verifier.cs#L22-L38"/>
    internal static Test GetTest(TestContext context)
    {
        TestContext.TestAdapter adapter = context.Test;
        FieldInfo field = typeof(TestContext.TestAdapter)
                              .GetField("_test", BindingFlags.Instance | BindingFlags.NonPublic)
                          ?? throw new("Could not find field `_test` on TestContext.TestAdapter.");
        Test test = (Test)field.GetValue(adapter);
        return test;
    }

    /// <see href="https://github.com/VerifyTests/Verify/blob/16.7.0/src/Verify/Naming/ReflectionFileNameBuilder.cs"/>
    internal static string GetTypeName(Type type)
    {
        return type.IsNested
            ? $"{type.ReflectedType!.Name}.{type.Name}"
            : type.Name;
    }

    /// <see href="https://github.com/VerifyTests/Verify/blob/16.7.0/src/Verify/Naming/ReflectionFileNameBuilder.cs"/>
    internal static Dictionary<string, object?> GetParameterDictionary(MethodInfo method, ITest test)
    {
        object?[] parameterValues = test.Arguments;
        ParameterInfo[] methodParameters = method.GetParameters();
        if (!methodParameters.Any())
            return new();
        if (methodParameters.Length != parameterValues.Length)
            throw new($"The number of passed in parameters ({parameterValues.Length}) must match the number of parameters for the method ({methodParameters.Length}).");
        Dictionary<string, object?> dictionary = new();
        for (int index = 0; index < methodParameters.Length; index++)
        {
            ParameterInfo parameter = methodParameters[index];
            object? value = parameterValues[index];
            dictionary[parameter.Name!] = value;
        }
        return dictionary;
    }
}
