using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            var user = _userService.LoginUser(new UserLoginDto()
            {
                UserName = UserName,
                Password = Password
            });
            if (user == null)
            {
                ModelState.AddModelError("UserName", "چنین کاربری وجود ندارد");
                return Page();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim("test","taest"),
                new Claim(ClaimTypes.NameIdentifier , user.UserId.ToString()),
                new Claim(ClaimTypes.Name , user.FullName)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal , properties);

            return RedirectToPage("Index");


        }
    }
}
