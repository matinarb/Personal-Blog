using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PersonalBlog.Web.Areas.Admin.Models.Categories;

public class AddCategoryViewModel
{
    public int? ParentId { get; set; }
    [Display(Name ="نام دسته بندی")]
    [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name ="MetaTag (با - از هم جدا کنید)")]
    public string? MetaTag { get; set; }
    [Display(Name ="MetaDescription (با - از هم جدا کنید)")]
    public string? MetaDescription { get; set; }
}