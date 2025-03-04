namespace PersonalBlog.CoreLayer.DTOs.Posts;

public class FilterPostDto
{
    public int PageCount { get; set; }
    public PostFilterParams filterParams { get; set; }
    public List<PostDto> posts { get; set; }

}

public class PostFilterParams
{
    public int PageId { get; set; }
    public int Take { get; set; }
    public string Search { get; set; }
    public string CategorySlug { get; set; }

}