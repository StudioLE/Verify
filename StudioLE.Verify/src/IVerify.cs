using StudioLE.Core.Results;
using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify;

/// <summary>
/// Verify test results with an <see cref="IVerifier{TValue}"/>.
/// </summary>
public interface IVerify
{
    public string GetFilePath(string suffix);

    /// <summary>
    /// Process the result after verification.
    /// </summary>
    public void OnResult(IResult result, string expectedPath, string actualPath);
}
