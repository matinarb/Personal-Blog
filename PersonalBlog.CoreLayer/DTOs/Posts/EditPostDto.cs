using Microsoft.AspNetCore.Http;

namespace PersonalBlog.CoreLayer.DTOs.Posts;

public class EditPostDto
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public IFormFile Image { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
}