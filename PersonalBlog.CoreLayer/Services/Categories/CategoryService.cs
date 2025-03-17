using Microsoft.EntityFrameworkCore;
using PersonalBlog.CoreLayer.DTOs.Categories;
using PersonalBlog.CoreLayer.Mapper.Categories;
using PersonalBlog.CoreLayer.Utilities;
using PersonalBlog.DataLayer.Context;
using PersonalBlog.DataLayer.Entities;

namespace PersonalBlog.CoreLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogContext _context;
        public CategoryService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult AddCategory(AddCategoryDto addCategoryDto)
        {
            addCategoryDto.Slug = addCategoryDto.Title.toSlug();
            if (isSlugExist(addCategoryDto.Slug))
                return OperationResult.Error("این دسته بندی از قبل وجود دارد");
            _context.Categories.Add(CategoryMapper.MapTo(addCategoryDto));
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto editCategoryDto)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == editCategoryDto.Id);
            if (category == null) return OperationResult.NotFound();
            var newCategory = CategoryMapper.MapTo(editCategoryDto,category);

            _context.Categories.Update(newCategory);
            _context.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult DeleteCategory(string slug)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(c=>c.Slug==slug);
            if(category==null) return OperationResult.NotFound();
            category.IsDelete =true;
            _context.Update(category);
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public List<CategoriesDto> GetAllCategories()
        {
            var categories = _context.Categories.AsNoTracking().Where(c => c.IsDelete == false).Select(c =>CategoryMapper.MapTo(c)).ToList();
            return categories;
        }

        public CategoriesDto GetCategoryBy(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return null;
            var categoryDto = CategoryMapper.MapTo(category);
            return categoryDto;
        }

        public CategoriesDto GetCategoryBy(string Slug)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Slug == Slug);
            if (category == null) return null;
            var categoryDto = CategoryMapper.MapTo(category);
            return categoryDto;
        }

        public bool isSlugExist(string text)
        {
            return _context.Categories.Any(c => c.Slug == text);
        }
    }
}