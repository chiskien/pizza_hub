using System.Collections.Generic;
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
    }
}