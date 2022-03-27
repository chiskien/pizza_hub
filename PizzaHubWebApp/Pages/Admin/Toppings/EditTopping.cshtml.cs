using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Toppings
{
    public class EditToppingModel : PageModel
    {
        private readonly ToppingDao _toppingDao;
        private readonly CategoryDao _categoryDao;
        public Topping Topping { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public EditToppingModel(PizzaHubContext pizzaHubContext)
        {
            _toppingDao = new ToppingDao(pizzaHubContext);
            _categoryDao = new CategoryDao(pizzaHubContext);
            Categories = _categoryDao.GetCategories();
        }

        public void OnGet(string id)
        {
            try
            {
                var toppingid = int.Parse(id);
                Topping = _toppingDao.GetToppingById(toppingid);
            }
            catch (Exception)
            {
                Response.Redirect("/Admin/Toppings/ToppingManagement");
            }
        }

        public IActionResult OnPostEdit(int id, string name, int cate, IFormFile image)
        {
            try
            {
                var topping = _toppingDao.GetToppingById(id);
                if (topping != null)
                {
                    topping.ToppingName = name;
                    topping.CategoryId = cate;
                    if (image != null)
                    {
                        try
                        {
                            System.IO.File.Delete(Path.Combine(
                                Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Ingredients\\singles\\", topping.Image));
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        image.CopyTo(new FileStream(
                                    Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Ingredients\\singles\\" + image.FileName,
                                    FileMode.Create));
                        topping.Image = image.FileName;
                    }
                    _toppingDao.EditTopping(topping);
                    return Redirect("/Admin/Toppings/ToppingManagement");
                }
                else
                {
                    return Redirect("/Admin/Toppings/ToppingManagement");
                }
            }
            catch (Exception)
            {
                return Redirect("/Admin/Toppings/ToppingManagement");
            }
        }
    }
}
