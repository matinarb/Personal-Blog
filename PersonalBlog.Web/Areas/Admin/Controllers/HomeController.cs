using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}