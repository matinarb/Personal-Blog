namespace PersonalBlog.CoreLayer.DTOs.Categories
{
    public class CategoriesDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public CategoriesDto? Parent { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string? MetaTag { get; set; }
        public string? MetaDescription { get; set; }
    }
}