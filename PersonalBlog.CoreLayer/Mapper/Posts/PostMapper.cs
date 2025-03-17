using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Mapper.Categories;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.Mapper.Posts;

public static class PostMapper
{
    public static Post MapTo(CreatePostDto createPost)
    {
        return new Post()
        {
            Title = createPost.Title,
            CreationDate = DateTime.Now,
            Slug = createPost.Title.toSlug(),
            Description = createPost.Description,
            UserId = createPost.UserId,
            CategoryId = createPost.CategoryId,
            Visit = 0,
            IsDelete = false,
        };
    }

    public static Post MapTo(EditPostDto editPost, Post post)
    {
        post.Title = editPost.Title;
        post.Description = editPost.Description;
        post.Slug = editPost.Title.toSlug();
        post.CategoryId = editPost.CategoryId;
        post.Image = editPost.Image;
        return post;
    }

    public static PostDto Mapto(Post p)
    {
        return new PostDto()
        {
            PostId = p.Id,
            CategoryId = p.CategoryId,
            Category = CategoryMapper.MapTo(p.Category),
            CreationDate = p.CreationDate,
            Slug = p.Slug,
            Title = p.Title,
            Image = p.Image,
            UserId = p.UserId,
            Description = p.Description,
            Visit = p.Visit
        };
    }
}