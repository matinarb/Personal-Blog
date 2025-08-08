using Microsoft.EntityFrameworkCore;
using PersonalBlog.CoreLayer.DTOs.Categories;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Mapper.Posts;
using PersonalBlog.CoreLayer.Services.FileManager;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.DataLayer.Context;
using PersonalBlog.DataLayer.Entities;
using System.Collections.Generic;

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

        var ImageName = _fileManager.SaveImage(createPost.Image, Directories.ThumbImg);
        if (String.IsNullOrEmpty(ImageName)) return OperationResult.Error("فایل عکس نیست");
        post.Image = ImageName;

        _context.Posts.Add(post);
        _context.SaveChanges();

        return OperationResult.Success();
    }

    public OperationResult EditPost(EditPostDto editPost)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == editPost.PostId && p.IsDelete == false);
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
        if (param.UserId != null)
            result = result.Where(p => p.UserId == param.UserId).AsQueryable();
            
        if (!string.IsNullOrWhiteSpace(param.CategorySlug))
            result = result.Where(p => p.Category.Slug == param.CategorySlug);

        if (!string.IsNullOrWhiteSpace(param.Search))
            result = result.Where(p => p.Title.Contains(param.Search));

        var model = new FilterPostDto();
        model.GeneratePaging(result, param.Take, param.PageId);

        model.posts = result.Skip(model.Skip).Take(model.Take).Include(p => p.Category)
                                                  .Include(p => p.Category.Parent)
                                                  .Include(p => p.User)
                                                  .Select(p => PostMapper.Mapto(p)).ToList();
        model.filterParams = param;

        return model;
    }

    public PostDto GetPostBy(string slug)
    {
        var post = _context.Posts.Include(p => p.Category)
                                 .Include(p => p.Category.Parent)
                                 .Include(p => p.User)
                                 .FirstOrDefault(p => p.Slug == slug && p.IsDelete == false);
        if (post == null) return null;
        return PostMapper.Mapto(post);

    }

    public PostDto GetPostBy(int id)
    {
        var post = _context.Posts.Include(p => p.Category)
                                 .Include(p => p.Category.Parent)
                                 .Include(p => p.User)
                                 .FirstOrDefault(p => p.Id == id && p.IsDelete == false);
        if (post == null) return null;
        return PostMapper.Mapto(post);
    }


    public bool IsValidSlug(string slug)
    {
        return _context.Posts.Any(p => p.Slug == slug.toSlug());
    }

    public List<PostDto> GetRelatedPosts(int postId)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == postId && p.IsDelete == false);
        if (post == null) return null;

        var posts = _context.Posts.Where(p => p.CategoryId == post.CategoryId && p.IsDelete == false).OrderByDescending(p => p.CreationDate).Take(6)
                                                                          .Select(p => PostMapper.Mapto(p)).ToList();
        return posts;
    }

    public List<PostDto> GetPopularPosts()
    {
        var posts = _context.Posts.Where(p => p.IsDelete == false).OrderByDescending(p => p.Visit).Take(4)
                                                              .Include(p => p.User)
                                                              .Select(p => PostMapper.Mapto(p)).ToList();
        return posts;
    }

    public void RaiseVisit(int postId)
    {
        var post = _context.Posts.First(p => p.Id == postId);
        post.Visit += 1;
        _context.SaveChanges();
    }

    public List<PostDto> GetSpecialPosts()
    {
        var posts = _context.Posts.Where(p => p.IsSpecial == true).OrderByDescending(p => p.CreationDate)
                                                             .Include(p => p.Category)
                                                             .Include(p => p.User)
                                                             .Select(p => PostMapper.Mapto(p)).ToList();
        return posts;
    }
}