using PersonalBlog.CoreLayer.DTOs.Categories;
using PersonalBlog.CoreLayer.Utilities;

namespace PersonalBlog.CoreLayer.Services.Categories
{
    public interface ICategoryService
    {
        OperationResult AddCategory(AddCategoryDto addCategoryDto);
        OperationResult EditCategory(EditCategoryDto editCategoryDto);
        OperationResult DeleteCategory(string slug);
        List<CategoriesDto> GetAllCategories();
        CategoriesDto GetCategoryBy(int id);
        CategoriesDto GetCategoryBy(string Slug);
    }
}