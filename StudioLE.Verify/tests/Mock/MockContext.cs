using NUnit.Framework;
using StudioLE.Diagnostics;

namespace StudioLE.Verify.Tests.Mock;

internal sealed class MockContext : IContext
{
    private readonly string _fileNamePrefix;
    private readonly string _directory;

    /// <inheritdoc />
    public bool IsDebugBuild { get; } = true;

    public MockContext(string fileNamePrefix)
    {
        _fileNamePrefix = fileNamePrefix;
        string directory = Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "Verify");
        _directory = Path.GetFullPath(directory);
    }

    internal FileInfo GetSourceFile(string fileExtension)
    {
        return new(Path.Combine(_directory, $"{_fileNamePrefix}.source{fileExtension}"));
    }

    internal string ReadSourceFile(string fileExtension)
    {
        FileInfo sourceFile = GetSourceFile(fileExtension);
        if (!sourceFile.Exists)
            throw new FileNotFoundException("Source file was not found: " + sourceFile.FullName);
        return File.ReadAllText(sourceFile.FullName);
    }

    /// <inheritdoc />
    public string GetShortName()
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public string GetLongName()
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public string GetDescription()
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public string GetEscapedName()
    {
        return _fileNamePrefix;
    }

    /// <inheritdoc />
    public Metadata GetMetadata()
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public void OnFailure(string message)
    {
        Console.WriteLine(message);
    }
}
