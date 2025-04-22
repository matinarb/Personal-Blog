using Microsoft.EntityFrameworkCore;
using PersonalBlog.CoreLayer.DTOs.Categories;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Mapper.Posts;
using PersonalBlog.CoreLayer.Services.FileManager;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.DataLayer.Context;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.Services.Posts;

public class PostService : IPostService
{
    private readonly BlogContext _context;
    private readonly IFileManager _fileManager;
    public PostService(BlogContext context, IFileManager fileManager)
    {
        _fileManager = fileManager;
        _context = context;
    }


    public OperationResult CreatePost(CreatePostDto createPost)
    {
        if (createPost.Image == null) return OperationResult.Error();

        var post = PostMapper.MapTo(createPost);
        post.Image = _fileManager.SaveFile(createPost.Image, Directories.ThumbImg);

        _context.Posts.Add(post);
        _context.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult EditPost(EditPostDto editPost)
    {
        var post = _context.Posts.AsNoTracking().FirstOrDefault(p => p.Id == editPost.PostId && p.IsDelete == false);
        if (post == null) return OperationResult.NotFound();

        string oldImg = post.Image;

        var newPost = PostMapper.MapTo(editPost, post);
        if (editPost.Image != null)
        {
            newPost.Image = _fileManager.SaveFile(editPost.Image, Directories.ThumbImg);
        }

        _context.Posts.Update(newPost);
        _context.SaveChanges();

        if (editPost.Image != null)
        {
            _fileManager.DeleteFile(oldImg, Directories.ThumbImg);
        }

        return OperationResult.Success();
    }

    public OperationResult DeletePost(int id)
    {
        var post = _context.Posts.AsNoTracking().FirstOrDefault(p => p.Id == id);
        if (post == null) return OperationResult.NotFound();
        post.IsDelete = true;
        _context.Update(post);
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public FilterPostDto GetPostByFilter(PostFilterParams param)
    {
        var result = _context.Posts.Where(p => p.IsDelete == false).OrderByDescending(p => p.CreationDate).AsQueryable();

        if (!string.IsNullOrWhiteSpace(param.CategorySlug))
            result = result.Where(p => p.Category.Slug == param.CategorySlug);

        if (!string.IsNullOrWhiteSpace(param.Search))
            result = result.Where(p => p.Title.Contains(param.Search));

        var skip = (param.PageId - 1) * param.Take;
        var PageCount = result.Count() % param.Take == 0 ? result.Count() / param.Take : result.Count() / param.Take + 1;

        return new FilterPostDto()
        {
            PageCount = PageCount,
            filterParams = param,
            posts = result.Skip(skip).Take(param.Take).Include(p => p.Category).Select(p => PostMapper.Mapto(p)).ToList()
        };
    }

    public PostDto GetPostBy(string slug)
    {
        var post = _context.Posts.AsNoTracking().Include(p => p.Category).FirstOrDefault(p => p.Slug == slug && p.IsDelete == false);
        if (post == null) return null;
        return PostMapper.Mapto(post);

    }

    public PostDto GetPostBy(int id)
    {
        var post = _context.Posts.AsNoTracking().Include(p => p.Category).FirstOrDefault(p => p.Id == id && p.IsDelete == false);
        if (post == null) return null;
        return PostMapper.Mapto(post);
    }


    public bool IsValidSlug(string slug)
    {
        return _context.Posts.Any(p => p.Slug == slug.toSlug());
    }

}