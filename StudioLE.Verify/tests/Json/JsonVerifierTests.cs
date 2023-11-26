using NUnit.Framework;
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
        MockContext context = new("JsonVerifier_Pass");
        JsonVerifier verifier = new();

        // Act
        bool isVerified = await verifier.Execute(context, actual);

        // Assert
        Assert.That(isVerified, Is.True);
    }

    [Test]
    public async Task JsonVerifier_IsInvalid()
    {
        // Arrange
        BBox3[] actual = SampleHelpers.GetInvalidSample();
        MockContext context = new("JsonVerifier_Fail");
        JsonVerifier verifier = new();

        // Act
        bool isVerified = await verifier.Execute(context, actual);

        // Assert
        Assert.That(isVerified, Is.False);
    }
}
