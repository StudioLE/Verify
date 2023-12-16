using DiffEngine;
using NUnit.Framework;
using StudioLE.Diagnostics;
using StudioLE.Diagnostics.NUnit;

namespace StudioLE.Verify.Tests;

internal sealed class VerifyHelpersTests
{
    private readonly IContext _context = new NUnitContext();

    [TestCase(DiffTool.Rider)]
    [TestCase(DiffTool.VisualStudioCode)]
    [Explicit("Intentionally failing")]
    public async Task VerifyHelpers_DiffTools(DiffTool tool)
    {
        // Arrange
        string expected = "Hello, world!";
        string actual = "Hmm, this isn't correct.";

        // Act
        VerifyHelpers.DiffTools = new[] { tool };

        // Assert
        await _context.Verify(expected, actual);
    }
}
