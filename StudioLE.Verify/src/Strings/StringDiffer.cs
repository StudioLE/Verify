using StudioLE.Verify.Abstractions;

namespace StudioLE.Verify.Strings;

public class StringDiffer : IDiffer
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<string>> Execute(IReadOnlyCollection<VerifyFile> files)
    {
        string[] errors = CheckFilesExist(files);
        if (!errors.Any())
            errors = await CheckNoDistinctLines(files);
        if (!errors.Any())
            errors = CheckNoLinesRemain(files);
        foreach (VerifyFile file in files)
            file.Dispose();
        return errors;
    }

    private static string[] CheckFilesExist(IReadOnlyCollection<VerifyFile> files)
    {
        return files
            .Where(x => !File.Exists(x.Path))
            .Select(x => $"The {x.Name} file does not exist.")
            .ToArray();
    }

    private static async Task<string[]> CheckNoDistinctLines(IReadOnlyCollection<VerifyFile> files)
    {
        int lineNumber = 1;
        while (files.Any(x => x.Reader.Peek() != -1))
        {
            string[] lines = await ReadLinesAsync(files);
            if (AnyAreDifferent(lines))
                return Array.Empty<string>()
                    .Append($"Difference found on line {lineNumber}.")
                    .Concat(files.Select((_, i) => $"{files.ElementAt(i).Name}: {lines[i]}"))
                    .ToArray();
            lineNumber++;
        }
        return Array.Empty<string>();
    }

    private static Task<string[]> ReadLinesAsync(IReadOnlyCollection<VerifyFile> files)
    {
        IEnumerable<Task<string>> tasks = files.Select(x => x.Reader.ReadLineAsync());
        return Task.WhenAll(tasks);
    }

    private static bool AnyAreDifferent(IEnumerable<string> lines)
    {
        return lines.Distinct().Skip(1).Any();
    }

    private static string[] CheckNoLinesRemain(IReadOnlyCollection<VerifyFile> files)
    {
        return files
            .Where(x => x.Reader.Peek() != -1)
            .Select(x => $"Line counts don't match. {x.Name} still has lines.")
            .ToArray();
    }
}
