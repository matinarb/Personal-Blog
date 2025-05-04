using Microsoft.EntityFrameworkCore;
using PersonalBlog.CoreLayer.DTOs.Comments;
using PersonalBlog.CoreLayer.Mapper.Comment;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.DataLayer.Context;

namespace PersonalBlog.CoreLayer.Services.Comment;

public class CommentService(BlogContext context) : ICommentService
{
    private readonly BlogContext _context = context;
    public OperationResult AddComment(AddCommentDto command)
    {
        var comment = CommentMapper.MapTo(command);
        _context.PostComments.Add(comment);
        _context.SaveChanges();
        return OperationResult.Success();            
    }

    public OperationResult DeleteComment(int CommentId)
    {
        var comment = _context.PostComments.AsNoTracking().FirstOrDefault(pc=>pc.Id == CommentId);
        if(comment==null) return OperationResult.NotFound();
        comment.IsDelete=true;
        _context.PostComments.Update(comment);
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public List<CommentDto> GetComments(int postId)
    {
        var comments = _context.PostComments.Where(c=>c.PostId==postId)
                                            .Include(c=>c.Post)
                                            .Include(c=>c.User)
                                            .Select(c=>CommentMapper.MapTo(c)).ToList();

        return comments;
    }
}
