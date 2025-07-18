using Microsoft.AspNetCore.Mvc;
using PersonalBlog.CoreLayer.DTOs.Users;
using PersonalBlog.CoreLayer.Services.Users;
using PersonalBlog.Web.Areas.Admin.Models.Users;

namespace PersonalBlog.Web.Areas.Admin.Controllers
{
    public class UserController(IUserService userService) : AdminControllerBase
    {
        private readonly IUserService _userService = userService;

        public IActionResult Index(int pageId = 1, string search = "")
        {
            var model = _userService.FilterUser(new UserFilterParams()
            {
                PageId = pageId,
                SearchTerm = search,
                Take = 8
            });
            return View(model);
        }

        public IActionResult Pagination(int pageId = 1, string search = "")
        {
            var model = _userService.FilterUser(new UserFilterParams()
            {
                PageId = pageId,
                SearchTerm = search,
                Take = 8
            });
            return PartialView("_UserFiltering", model);
        }

        public IActionResult UserEditModal(int userId)
        {
            var user = _userService.GetUserBy(userId);
            var model = new EditUserViewModel()
            {
                FullName = user.FullName,
                Role = user.Role,
                UserId = user.UserId
            };
            return PartialView("_UserEdit", model);
        }

        [HttpPost]
        public IActionResult EditUser(EditUserViewModel viewModel)
        {
            var result = _userService.EditUser(new UserEditDto()
            {
                FullName = viewModel.FullName,
                Role = viewModel.Role,
                UserId=viewModel.UserId
            });
            return RedirectToAction("Index");
        }
    }
}