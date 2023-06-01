namespace StudioLE.Verify.Abstractions;

public class VerifyFile : IDisposable
{
    private StreamReader? _reader;

    public string Name { get; }

    public string Path { get; }

    public VerifyFile(string name, string path)
    {
        Name = name;
        Path = path;
    }

    public StreamReader Reader => _reader ??= new(Path);

    /// <inheritdoc />
    public void Dispose()
    {
        _reader?.Dispose();
    }
}
