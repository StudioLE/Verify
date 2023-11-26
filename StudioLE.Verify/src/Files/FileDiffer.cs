using System.Security.Cryptography;
using StudioLE.Verify.Abstractions;
using StudioLE.Verify.Strings;

namespace StudioLE.Verify.Files;

public class FileDiffer : IDiffer
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<string>> Execute(IReadOnlyCollection<VerifyFile> files)
    {
        bool areTextFiles = files.All(IsTextFile);
        if (areTextFiles)
        {
            StringDiffer stringDiffer = new();
            return await stringDiffer.Execute(files);
        }
        string[] errors = CheckFilesExist(files);
        if (errors.Any())
            return errors;
        errors = CheckValuesMatch(files, x => x.Length);
        if (errors.Any())
            return errors.Prepend("File sizes are different").ToArray();
        errors = CheckValuesMatch(files, GetFileHash);
        if (errors.Any())
            return errors.Prepend("File hashes are different").ToArray();
        return Array.Empty<string>();
    }

    private static bool IsTextFile(VerifyFile file)
    {
        string[] textFileExtensions =
        {
            ".json",
            ".svg",
            ".txt",
            ".xml",
            ".yaml",
            ".yml"
        };
        string extension = Path.GetExtension(file.Path);
        return textFileExtensions.Contains(extension);
    }

    private static string[] CheckFilesExist(IReadOnlyCollection<VerifyFile> files)
    {
        return files
            .Where(x => !File.Exists(x.Path))
            .Select(x => $"The {x.Name} file does not exist.")
            .ToArray();
    }

    private static string GetFileHash(FileInfo file)
    {
        using MD5 md5 = MD5.Create();
        using FileStream stream = File.OpenRead(file.FullName);
        byte[] bytes = md5.ComputeHash(stream);
        return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
    }

    private static string[] CheckValuesMatch<TCompare>(IEnumerable<VerifyFile> files, Func<FileInfo, TCompare> func)
    {
        KeyValuePair<string, TCompare>[] hashes = files
            .Select(x =>
            {
                FileInfo file = new(x.Path);
                TCompare result = func.Invoke(file);
                return new KeyValuePair<string, TCompare>(x.Name, result);
            })
            .ToArray();
        KeyValuePairValueComparer<string, TCompare> comparer = new();
        bool areDifferent = hashes.Distinct(comparer).Skip(1).Any();
        return areDifferent
            ? hashes.Select(x => $"{x.Key}: {x.Value}").ToArray()
            : Array.Empty<string>();
    }

}
