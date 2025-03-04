using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Utilities;

namespace PersonalBlog.CoreLayer.Services.Posts;

public interface IPostService
{
    OperationResult CreatePost(CreatePostDto createPost);
    OperationResult EditPost(EditPostDto editPost);
    FilterPostDto GetPostByFilter(PostFilterParams param);
    PostDto GetPostBy(string slug);
    PostDto GetPostBy(int id);
    bool IsValidSlug(string slug);
}