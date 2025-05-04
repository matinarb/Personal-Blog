using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.DTOs.Users;

namespace PersonalBlog.CoreLayer.DTOs.Comments;

public class CommentDto
{
    public int CommentId { get; set; }
    public required string Text { get; set; }
    public required UserDto User { get; set; }
    public required PostDto Post { get; set; }
}