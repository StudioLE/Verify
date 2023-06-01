using NUnit.Framework;
using StudioLE.Core.Results;

namespace StudioLE.Verify.Tests.Mock;

internal sealed class MockVerify : IVerify
{
    private readonly string _fileNamePrefix;
    private readonly string _directory;

    public MockVerify(string fileNamePrefix)
    {
        _fileNamePrefix = fileNamePrefix;
        string directory = Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "Verify");
        _directory = Path.GetFullPath(directory);
    }

    /// <inheritdoc/>
    public string GetFilePath(string suffix)
    {
        string path = Path.Combine(_directory, _fileNamePrefix + suffix);
        return path;
    }

    /// <inheritdoc/>
    public void OnResult(IResult result, string actualPath, string expectedPath)
    {
        // if (AssemblyHelpers.IsDebugBuild())
        //     DiffRunner.LaunchAsync(receivedFile.FullName, verifiedFile.FullName);
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
}
