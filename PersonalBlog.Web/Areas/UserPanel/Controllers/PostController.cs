
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Services.FileManager;
using PersonalBlog.CoreLayer.Services.Posts;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.Web.Areas.UserPanel.Models.Posts;

namespace PersonalBlog.Web.Areas.UserPanel.Controllers
{
    public class PostController : UserPanelControllerBase
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
                UserId = User.GetUserId(),
                PageId = pageid,
                Search = search,
                CategorySlug = categorySlug,
                Take = 8
            });
            return View(posts);
        }
        public IActionResult Pagination(int pageid = 1, string search = "", string categorySlug = "")
        {
            var posts = _postService.GetPostByFilter(new PostFilterParams()
            {
                UserId = User.GetUserId(),
                PageId = pageid,
                Search = search,
                CategorySlug = categorySlug,
                Take = 8
            });
            return PartialView("_PostFiltering", posts);
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
            if (!User?.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/login");
            }
            var result = _postService.CreatePost(new CreatePostDto()
            {
                Title = model.Title,
                Description = model.Description,
                Image = model.Image,
                CategoryId = model.CategoryId,
                UserId = User.GetUserId(),
                IsSpecial = model.IsSpecial
            });


            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError("Image", result.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        [Route("/UserPanel/post/edit/{id}")]
        public ActionResult Edit(int id)
        {
            var post = _postService.GetPostBy(id);
            if (post.UserId != User.GetUserId())
            {
                throw new Exception();
            }
            var EditViewModel = new EditPostViewModel()
            {
                Title = post.Title,
                CategoryId = post.CategoryId,
                Description = post.Description,
                IsSpecial = post.IsSpecial
            };
            return View(EditViewModel);
        }

        [HttpPost("/UserPanel/post/edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditPostViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var post = _postService.GetPostBy(id);
            if (post.UserId != User.GetUserId())
            {
                return NotFound();
            }

            var result = _postService.EditPost(new EditPostDto()
            {
                Title = model.Title,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Image = model.Image,
                PostId = id,
                IsSpecial = model.IsSpecial
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError("Title", result.Message);
                return View(model);
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var result = _postService.DeletePost(id);
            return RedirectToAction("Index");
        }
    }
}