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

namespace PizzaHubWebApp.Pages.Admin.Toppings
{
    public class CreateToppingModel : PageModel
    {
        private readonly ToppingDao _toppingDao;
        private readonly CategoryDao _categoryDao;
        public IEnumerable<Category> Categories { get; set; }
        public CreateToppingModel(PizzaHubContext pizzaHubContext)
        {
            _toppingDao = new ToppingDao(pizzaHubContext);
            _categoryDao = new CategoryDao(pizzaHubContext);
            Categories = _categoryDao.GetCategories();
        }
        public void OnGet()
        {
        }
        public void OnPost(string name, int cate, IFormFile image)
        {
            var e = new Topping();
            e.ToppingName = name;
            e.CategoryId = cate;
            if (image != null)
            {
                image.CopyTo(new FileStream(
                    Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Ingredients\\singles\\" + image.FileName, FileMode.Create));
                e.Image = image.FileName;
            }
            _toppingDao.AddTopping(e);
            ViewData["AddMessage"] = "Add success";
            Response.Redirect("/Admin/Toppings/ToppingManagement");
        }
    }
}
