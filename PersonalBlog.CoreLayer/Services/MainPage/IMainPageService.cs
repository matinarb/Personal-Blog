using PersonalBlog.CoreLayer.DTOs.MainPage;

namespace PersonalBlog.CoreLayer.Services.MainPage;

public interface IMainPageService
{
    public MainPageDto GetAll(string? Slug);
}