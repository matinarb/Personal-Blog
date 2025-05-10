using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonalBlog.CoreLayer.DTOs.Comments;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Services.Comment;
using PersonalBlog.CoreLayer.Services.Posts;
using PersonalBlog.CoreLayer.Utilities;

namespace MyApp.Namespace
{
    [ValidateAntiForgeryToken]
    public class PostModel(IPostService postService, ICommentService commentService) : PageModel
    {
        private readonly ICommentService _commentService = commentService;
        private readonly IPostService _postService = postService;

        public required PostDto Post { get; set; }
        public List<CommentDto>? Comments { get; set; }
        public List<PostDto> RelatedPosts { get; set; }

        [BindProperty]
        [Display(Name ="دیدگاه")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public required string Text { get; set; }
        [BindProperty]
        [Required]
        public int  PostId{ get; set; }



        public IActionResult OnGet(string slug = "")
        {

            Post = _postService.GetPostBy(slug);
            if (Post == null) return NotFound();
            Comments = _commentService.GetComments(Post.PostId);
            RelatedPosts = _postService.GetRelatedPosts(Post.PostId);
            return Page();
        }
        public IActionResult OnPost(string slug = "")
        {
            
            if (!User.Identity.IsAuthenticated) return RedirectToPage("Post", new { slug = slug });

            if (!ModelState.IsValid)
            {
                Post = _postService.GetPostBy(slug);
                Comments = _commentService.GetComments(Post.PostId);
                RelatedPosts = _postService.GetRelatedPosts(Post.PostId);
                return Page();
            }

            _commentService.AddComment(new AddCommentDto()
            {
                Text = Text,
                PostId = PostId,
                UserId = User.GetUserId()
            });

            return RedirectToPage("Post", new { slug = slug });
        }
    }
}
