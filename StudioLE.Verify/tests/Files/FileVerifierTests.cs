using NUnit.Framework;
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
        MockContext context = new("FileVerifier_Pass");
        FileInfo file = context.GetSourceFile(fileExtension);
        FileVerifier verifier = new()
        {
            FileExtension = fileExtension
        };

        // Act
        bool isVerified = await verifier.Execute(context, file);

        // Assert
        Assert.That(isVerified, Is.True);
    }

    [TestCase(".txt")]
    [TestCase(".pdf")]
    [TestCase(".bin")]
    public async Task FileVerifier_IsInvalid(string fileExtension)
    {
        // Arrange
        MockContext context = new("FileVerifier_Fail");
        FileInfo file = context.GetSourceFile(fileExtension);
        FileVerifier verifier = new()
        {
            FileExtension = fileExtension
        };

        // Act
        bool isVerified = await verifier.Execute(context, file);

        // Assert
        Assert.That(isVerified, Is.False);
    }
}
