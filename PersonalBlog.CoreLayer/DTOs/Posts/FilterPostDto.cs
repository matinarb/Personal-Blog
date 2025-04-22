namespace PersonalBlog.CoreLayer.DTOs.Posts;

public class FilterPostDto
{
    public int PageCount { get; set; }
    public PostFilterParams filterParams { get; set; }
    public List<PostDto> posts { get; set; }
    
}
