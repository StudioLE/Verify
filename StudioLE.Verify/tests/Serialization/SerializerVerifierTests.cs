using NUnit.Framework;
using StudioLE.Core.Results;
using StudioLE.Core.System;
using StudioLE.Verify.Tests.Mock;
using StudioLE.Verify.Serialization;

namespace StudioLE.Verify.Tests.Serialization;

internal sealed class SerializationVerifierTests
{
    [Test]
    public async Task SerializerVerifier_IsValid()
    {
        // Arrange
        BBox3[] actual = SampleHelpers.GetValidSample();
        MockVerify verify = new("SerializationVerifier_Pass");
        SerializationVerifier verifier = new();

        // Act
        IResult result = await verify.Execute(verifier, actual);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, "Is Success");
        Assert.That(result.Errors, Is.Empty, "Errors");
    }

    [Test]
    public async Task SerializationVerifier_IsInvalid()
    {
        // Arrange
        BBox3[] actual = SampleHelpers.GetInvalidSample();
        MockVerify verify = new("SerializationVerifier_Fail");
        SerializationVerifier verifier = new();

        // Act
        IResult result = await verify.Execute(verifier, actual);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, Is.False, "Is Success");
        Assert.That(result.Errors, Is.Not.Empty, "Errors");
    }
}
