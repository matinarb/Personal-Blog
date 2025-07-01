using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBlog.DataLayer.Entities;

public class Post : BaseEntity
{    
    
    [Required]
    public string Title { get; set; }
    public string Image { get; set; }
    [Required]
    public string Slug { get; set; }
    [Required]
    public string Description { get; set; }
    public int Visit { get; set; }
    public bool IsSpecial { get; set; }



    #region relations
    public int UserId { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    public ICollection<PostComment> PostComments { get; set; }
    #endregion


}
