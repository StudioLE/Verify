using NUnit.Framework;
using StudioLE.Core.Results;

namespace StudioLE.Verify.Tests.Mock;

internal sealed class MockVerifyContext : IVerifyContext
{
    /// <inheritdoc/>
    public string FileNamePrefix { get; }

    /// <inheritdoc/>
    public DirectoryInfo Directory { get; } = new(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "Verify"));

    public MockVerifyContext(string fileNamePrefix)
    {
        FileNamePrefix = fileNamePrefix;
    }

    /// <inheritdoc />
    public void Reset()
    {
    }

    /// <inheritdoc/>
    public void OnResult(IResult result, FileInfo receivedFile, FileInfo verifiedFile)
    {
        // if (AssemblyHelpers.IsDebugBuild())
        //     DiffRunner.LaunchAsync(receivedFile.FullName, verifiedFile.FullName);
    }

    internal FileInfo GetSourceFile(string fileExtension)
    {
        return new(Path.Combine(Directory.FullName, $"{FileNamePrefix}.source{fileExtension}"));
    }

    internal string ReadSourceFile(string fileExtension)
    {
        FileInfo sourceFile = GetSourceFile(fileExtension);
        if (!sourceFile.Exists)
            throw new FileNotFoundException("Source file was not found: " + sourceFile.FullName);
        return File.ReadAllText(sourceFile.FullName, Verify.Encoding);
    }
}
