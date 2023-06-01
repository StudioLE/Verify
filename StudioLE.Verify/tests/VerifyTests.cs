using NUnit.Framework;
using StudioLE.Verify.Json;
using StudioLE.Verify.NUnit;
using StudioLE.Verify.Tests.Mock;
using StudioLE.Verify.Yaml;

namespace StudioLE.Verify.Tests;

internal sealed class VerifyTests
{
    private readonly IVerify _verify = new NUnitVerify();

    [TestCase(".txt")]
    [TestCase(".pdf")]
    [TestCase(".bin")]
    public async Task Verify_File(string fileExtension)
    {
        // Arrange
        MockVerify context = new("FileVerifier_Pass");
        FileInfo file = context.GetSourceFile(fileExtension);

        // Act
        // Assert
        await _verify.File(file);
    }

    [Test]
    public async Task Verify_AsJson()
    {
        // Arrange
        BBox3[] sample = SampleHelpers.GetValidSample();

        // Act
        // Assert
        await _verify.AsJson(sample);
    }

    [Test]
    public async Task Verify_AsYaml()
    {
        // Arrange
        BBox3[] sample = SampleHelpers.GetValidSample();

        // Act
        // Assert
        await _verify.AsYaml(sample);
    }

    [Test]
    public async Task Verify_String()
    {
        // Arrange
        MockVerify context = new("StringVerifier_Pass");
        string actual = context.ReadSourceFile(".txt");

        // Act
        // Assert
        await _verify.String(actual);
    }

    [Test]
    [Explicit("Intentionally failing")]
    public async Task Verify_String_Fail()
    {
        // Arrange
        string expected = "Hello, world!";
        string actual = "Hmm, this isn't correct.";

        // Act
        // Assert
        await _verify.String(expected, actual);
    }
}
