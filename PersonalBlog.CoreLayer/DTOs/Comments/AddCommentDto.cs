namespace PersonalBlog.CoreLayer.DTOs.Comments;

public class AddCommentDto
{
    public required string Text { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

}