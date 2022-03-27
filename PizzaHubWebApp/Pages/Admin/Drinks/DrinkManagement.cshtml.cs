using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Drinks
{
    public class DrinkManagement : PageModel
    {
        public IEnumerable<Drink> Drinks { get; set; }
        private readonly DrinkDao _drinkDao;

        public DrinkManagement(PizzaHubContext context)
        {
            _drinkDao = new DrinkDao(context);
        }

        public void OnGet()
        {
            Drinks = _drinkDao.GetDrinks();
        }
        public IActionResult OnPostDelete(int id)
        {
            var d = _drinkDao.GetDrinkById(id);
            if(d != null)
            {
                _drinkDao.DeleteDrink(d);
            }
            return Redirect("/Admin/Drinks/DrinkManagement");
        }
    }
}