using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Services.Posts;

namespace MyApp.Namespace
{
    public class PostModel(IPostService postService) : PageModel
    {
        private readonly IPostService _postService = postService;
        public required PostDto Post { get; set; }

        public IActionResult OnGet(string slug = "")
        {
            Post = _postService.GetPostBy(slug);
            if (Post ==null) return NotFound();
            else return Page();
        }
    }
}
