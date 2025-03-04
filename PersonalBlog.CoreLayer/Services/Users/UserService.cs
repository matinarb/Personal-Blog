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

        public UserDto LoginUser(UserLoginDto loginDto)
        {
            var PasswordHash = loginDto.Password.EncodeToMd5();
            var user = _context.Users.FirstOrDefault(u=> u.UserName==loginDto.UserName && u.Password==PasswordHash); 
            if(user == null)
                return null;


            return new UserDto(){
                UserName = user.UserName,
                Password = user.Password,
                FullName = user.FullName,
                Role = user.Role,
                UserId = user.Id,
                CreationDate = user.CreationDate
            };
        }
    }

}