using PersonalBlog.CoreLayer.Utilities;

namespace PersonalBlog.CoreLayer.DTOs.Posts;

public class FilterPostDto :BasePagination
{
    public PostFilterParams filterParams { get; set; }
    public List<PostDto> posts { get; set; }
    
}
