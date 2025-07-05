using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonalBlog.CoreLayer.DTOs.Categories;
using PersonalBlog.CoreLayer.Services.Categories;
using PersonalBlog.DataLayer.Entities;
using PersonalBlog.Web.Areas.Admin.Models.Categories;

namespace PersonalBlog.Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View(_categoryService.GetAllCategories());
        }
        [Route("admin/Category/Add/{slug?}")]
        public IActionResult Add(string? slug)
        {
            return View();
        }
        [HttpPost("admin/Category/Add/{slug?}")]
        public IActionResult Add(string? slug, AddCategoryViewModel categoryViewModel)
        {
            int? parentid = _categoryService.GetCategoryBy(slug)?.Id ?? null;
            if (!ModelState.IsValid) return View(categoryViewModel);
            var result = _categoryService.AddCategory(new AddCategoryDto()
            {
                ParentId = parentid,
                Title = categoryViewModel.Title,
                MetaTag = categoryViewModel.MetaTag,
                MetaDescription = categoryViewModel.MetaDescription
            });
            if (result.Status == CoreLayer.Utilities.OperationResultStatus.Error)
            {
                ModelState.AddModelError("Title", result.Message);
                return View(categoryViewModel);
            }
            return RedirectToAction("Index");
        }


        [Route("admin/Category/Edit/{slug}")]
        public IActionResult Edit(string slug)
        {
            var category = _categoryService.GetCategoryBy(slug);
            if (category == null) return RedirectToAction("Index");
            var editCategory = new EditCategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
                MetaTag = category.MetaTag,
                MetaDescription = category.MetaDescription
            };

            return View(editCategory);
        }

        [HttpPost("admin/Category/Edit/{slug}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategoryViewModel editCategory)
        {
            if (!ModelState.IsValid) return View(editCategory);
            _categoryService.EditCategory(new EditCategoryDto()
            {
                Id = editCategory.Id,
                Title = editCategory.Title,
                MetaTag = editCategory.MetaTag,
                MetaDescription = editCategory.MetaDescription,

            });
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string slug)
        {
            _categoryService.DeleteCategory(slug);
            return RedirectToAction("Index");
        }
    }
}