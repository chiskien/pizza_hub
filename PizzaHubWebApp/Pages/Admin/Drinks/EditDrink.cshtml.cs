using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Drinks
{
    public class EditDrinkModel : PageModel
    {
        public Drink Drink { get; set; }
        private readonly DrinkDao _drinkDao;

        public EditDrinkModel(PizzaHubContext pizzaHubContext)
        {
            _drinkDao = new DrinkDao(pizzaHubContext);
        }

        public void OnGet(string id)
        {
            try
            {
                var drinkid = int.Parse(id);
                Drink = _drinkDao.GetDrinkById(drinkid);
            }
            catch (Exception)
            {
                Response.Redirect("/Admin/Drinks/DrinkManagement");
            }
        }

        public IActionResult OnPostEdit(int id, string name, string brand, IFormFile image)
        {
            try
            {
                var drink = _drinkDao.GetDrinkById(id);
                if (drink != null)
                {
                    drink.DrinkName = name;
                    drink.Brand = brand;
                    if (image != null)
                    {
                        try
                        {
                            System.IO.File.Delete(Path.Combine(
                                Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Drink\\", drink.Image));
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        image.CopyTo(new FileStream(
                                    Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Drink\\" + image.FileName,
                                    FileMode.Create));
                        drink.Image = image.FileName;
                    }
                    _drinkDao.EditDrink(drink);
                    return Redirect("/Admin/Drinks/DrinkManagement");
                }
                else
                {
                    return Redirect("/Admin/Drinks/DrinkManagement");
                }
            }
            catch (Exception)
            {
                return Redirect("/Admin/Drinks/DrinkManagement");
            }
        }
    }
}
