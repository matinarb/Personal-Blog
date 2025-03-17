
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Services.FileManager;
using PersonalBlog.CoreLayer.Services.Posts;
using PersonalBlog.Web.Areas.Admin.Models.Posts;

namespace PersonalBlog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public ActionResult Index(int pageid = 1, string search = "", string categorySlug = "")
        {
            var posts = _postService.GetPostByFilter(new PostFilterParams()
            {
                PageId = pageid,
                Search = search,
                CategorySlug = categorySlug,
                Take = 20
            });
            return View(posts);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _postService.CreatePost(new CreatePostDto(){
                Title = model.Title,
                Description = model.Description,
                Image = model.Image,
                CategoryId = model.CategoryId,

            });
            return RedirectToAction("Index");
        }
    }
}