namespace EventPad.Common;

using EventPad.Common.Files;

public static class FilePayloadExtension
{
    public static FileData ToFileData(this FilePayload file)
    {
        var content = Convert.FromBase64String(file.Content);

        var extension = Path.GetExtension(file.FileName);

        return new FileData()
        {
            Name = file.FileName,
            Extension = extension,
            Content = content
        };
    }
}