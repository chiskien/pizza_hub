using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Drinks
{
    public class DrinkManagement : PageModel
    {
        private readonly DrinkDao _drinkDao;

        public DrinkManagement(PizzaHubContext context)
        {
            _drinkDao = new DrinkDao(context);
        }

        public void OnGet()
        {
            Drinks = _drinkDao.GetDrinks();
        }

        public IEnumerable<Drink> Drinks { get; set; }
    }
}