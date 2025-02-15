using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.CoreLayer.DTOs.Users;
using PersonalBlog.CoreLayer.Services.Users;
using PersonalBlog.CoreLayer.Utilities;

namespace MyApp.Namespace
{
    [ValidateAntiForgeryToken]
    [BindProperties] // give access to change properties
    public class RegisterModel : PageModel
    {
        // add viewmodel the properties that is gonna used in view

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string fullname { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string username { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(8, ErrorMessage = "{0} باید بیشتر از 8 کاراکتر باشد")]
        public string password { get; set; }

        // inject service to controller
        private readonly IUserService _userService;
        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }
        
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = _userService.RegisterUser(new UserRegisterDto()
            {
                UserName = username,
                Password = password,
                FullName = fullname
            });
            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("username", result.Message);
                return Page();
            }

            return RedirectToPage("Login");


        }
    }
}
