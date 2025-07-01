using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Services.Posts;
using PersonalBlog.CoreLayer.Services.Categories;
using PersonalBlog.CoreLayer.DTOs.Categories;

namespace MyApp.Namespace
{
    public class CategoryModel(IPostService postService, ICategoryService categoryService) : PageModel
    {
        private readonly IPostService _postService = postService;
        private readonly ICategoryService _categoryService = categoryService;

        public required FilterPostDto Filter { get; set; }
        public required CategoriesDto category { get; set; }

        public void OnGet(string slug, int pageId = 1)
        {
            category = _categoryService.GetCategoryBy(slug);
            var param = new PostFilterParams()
            {
                Search = "",
                CategorySlug = slug,
                PageId = pageId,
                Take = 6
            };
            ViewData["category"] = category?.Title ?? "not found";
            Filter = _postService.GetPostByFilter(param);
        }

        public IActionResult OnGetPagination(string slug, int pageId = 1)
        {
            category = _categoryService.GetCategoryBy(slug);
            if(category==null) return NotFound();

            var param = new PostFilterParams()
            {
                Search = "",
                CategorySlug = slug,
                PageId = pageId,
                Take = 6
            };
            ViewData["category"] = category?.Title ?? "not found";
            Filter = _postService.GetPostByFilter(param);
            return Partial("_CategoryPagination", Filter);
        }
    }
}
