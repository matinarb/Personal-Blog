using PersonalBlog.CoreLayer.DTOs.Posts;

namespace PersonalBlog.CoreLayer.DTOs.MainPage;

public class MainPageDto
{
    public required List<PostDto> SpecialPosts { get; set; }
    public required FilterPostDto Posts { get; set; }
    public required List<MainPageCategoryDto> Categories { get; set; }
}