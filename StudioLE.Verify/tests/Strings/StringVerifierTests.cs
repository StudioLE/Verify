using NUnit.Framework;
using StudioLE.Verify.Strings;
using StudioLE.Verify.Tests.Mock;

namespace StudioLE.Verify.Tests.Strings;

internal sealed class StringVerifierTests
{
    [Test]
    public async Task StringVerifier_IsValid()
    {
        // Arrange
        MockContext context = new("StringVerifier_Pass");
        StringVerifier verifier = new();
        string actual = context.ReadSourceFile(".txt");

        // Act
        bool isVerified = await verifier.Execute(context, actual);

        // Assert
        Assert.That(isVerified, Is.True);
    }

    [Test]
    public async Task StringVerifier_IsInvalid()
    {
        // Arrange
        MockContext context = new("StringVerifier_Fail");
        StringVerifier verifier = new();
        string actual = context.ReadSourceFile(".txt");

        // Act
        bool isVerified = await verifier.Execute(context, actual);

        // Assert
        Assert.That(isVerified, Is.False);
    }
}
