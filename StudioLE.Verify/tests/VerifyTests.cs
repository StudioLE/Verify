using Newtonsoft.Json;
using StudioLE.Verify.NUnit;
using StudioLE.Verify.Tests.Mock;

namespace StudioLE.Verify.Tests;

internal sealed class VerifyTests
{
    private readonly Verify _verify = new(new NUnitVerifyContext());

    [TestCase(".txt")]
    [TestCase(".pdf")]
    [TestCase(".bin")]
    public async Task Verify_File(string fileExtension)
    {
        // Arrange
        MockVerifyContext context = new("FileVerifier_Pass");
        FileInfo file = context.GetSourceFile(fileExtension);

        // Act
        // Assert
        await _verify.File(file);
    }

    [Test]
    public async Task Verify_AsJson()
    {
        // Arrange
        MockVerifyContext context = new("JsonVerifier_Pass");
        string actualJson = context.ReadSourceFile(".json");

        // Act
        BBox3[]? actual = JsonConvert.DeserializeObject<BBox3[]>(actualJson, JsonVerifier.Converters);
        if (actual is null)
            throw new("Failed to de-serialize.");

        // Act
        // Assert
        await _verify.AsJson(actual);
    }

    [Test]
    public async Task Verify_String()
    {
        // Arrange
        MockVerifyContext context = new("StringVerifier_Pass");
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
