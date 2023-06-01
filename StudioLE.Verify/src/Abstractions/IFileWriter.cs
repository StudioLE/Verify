namespace StudioLE.Verify.Abstractions;

public interface IFileWriter<in TValue>
{
    public Task Write(string path, TValue value);
}
