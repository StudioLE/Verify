using DiffEngine;
using StudioLE.Diagnostics;
using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Files;
using StudioLE.Verify.Strings;

namespace StudioLE.Verify;

/// <summary>
/// Methods to help verify test results using <see cref="IVerifier{TValue}"/>.
/// </summary>
public static class VerifyHelpers
{
    /// <summary>
    /// The diff tools, in order of preference, to use when verifying.
    /// </summary>
    public static DiffTool[] DiffTools { get; set; } = {
        DiffTool.VisualStudioCode,
        DiffTool.Rider,
        DiffTool.BeyondCompare,
        DiffTool.P4Merge,
        DiffTool.Kaleidoscope,
        DiffTool.DeltaWalker,
        DiffTool.WinMerge,
        DiffTool.TortoiseMerge,
        DiffTool.TortoiseGitMerge,
        DiffTool.TortoiseGitIDiff,
        DiffTool.TortoiseIDiff,
        DiffTool.KDiff3,
        DiffTool.TkDiff,
        DiffTool.Guiffy,
        DiffTool.ExamDiff,
        DiffTool.Diffinity,
        DiffTool.Vim,
        DiffTool.Neovim,
        DiffTool.AraxisMerge,
        DiffTool.Meld,
        DiffTool.SublimeMerge,
        DiffTool.VisualStudio
    };

    /// <summary>
    /// Should the received file be accepted as the verified file?
    /// </summary>
    /// <remarks>
    /// Automatic acceptance revises the utility of verification from the automated test to the developer's action of committing the verified result.
    /// </remarks>
    public static bool AcceptReceived { get; set; } = false;

    /// <summary>
    /// Verify a string.
    /// </summary>
    public static async Task Verify(this IContext context, string actual)
    {
        StringVerifier verifier = new();
        await verifier.Execute(context, actual);
    }

    /// <summary>
    /// Verify a string.
    /// </summary>
    public static async Task Verify(this IContext context, string expected, string actual)
    {
        StringVerifier verifier = new();
        await verifier.Execute(context, expected, actual);
    }

    /// <summary>
    /// Verify a file.
    /// </summary>
    public static async Task Verify(this IContext context, FileInfo actual)
    {
        FileVerifier verifier = new()
        {
            FileExtension = actual.Extension
        };
        await verifier.Execute(context, actual);
    }
}
