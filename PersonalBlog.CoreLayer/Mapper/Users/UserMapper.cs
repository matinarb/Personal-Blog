using PersonalBlog.CoreLayer.DTOs.Users;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.Mapper.Users;

public static class UserMapper
{
    public static UserDto MapTo(User user)
    {
        if (user == null) return null;
        return new UserDto()
        {
            CreationDate = user.CreationDate,
            FullName = user.FullName,
            Role = user.Role,
            UserId = user.Id,
            UserName = user.UserName
        };
    }
}