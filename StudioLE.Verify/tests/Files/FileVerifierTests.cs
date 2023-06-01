using NUnit.Framework;
using StudioLE.Core.Results;
using StudioLE.Core.System;
using StudioLE.Verify.Files;
using StudioLE.Verify.Tests.Mock;

namespace StudioLE.Verify.Tests.Files;

internal sealed class FileVerifierTests
{
    [TestCase(".txt")]
    [TestCase(".pdf")]
    [TestCase(".bin")]
    public async Task FileVerifier_IsValid(string fileExtension)
    {
        // Arrange
        MockVerify verify = new("FileVerifier_Pass");
        FileInfo file = verify.GetSourceFile(fileExtension);
        FileVerifier verifier = new();

        // Act
        IResult result = await verify.Execute(verifier, file, fileExtension);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, "Is Success");
        Assert.That(result.Errors, Is.Empty, "Errors");
    }

    [TestCase(".txt")]
    [TestCase(".pdf")]
    [TestCase(".bin")]
    public async Task FileVerifier_IsInvalid(string fileExtension)
    {
        // Arrange
        MockVerify verify = new("FileVerifier_Fail");
        FileInfo file = verify.GetSourceFile(fileExtension);
        FileVerifier verifier = new();

        // Act
        IResult result = await verify.Execute(verifier, file, fileExtension);
        if (result.Errors.Any())
            Console.WriteLine(result.Errors.Join());

        // Assert
        Assert.That(result is Success, Is.False, "Is Success");
        Assert.That(result.Errors, Is.Not.Empty, "Errors");
    }
}
