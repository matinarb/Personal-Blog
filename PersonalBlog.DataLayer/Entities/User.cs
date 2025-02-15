using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.DataLayer.Entities;

public class User : BaseEntity
{
    
    [Required]
    public string UserName { get; set; }
    public string FullName { get; set; }
    [Required]
    public string Password { get; set; }
    public UserRole Role { get; set; }

    #region relations
    public ICollection<Post> Posts { get; set; }
    public ICollection<PostComment> PostComments { get; set; }
    #endregion

}

public enum UserRole
{
    Admin,
    User,
    Writer
}