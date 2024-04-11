namespace EventPad.Common;

using EventPad.Common.Files;
using Microsoft.AspNetCore.Http;

public static class FormFileExtension
{
    public static FileData ToFileData(this IFormFile file)
    {
        using var stream = new MemoryStream();
        file.CopyTo(stream);

        var extension = Path.GetExtension(file.Name);

        return new FileData()
        {
            Name = file.FileName,
            Extension = extension,
            Content = stream.ToArray()
        };
    }
}