
using NUnit.Framework;

namespace StudioLE.Diagnostics.NUnit.Tests;

internal sealed class NUnitContextTests
{
    public enum Example
    {
        Hello,
        World
    }

    [Test]
    public void NUnitContextTests_Execute([Range(1,2)] int intValue, [Values] Example enumValue)
    {
        // Arrange
        NUnitContext context = new();

        // Act
        Metadata _ = context.GetMetadata();
        string shortName = context.GetShortName();
        string longName = context.GetLongName();
        string escapedName = context.GetEscapedName();
        string description = context.GetDescription();

        // Assert
        string eol = Environment.NewLine;
        Assert.That(shortName, Is.EqualTo($"NUnitContextTests_Execute ({intValue}, {enumValue})"));
        Assert.That(longName, Is.EqualTo($"NUnitContextTests NUnitContextTests_Execute intValue: {intValue}, enumValue: {enumValue}"));
        Assert.That(escapedName, Is.EqualTo($"NUnitContextTests.NUnitContextTests_Execute_intValue={intValue}_enumValue={enumValue}"));
        Assert.That(description, Is.EqualTo($"NUnitContextTests{eol}NUnitContextTests_Execute{eol}intValue: {intValue}{eol}enumValue: {enumValue}"));
    }
}
