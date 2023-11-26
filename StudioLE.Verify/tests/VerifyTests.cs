using NUnit.Framework;
using StudioLE.Diagnostics;
using StudioLE.Diagnostics.NUnit;
using StudioLE.Verify.Json;
using StudioLE.Verify.Tests.Mock;
using StudioLE.Verify.Yaml;

namespace StudioLE.Verify.Tests;

internal sealed class VerifyTests
{
    private readonly IContext _context = new NUnitContext();

    [TestCase(".txt")]
    [TestCase(".pdf")]
    [TestCase(".bin")]
    public async Task Verify_File(string fileExtension)
    {
        // Arrange
        MockContext context = new("FileVerifier_Pass");
        FileInfo file = context.GetSourceFile(fileExtension);

        // Act
        // Assert
        await _context.Verify(file);
    }

    [Test]
    public async Task Verify_AsJson()
    {
        // Arrange
        BBox3[] sample = SampleHelpers.GetValidSample();

        // Act
        // Assert
        await _context.VerifyAsJson(sample);
    }

    [Test]
    public async Task Verify_AsYaml()
    {
        // Arrange
        BBox3[] sample = SampleHelpers.GetValidSample();

        // Act
        // Assert
        await _context.VerifyAsYaml(sample);
    }

    [Test]
    public async Task Verify_String()
    {
        // Arrange
        MockContext context = new("StringVerifier_Pass");
        string actual = context.ReadSourceFile(".txt");

        // Act
        // Assert
        await _context.Verify(actual);
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
        await _context.Verify(expected, actual);
    }

    [Test]
    public async Task Verify_String_Unverified()
    {
        // Arrange
        string actual = string.Empty;

        // Act
        // Assert
        await _context.Verify(actual);
    }
}
