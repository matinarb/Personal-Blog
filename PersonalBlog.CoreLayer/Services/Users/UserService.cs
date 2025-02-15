using Azure;
using PersonalBlog.CoreLayer.DTOs.Users;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.DataLayer.Context;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;

        public UserService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            var IsUserExist = _context.Users.Any(u=> u.UserName == registerDto.UserName);
            if(IsUserExist)
                return OperationResult.Error("نام کاربری تکراری است");

            var passwordHash = registerDto.Password.EncodeToMd5();

            _context.Users.Add(new User(){
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                Password = passwordHash,
                IsDelete = false,
                Role = UserRole.User,
                CreationDate = DateTime.Now
            });
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult LoginUser(UserLoginDto loginDto)
        {
            var PasswordHash = loginDto.Password.EncodeToMd5();
            var isLoggedIn = _context.Users.Any(u => u.UserName == loginDto.UserName && u.Password== PasswordHash);
            if(!isLoggedIn)
                return OperationResult.NotFound("نام کاربری یا رمز عبور اشتباه است");


            return OperationResult.Success();
        }
    }

}