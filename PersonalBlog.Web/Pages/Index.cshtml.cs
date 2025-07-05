using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.CoreLayer.DTOs.MainPage;
using PersonalBlog.CoreLayer.Services.MainPage;
using PersonalBlog.DataLayer.Context;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.Web.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IMainPageService _mainPageService;
    public MainPageDto mainDto { get; set; }

    public IndexModel(IMainPageService mainPageService)
    {
        _mainPageService = mainPageService;

    }
    public void OnGet()
    {
        mainDto = _mainPageService.GetAll("");
    }

    public IActionResult OnGetPagination(string Slug)
    {
        mainDto = _mainPageService.GetAll(Slug);
        return Partial("_MainPagePagination",mainDto);
    }
}
