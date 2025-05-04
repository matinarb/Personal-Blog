using PersonalBlog.CoreLayer.DTOs.Comments;
using PersonalBlog.CoreLayer.Mapper.Posts;
using PersonalBlog.CoreLayer.Mapper.Users;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.Mapper.Comment;

public static class CommentMapper
{
    public static PostComment MapTo(AddCommentDto ad){
        return new PostComment(){
            IsDelete=false,
            PostId = ad.PostId,
            UserId =ad.UserId,
            Text = ad.Text,
            CreationDate = DateTime.Now,
        };
    }

    public static CommentDto MapTo(PostComment p){
        return new CommentDto(){
            CommentId = p.Id,
            Text = p.Text,
            User = UserMapper.MapTo(p.User),
            Post = PostMapper.Mapto(p.Post)
        };
    }

}