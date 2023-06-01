using NUnit.Framework;
using StudioLE.Core.Results;
using StudioLE.Core.System;
using StudioLE.Verify.Tests.Mock;
using StudioLE.Verify.Yaml;

namespace StudioLE.Verify.Tests.Yaml;

internal sealed class YamlVerifierTests
{
    [Test]
    public async Task YamlVerifier_IsValid()
    {
        // Arrange
        BBox3[] actual = SampleHelpers.GetValidSample();
        MockVerify verify = new("YamlVerifier_Pass");
        YamlVerifier verifier = new();

        // Act
        IResult result = await verify.Execute(verifier, actual);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, "Is Success");
        Assert.That(result.Errors, Is.Empty, "Errors");
    }

    [Test]
    public async Task YamlVerifier_IsInvalid()
    {
        // Arrange
        BBox3[] actual = SampleHelpers.GetInvalidSample();
        MockVerify verify = new("YamlVerifier_Fail");
        YamlVerifier verifier = new();

        // Act
        IResult result = await verify.Execute(verifier, actual);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, Is.False, "Is Success");
        Assert.That(result.Errors, Is.Not.Empty, "Errors");
    }
}
