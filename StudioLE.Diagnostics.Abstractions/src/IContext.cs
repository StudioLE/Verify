namespace StudioLE.Diagnostics;

/// <summary>
/// The diagnostic context.
/// </summary>
public interface IContext
{
    /// <summary>
    /// The short name of the diagnostic context.
    /// </summary>
    public string GetShortName();

    /// <summary>
    /// The long name of the diagnostic context.
    /// </summary>
    public string GetLongName();

    /// <summary>
    /// The description of the diagnostic context.
    /// </summary>
    public string GetDescription();

    /// <summary>
    /// The escaped name of the diagnostic context.
    /// </summary>
    public string GetEscapedName();

    /// <summary>
    /// The metadata of the diagnostic context
    /// </summary>
    public Metadata GetMetadata();

    /// <summary>
    /// Method called when a diagnostic context fails.
    /// </summary>
    public void OnFailure(string message);
}
