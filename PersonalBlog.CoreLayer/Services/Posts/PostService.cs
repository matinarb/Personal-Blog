using Microsoft.EntityFrameworkCore;
using PersonalBlog.CoreLayer.DTOs.Categories;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.DataLayer.Context;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.Services.Posts;

public class PostService : IPostService
{
    private readonly BlogContext _context;
    public PostService(BlogContext context)
    {
        _context = context;
    }


    public OperationResult CreatePost(CreatePostDto createPost)
    {
        _context.Posts.Add(new Post()
        {
            Title = createPost.Title,
            Slug = createPost.Title.toSlug(),
            Description = createPost.Description,
            UserId = createPost.UserId,
            CategoryId = createPost.CategoryId,
            Visit = 0,
            IsDelete = false
        });
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public OperationResult EditPost(EditPostDto editPost)
    {
        var post = _context.Posts.AsNoTracking().FirstOrDefault(p => p.Id == editPost.PostId && p.IsDelete == false);
        if (post == null) return OperationResult.NotFound();
        post.Title = editPost.Title;
        post.Description = editPost.Description;
        post.Slug = editPost.Title.toSlug();
        post.CategoryId = editPost.CategoryId;
        _context.Posts.Update(post);
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public FilterPostDto GetPostByFilter(PostFilterParams param)
    {
        var result = _context.Posts.OrderByDescending(p => p.CreationDate).AsQueryable();

        if (!string.IsNullOrWhiteSpace(param.CategorySlug))
            result.Where(p => p.Category.Slug == param.CategorySlug);

        if(!string.IsNullOrWhiteSpace(param.Search))
            result.Where(p=>p.Title.Contains(param.Search));

        var skip = (param.PageId-1)*param.Take;
        var PageCount = result.Count() / param.Take;

        return new FilterPostDto(){
            PageCount = PageCount,
            filterParams=param,
            posts = result.Skip(skip).Take(param.Take).Select(p=>new PostDto(){
                PostId = p.Id,
            CategoryId = p.CategoryId,
            Category = new CategoriesDto()
            {
                Id = p.Category.Id,
                Title = p.Category.Title,
                Slug = p.Category.Slug,
                MetaDescription = p.Category.MetaDescription,
                MetaTag = p.Category.MetaTag,
                ParentId = p.Category.ParentId
            },
            CreationDate = p.CreationDate,
            Slug = p.Slug,
            Title = p.Title,
            UserId = p.UserId,
            Description = p.Description,
            Visit = p.Visit

            }).ToList()};
    }

    public PostDto GetPostBy(string slug)
    {
        var post = _context.Posts.AsNoTracking().FirstOrDefault(p => p.Slug == slug && p.IsDelete == false);
        if (post == null) return null;
        return new PostDto()
        {
            PostId = post.Id,
            CategoryId = post.CategoryId,
            Category = new CategoriesDto()
            {
                Id = post.Category.Id,
                Title = post.Category.Title,
                Slug = post.Category.Slug,
                MetaDescription = post.Category.MetaDescription,
                MetaTag = post.Category.MetaTag,
                ParentId = post.Category.ParentId
            },
            CreationDate = post.CreationDate,
            Slug = post.Slug,
            Title = post.Title,
            UserId = post.UserId,
            Description = post.Description,
            Visit = post.Visit
        };

    }

    public PostDto GetPostBy(int id)
    {
        var post = _context.Posts.AsNoTracking().FirstOrDefault(p => p.Id == id && p.IsDelete == false);
        if (post == null) return null;
        return new PostDto()
        {
            PostId = post.Id,
            CategoryId = post.CategoryId,
            Category = new CategoriesDto()
            {
                Id = post.Category.Id,
                Title = post.Category.Title,
                Slug = post.Category.Slug,
                MetaDescription = post.Category.MetaDescription,
                MetaTag = post.Category.MetaTag,
                ParentId = post.Category.ParentId
            },
            CreationDate = post.CreationDate,
            Slug = post.Slug,
            Title = post.Title,
            UserId = post.UserId,
            Description = post.Description,
            Visit = post.Visit
        };
    }


    public bool IsValidSlug(string slug)
    {
        return _context.Posts.Any(p => p.Slug == slug.toSlug());
    }
}