namespace PersonalBlog.CoreLayer.Utilities;

public static class Directories
{
    public const string ThumbImg = "wwwroot/img/content/thumb";
    public const string PostContentImg = "wwwroot/img/content/single";
    public static string GetThumbImg(string imageName) { return $"{ThumbImg.Replace("wwwroot","")}/{imageName}"; }
    public static string GetPostContentImg(string imageName) { return $"{PostContentImg.Replace("wwwroot","")}/{imageName}";}
}