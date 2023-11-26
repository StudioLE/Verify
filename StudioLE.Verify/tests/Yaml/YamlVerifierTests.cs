using NUnit.Framework;
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
        MockContext context = new("YamlVerifier_Pass");
        YamlVerifier verifier = new();

        // Act
        bool isVerified = await verifier.Execute(context, actual);

        // Assert
        Assert.That(isVerified, Is.True);
    }

    [Test]
    public async Task YamlVerifier_IsInvalid()
    {
        // Arrange
        BBox3[] actual = SampleHelpers.GetInvalidSample();
        MockContext context = new("YamlVerifier_Fail");
        YamlVerifier verifier = new();

        // Act
        bool isVerified = await verifier.Execute(context, actual);

        // Assert
        Assert.That(isVerified, Is.False);
    }
}
