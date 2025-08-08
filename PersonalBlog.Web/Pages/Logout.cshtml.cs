using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.SignOutAsync();
            
        }
    }
}
