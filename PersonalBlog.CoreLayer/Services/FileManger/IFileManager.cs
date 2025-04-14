using Microsoft.AspNetCore.Http;

namespace PersonalBlog.CoreLayer.Services.FileManager;

public interface IFileManager
{
    string SaveFile(IFormFile file, string savePath);
    void DeleteFile(string fileName, string path);
}