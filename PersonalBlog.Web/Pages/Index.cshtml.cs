using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.DataLayer.Context;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;

    }
    public int Id { get; set; }
    public string Name { get; set; }
    public void OnGet(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public IActionResult OnPost()
    {

        return Redirect("/Privacy");
    }
}
