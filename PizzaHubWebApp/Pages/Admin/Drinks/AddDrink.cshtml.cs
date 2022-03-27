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

namespace PizzaHubWebApp.Pages.Admin.Drinks
{
    public class AddDrinkModel : PageModel
    {
        private readonly DrinkDao _drinkDao;

        public AddDrinkModel(PizzaHubContext pizzaHubContext)
        {
            _drinkDao = new DrinkDao(pizzaHubContext);
        }
        public void OnGet()
        {
        }
        public void OnPost(string name, string brand, decimal price, IFormFile image)
        {
            var e = new Drink();
            e.DrinkName = name;
            e.Brand = brand;
            e.Price = price;
            if (image != null)
            {
                image.CopyTo(new FileStream(
                    Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Drink\\" + image.FileName, FileMode.Create));
                e.Image = image.FileName;
            }
            _drinkDao.AddDrink(e);
            ViewData["AddMessage"] = "Add success";
            Response.Redirect("/Admin/Drinks/DrinkManagement");
        }
    }
}
