using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBlog.DataLayer.Entities;

public class PostComment : BaseEntity
{
    
    [Required]
    public string Text { get; set; }


    #region relations
    public int PostId { get; set; }
    public int UserId { get; set; }
    [ForeignKey("PostId")]
    public Post Post { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    #endregion
}
