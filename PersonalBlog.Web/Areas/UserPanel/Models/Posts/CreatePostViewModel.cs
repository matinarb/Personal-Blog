using System.ComponentModel.DataAnnotations;


namespace PersonalBlog.Web.Areas.UserPanel.Models.Posts;

public class CreatePostViewModel
{

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [UIHint("ckEditor4")]
    public string Description { get; set; }

    [Display(Name = "انتخاب دسته بندی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int CategoryId { get; set; }

    [Display(Name = "عکس")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public IFormFile Image { get; set; }

    [Display(Name = "پست ویژه")]
    public bool IsSpecial { get; set; }
}