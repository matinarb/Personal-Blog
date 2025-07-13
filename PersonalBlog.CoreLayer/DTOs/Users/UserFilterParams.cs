namespace PersonalBlog.CoreLayer.DTOs.Users
{
    public class UserFilterParams
    {
        public string SearchTerm { get; set; }
        public int PageId { get; set; }
        public int Take { get; set; }
    }
}