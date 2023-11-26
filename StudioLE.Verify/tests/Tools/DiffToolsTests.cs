using DiffEngine;
using NUnit.Framework;
using StudioLE.Diagnostics;
using StudioLE.Diagnostics.NUnit;

namespace StudioLE.Verify.Tests.Tools;

internal sealed class DiffToolsTests
{
    private readonly IContext _context = new NUnitContext();

    [TestCase("BeyondCompare")]
    [TestCase("Rider")]
    [TestCase("VisualStudioCode")]
    [Explicit("Requires tools to be installed")]
    public void DiffTools_Resolved(string toolName)
    {
        // Arrange
        // Act
        ResolvedTool[] tools = DiffTools.Resolved.ToArray();
        string[] toolNames = tools.Select(x => x.Name).ToArray();

        // Assert
        Assert.That(toolNames, Does.Contain(toolName));
    }

    [TestCase(DiffTool.BeyondCompare)]
    [TestCase(DiffTool.Rider)]
    [TestCase(DiffTool.VisualStudioCode)]
    [Explicit("Intentionally failing")]
    public async Task DiffTools_VisualTest(DiffTool tool)
    {
        // Arrange
        const string actual = "THIS IS THE RECEIVED. The VERIFIED should be blank.";

        // Act
        DiffTools.UseOrder(tool);
        ResolvedTool[] tools = DiffTools.Resolved.ToArray();
        string[] toolNames = tools.Select(x => x.Name).ToArray();

        // Assert
        Assert.That(toolNames, Does.Contain(tool.ToString()));
        await _context.Verify(actual);
    }
}
