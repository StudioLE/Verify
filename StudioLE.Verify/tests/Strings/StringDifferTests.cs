using NUnit.Framework;
using StudioLE.Extensions.System;
using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Strings;

namespace StudioLE.Verify.Tests.Strings;

[TestFixture]
public class StringDifferTests
{
    [TestCase(2, 3)]
    [TestCase(3, 2)]
    [TestCase(3, 3)]
    public async Task StringDiffer_DifferentLineCount(int expectedLineCount, int actualLineCount)
    {
        // Arrange
        string expected = Enumerable
            .Range(0, expectedLineCount)
            .Select(x => x.ToString())
            .Join();
        string actual = Enumerable
            .Range(0, actualLineCount)
            .Select(x => x.ToString())
            .Join();
        string expectedPath = Path.GetTempFileName();
        string actualPath = Path.GetTempFileName();
        await File.WriteAllTextAsync(expectedPath, expected);
        await File.WriteAllTextAsync(actualPath, actual);
        VerifyFile expectedFile = new("expected", expectedPath);
        VerifyFile actualFile = new("actual", actualPath);
        StringDiffer differ = new();

        // Act
        IReadOnlyCollection<string> errors = await differ.Execute([expectedFile, actualFile]);

        // Assert
        if(expectedLineCount != actualLineCount)
            Assert.That(errors, Is.Not.Empty);
        else
            Assert.That(errors, Is.Empty);
    }
}
