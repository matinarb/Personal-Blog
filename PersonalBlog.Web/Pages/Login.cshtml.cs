using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.CoreLayer.DTOs.Users;
using PersonalBlog.CoreLayer.Services.Users;
using PersonalBlog.CoreLayer.Utilities;

namespace MyApp.Namespace
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class LoginModel : PageModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid){
                return Page();
            }
            var isLoggedIn = _userService.LoginUser(new UserLoginDto()
            {
                UserName = UserName,
                Password = Password
            });
            if (isLoggedIn.Status == OperationResultStatus.NotFound)
            {
                ModelState.AddModelError("UserName", isLoggedIn.Message);
                return Page();
            }

            return RedirectToPage("Index");


        }
    }
}
