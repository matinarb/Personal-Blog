using System.ComponentModel.DataAnnotations;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.Web.Areas.Admin.Models.Users;

public class EditUserViewModel
{
    public int UserId { get; set; }
    [Display(Name = "نام و نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string FullName { get; set; }
    [Display(Name = "دسترسی")]
    public UserRole Role { get; set; }
}