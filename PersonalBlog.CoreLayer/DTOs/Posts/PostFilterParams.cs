namespace PersonalBlog.CoreLayer.DTOs.Posts;


public class PostFilterParams
{
    public int PageId { get; set; }
    public int Take { get; set; }
    public string Search { get; set; }
    public string CategorySlug { get; set; }

}