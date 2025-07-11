using PersonalBlog.CoreLayer.DTOs.Categories;
using PersonalBlog.CoreLayer.DTOs.Users;

namespace PersonalBlog.CoreLayer.DTOs.Posts;

public class PostDto
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public CategoriesDto? Category { get; set; }
    public UserDto? User { get; set; }
    public int Visit { get; set; }
    public bool IsSpecial { get; set; }

}