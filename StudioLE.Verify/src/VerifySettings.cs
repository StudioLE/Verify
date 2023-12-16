using DiffEngine;

namespace StudioLE.Verify;

/// <summary>
/// The settings used for verification.
/// </summary>
public static class VerifySettings
{
    /// <summary>
    /// The diff tools, in order of preference, to use when verifying.
    /// </summary>
    public static DiffTool[] DiffTools { get; set; } =
    {
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
}
