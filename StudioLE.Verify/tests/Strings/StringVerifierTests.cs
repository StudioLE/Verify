using NUnit.Framework;
using StudioLE.Extensions.System;
using StudioLE.Results;
using StudioLE.Verify.Strings;
using StudioLE.Verify.Tests.Mock;

namespace StudioLE.Verify.Tests.Strings;

internal sealed class StringVerifierTests
{
    [Test]
    public async Task StringVerifier_IsValid()
    {
        // Arrange
        MockVerify verify = new("StringVerifier_Pass");
        StringVerifier verifier = new();
        string actual = verify.ReadSourceFile(".txt");

        // Act
        IResult result = await verify.Execute(verifier, actual);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, "Is Success");
        Assert.That(result.Errors, Is.Empty, "Errors");
    }

    [Test]
    public async Task StringVerifier_IsInvalid()
    {
        // Arrange
        MockVerify verify = new("StringVerifier_Fail");
        StringVerifier verifier = new();
        string actual = verify.ReadSourceFile(".txt");

        // Act
        IResult result = await verify.Execute(verifier, actual);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, Is.False, "Is Success");
        Assert.That(result.Errors, Is.Not.Empty, "Errors");
    }
}
