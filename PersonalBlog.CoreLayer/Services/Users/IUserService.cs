using PersonalBlog.CoreLayer.DTOs.Users;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.Services.Users
{
    public interface IUserService
    {
        OperationResult RegisterUser(UserRegisterDto registerDto);
        UserDto LoginUser(UserLoginDto loginDto);
    }

}

