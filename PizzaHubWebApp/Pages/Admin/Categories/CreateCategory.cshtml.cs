using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.Models;
using PizzaHubWebApp.DAO;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PizzaHubWebApp.Pages.Admin.Categories
{
    public class CreateCategory : PageModel
    {
        private readonly CategoryDao _categoryDao;

        public CreateCategory(PizzaHubContext pizzaHubContext)
        {
            _categoryDao = new CategoryDao(pizzaHubContext);
        }
        public void OnGet()
        {
        }
        public void OnPost(string name, IFormFile image)
        {
            var c = new Category();
            c.CategoryName = name;
            if (image != null)
            {
                image.CopyTo(new FileStream(
                    Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Category\\" + image.FileName, FileMode.Create));
                c.Image = image.FileName;
            }
            _categoryDao.AddCategory(c);
            ViewData["AddMessage"] = "Add success";
            Response.Redirect("/Admin/Categories/CategoryManagement");
        }
    }
}