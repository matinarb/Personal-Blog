using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.DTOs.Users
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreationDate { get; set; }
        
        


    }
}