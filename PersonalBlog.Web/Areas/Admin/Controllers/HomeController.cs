using Microsoft.AspNetCore.Mvc;
using PersonalBlog.CoreLayer.Services.FileManager;
using PersonalBlog.CoreLayer.Utilities;

namespace PersonalBlog.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {
        private readonly IFileManager _fileManager;
        public HomeController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/Upload/Article")]
        public ActionResult Upload(IFormFile upload)
        {
            if(upload == null) 
                BadRequest();

            var imageName = _fileManager.SaveFile(upload , Directories.PostContentImg);

            return Json(new {uploaded = true, url = Directories.GetPostContentImg(imageName)});
        }
    }
}