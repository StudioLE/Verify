using NUnit.Framework;
using StudioLE.Extensions.System;
using StudioLE.Results;
using StudioLE.Verify.Json;
using StudioLE.Verify.Tests.Mock;

namespace StudioLE.Verify.Tests.Json;

internal sealed class JsonVerifierTests
{
    [Test]
    public async Task JsonVerifier_IsValid()
    {
        // Arrange
        BBox3[] actual = SampleHelpers.GetValidSample();
        MockVerify verify = new("JsonVerifier_Pass");
        JsonVerifier verifier = new();

        // Act
        IResult result = await verify.Execute(verifier, actual);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, "Is Success");
        Assert.That(result.Errors, Is.Empty, "Errors");
    }

    [Test]
    public async Task JsonVerifier_IsInvalid()
    {
        // Arrange
        BBox3[] actual = SampleHelpers.GetInvalidSample();
        MockVerify verify = new("JsonVerifier_Fail");
        JsonVerifier verifier = new();

        // Act
        IResult result = await verify.Execute(verifier, actual);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, Is.False, "Is Success");
        Assert.That(result.Errors, Is.Not.Empty, "Errors");
    }
}
