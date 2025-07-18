using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.DTOs.Users;

public class UserEditDto
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public UserRole Role { get; set; }
}