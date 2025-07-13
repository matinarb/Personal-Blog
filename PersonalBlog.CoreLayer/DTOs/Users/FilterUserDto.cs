using PersonalBlog.CoreLayer.Utilities;

namespace PersonalBlog.CoreLayer.DTOs.Users
{
    public class FilterUserDto : BasePagination
    {
        public UserFilterParams Param { get; set; }
        public List<UserDto> Users { get; set; }
    }
}