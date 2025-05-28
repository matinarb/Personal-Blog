using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Services.Posts;

namespace MyApp.Namespace
{
    public class SearchModel(IPostService postService) : PageModel
    {
        IPostService _postService = postService;
        public FilterPostDto Filter { get; set; }
        public void OnGet(int pageId = 1, string search = "", string categorySlug = "")
        {
            var param = new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Search = search,
                Take = 1
            };
            Filter = _postService.GetPostByFilter(param);
        }

        public IActionResult OnGetPagination(int pageId, string search, string categorySlug)
        {
            var param = new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Search = search,
                Take = 1
            };
            Filter = _postService.GetPostByFilter(param);
            return Partial("_Pagination" , Filter);
        }
    }
}
