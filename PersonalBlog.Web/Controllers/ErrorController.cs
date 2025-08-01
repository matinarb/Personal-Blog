using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers;

public class ErrorController : Controller
{
    [Route("/Error/{code}")]
    public IActionResult Index(int code)
    {
        if (code == 404)
        {
            return View("NotFound");
        }
        else if (code == 500)
        {
            return View("ServerError");
        }

        return View("NotFound");
    }
}