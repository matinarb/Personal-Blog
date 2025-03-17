using PersonalBlog.CoreLayer.DTOs.Categories;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.Mapper.Categories;

public static class CategoryMapper
{
    public static Category MapTo(AddCategoryDto addCategoryDto)
    {
        return new Category()
        {
            ParentId = addCategoryDto.ParentId,
            Title = addCategoryDto.Title,
            Slug = addCategoryDto.Slug,
            MetaTag = addCategoryDto.MetaTag,
            MetaDescription = addCategoryDto.MetaDescription,
            IsDelete = false,
            CreationDate = DateTime.Now
        };
    }

    public static Category MapTo(EditCategoryDto editCategoryDto, Category category)
    {
        editCategoryDto.Slug = editCategoryDto.Title.toSlug();
        category.Title = editCategoryDto.Title;
        category.Slug = editCategoryDto.Slug;
        category.MetaTag = editCategoryDto.MetaTag;
        category.MetaDescription = editCategoryDto.MetaDescription;

        return category;
    }

    public static CategoriesDto MapTo(Category c)
    {
        return new CategoriesDto()
        {
            Id = c.Id,
            ParentId = c.ParentId,
            Title = c.Title,
            Slug = c.Slug,
            MetaTag = c.MetaTag,
            MetaDescription = c.MetaDescription,
        };
    }
}
