namespace CatFactGetter.Services;

public class FileService : IFileService
{
    public void AddFactLine(string path, string line)
    {
        try
        {
            using var writer = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write));
            writer.WriteLine(line);
        }
        catch (Exception e)
        {
            throw new IOException("Failed to add fact", e);
        }

    }
}