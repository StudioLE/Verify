namespace StudioLE.Verify.Tests.Mock;

public class BBox3
{
    // ReSharper disable once InconsistentNaming
    public string discriminator { get; set; } = string.Empty;
    public Vector3 Min { get; set; } = new();
    public Vector3 Max { get; set; } = new();
}
