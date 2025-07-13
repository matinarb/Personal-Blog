using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonalBlog.CoreLayer.DTOs.Users;
using PersonalBlog.CoreLayer.Mapper.Users;
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
            var IsUserExist = _context.Users.Any(u => u.UserName == registerDto.UserName);
            if (IsUserExist)
                return OperationResult.Error("نام کاربری تکراری است");

            var passwordHash = registerDto.Password.EncodeToMd5();

            _context.Users.Add(new User()
            {
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
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDto.UserName && u.Password == PasswordHash);
            if (user == null)
                return null;


            return new UserDto()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Role = user.Role,
                UserId = user.Id,
                CreationDate = user.CreationDate
            };
        }

        public FilterUserDto FilterUser(UserFilterParams Params)
        {
            var result = _context.Users.AsQueryable().Where(u => u.IsDelete == false);

            if (!String.IsNullOrWhiteSpace(Params.SearchTerm))
                result = result.Where(u => u.UserName == Params.SearchTerm || u.FullName == Params.SearchTerm);

            var model = new FilterUserDto();
            model.GeneratePaging(result, Params.Take, Params.PageId);
            model.Param = Params;
            model.Users = result.Skip(model.Skip).Take(model.Take).Select(u => UserMapper.MapTo(u)).ToList();

            return model;
        }
    }

}