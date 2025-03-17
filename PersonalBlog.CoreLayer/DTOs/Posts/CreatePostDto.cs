using Microsoft.AspNetCore.Http;

namespace PersonalBlog.CoreLayer.DTOs.Posts;
public class CreatePostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public IFormFile Image { get; set; }
}