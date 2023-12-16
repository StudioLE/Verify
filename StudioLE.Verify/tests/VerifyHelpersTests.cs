using DiffEngine;
using NUnit.Framework;
using StudioLE.Diagnostics;
using StudioLE.Diagnostics.NUnit;
using StudioLE.Verify.Json;
using StudioLE.Verify.Tests.Mock;

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

    [Test]
    [Explicit("Accepts the received value")]
    public async Task VerifyHelpers_AcceptReceived([Values]bool acceptReceived)
    {
        // Arrange
        BBox3[] actual = SampleHelpers.GetInvalidSample();
        MockContext context = new("JsonVerifier_Fail");
        JsonVerifier verifier = new();

        // Act
        VerifyHelpers.AcceptReceived = acceptReceived;
        bool isVerified = await verifier.Execute(context, actual);

        // Assert
        Assert.That(isVerified, Is.False);
    }
}
