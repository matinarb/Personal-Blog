using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}