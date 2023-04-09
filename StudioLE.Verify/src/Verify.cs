using System.Text;

namespace StudioLE.Verify;

/// <summary>
/// Methods to help verify test results using <see cref="VerifierBase{T}"/>.
/// Engine specific logic is abstracted to <see cref="IVerifyContext"/>.
/// </summary>
/// <remarks>
/// </remarks>
public class Verify
{
    private readonly IVerifyContext _context;

    public static readonly Encoding Encoding = Encoding.UTF8;

    public static bool IsEnabled { get; set; } = true;

    public Verify(IVerifyContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Verify <paramref name="actual"/> as JSON.
    /// </summary>
    public async Task AsJson(object actual)
    {
        if (!IsEnabled)
            return;
        _context.Reset();
        JsonVerifier verifier = new(_context);
        await verifier.Execute(actual);
    }

    /// <summary>
    /// Verify <paramref name="actual"/> as JSON.
    /// </summary>
    public async Task AsJson(object expected, object actual)
    {
        if (!IsEnabled)
            return;
        _context.Reset();
        JsonVerifier verifier = new(_context);
        await verifier.Execute(expected, actual);
    }

    /// <summary>
    /// Verify a string.
    /// </summary>
    public async Task String(string @string)
    {
        if (!IsEnabled)
            return;
        _context.Reset();
        StringVerifier verifier = new(_context, ".txt");
        await verifier.Execute(@string);
    }

    /// <summary>
    /// Verify a string.
    /// </summary>
    public async Task String(string expected, string actual)
    {
        if (!IsEnabled)
            return;
        _context.Reset();
        StringVerifier verifier = new(_context, ".txt");
        await verifier.Execute(expected, actual);
    }

    /// <summary>
    /// Verify a file.
    /// </summary>
    public async Task File(FileInfo file)
    {
        if (!IsEnabled)
            return;
        _context.Reset();
        FileVerifier verifier = new(_context, file.Extension);
        await verifier.Execute(file);
    }
}
