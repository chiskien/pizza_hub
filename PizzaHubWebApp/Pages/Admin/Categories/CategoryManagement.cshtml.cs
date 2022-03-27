using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Categories
{
    public class CategoryManagement : PageModel
    {
        private readonly CategoryDao _categoryDao;
        public IEnumerable<Category> Categories { get; set; }

        public CategoryManagement(PizzaHubContext context)
        {
            _categoryDao = new CategoryDao(context);
        }

        public void OnGet()
        {
            Categories = _categoryDao.GetCategories();
        }
        public IActionResult OnPostDelete(int id)
        {
            var d = _categoryDao.GetCategoryById2(id);
            if (d != null)
            {
                _categoryDao.DeleteCategory(d);
            }
            return Redirect("/Admin/Categories/CategoryManagement");
        }
    }
}