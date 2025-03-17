using Microsoft.AspNetCore.Http;

namespace PersonalBlog.CoreLayer.Services.FileManager;

public class FileManager : IFileManager
{
    public string SaveFile(IFormFile file, string savePath)
    {
        if (file == null)
            return "";


        var fileName = $"{Guid.NewGuid()}{file.FileName}";
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));
        var fullPath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        using var stream = new FileStream(fullPath, FileMode.Create);
        file.CopyToAsync(stream);

        return fullPath;
    }
}