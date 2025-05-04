using PersonalBlog.CoreLayer.DTOs.Comments;
using PersonalBlog.CoreLayer.Utilities;

namespace PersonalBlog.CoreLayer.Services.Comment;

public interface ICommentService
{
    List<CommentDto> GetComments(int postId);
    OperationResult AddComment(AddCommentDto command);
    OperationResult DeleteComment(int CommentId);
}