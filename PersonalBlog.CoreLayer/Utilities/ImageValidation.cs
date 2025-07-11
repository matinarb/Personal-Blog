using Microsoft.AspNetCore.Http;

namespace PersonalBlog.CoreLayer.Utilities;

public static class ImageValidation
{
    public static bool Validate(IFormFile file)
    {
        if (file == null || file.Length < 4) return false;

        using var reader = new BinaryReader(file.OpenReadStream());
        var header = reader.ReadBytes(8); // Max needed is 8 bytes (PNG)

        // Check JPEG (starts with FF D8)
        if (header[0] == 0xFF && header[1] == 0xD8)
            return true;

        // Check PNG (full 8-byte signature)
        byte[] pngSignature = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        if (header.Take(8).SequenceEqual(pngSignature))
            return true;

        return false;
    }
}