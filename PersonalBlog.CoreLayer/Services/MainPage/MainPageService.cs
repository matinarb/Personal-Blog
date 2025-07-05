using Microsoft.Identity.Client;
using PersonalBlog.CoreLayer.DTOs.Categories;
using PersonalBlog.CoreLayer.DTOs.MainPage;
using PersonalBlog.CoreLayer.DTOs.Posts;
using PersonalBlog.CoreLayer.Services.Categories;
using PersonalBlog.CoreLayer.Services.Posts;
using PersonalBlog.DataLayer.Context;

namespace PersonalBlog.CoreLayer.Services.MainPage;

public class MainPageService(IPostService postService, ICategoryService categoryService) : IMainPageService
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IPostService _postService = postService;
    public MainPageDto GetAll(string Slug = "")
    {
        var current = _categoryService.GetCategoryBy(Slug);

        var SpecialPosts = _postService.GetSpecialPosts().Take(4).ToList();

        var AllCategories = _categoryService.GetAllCategories();



        var Categories = AllCategories.Where(p => p.ParentId == null).Select(p => new MainPageCategoryDto()
        {
            IsCurrentCategory = false,
            Title = p.Title,
            Slug = p.Slug,
            PostChilds = AllCategories.Where(s => s.ParentId == p.Id).Count(),
        }).ToList();

        if (current != null) Categories.First(p => p.Slug == current.Slug).IsCurrentCategory = true;

        var posts = _postService.GetPostByFilter(new PostFilterParams()
        {
            Take = 4,
            PageId = 1,
            Search = "",
            CategorySlug = Slug
        });

        return new MainPageDto()
        {
            Categories = Categories,
            SpecialPosts = SpecialPosts,
            Posts = posts
        };

    }
}