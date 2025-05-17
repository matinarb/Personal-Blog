using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Utilities;

namespace PersonalBlog.CoreLayer.Services.Posts;

public interface IPostService
{
    OperationResult CreatePost(CreatePostDto createPost);
    OperationResult EditPost(EditPostDto editPost);
    OperationResult DeletePost(int id);
    FilterPostDto GetPostByFilter(PostFilterParams param);
    PostDto GetPostBy(string slug);
    PostDto GetPostBy(int id);
    List<PostDto> GetRelatedPosts(int postId);
    List<PostDto> GetPopularPosts();
    bool IsValidSlug(string slug);
    void RaiseVisit(int postId);
}