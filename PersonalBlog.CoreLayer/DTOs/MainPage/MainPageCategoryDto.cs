namespace PersonalBlog.CoreLayer.DTOs.MainPage;

public class MainPageCategoryDto
{
    public bool IsCurrentCategory { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public int PostChilds { get; set; }
}