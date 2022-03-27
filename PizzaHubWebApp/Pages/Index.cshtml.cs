using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Pizza> Pizzas = new List<Pizza>();
        public IEnumerable<Category> Categories = new List<Category>();
        public IEnumerable<Drink> Drinks = new List<Drink>();
        private readonly PizzaDao _pizzaDao;
        private readonly CategoryDao _categoryDao;
        private readonly DrinkDao _drinkDao;
        private readonly CartDao _cartDao;

        public IndexModel(PizzaHubContext pizzaHubContext)
        {
            _cartDao = new CartDao(pizzaHubContext);
            _drinkDao = new DrinkDao(pizzaHubContext);
            _pizzaDao = new PizzaDao(pizzaHubContext);
            _categoryDao = new CategoryDao(pizzaHubContext);
        }

        public string name { get; set; }

        public void OnGet()
        {
            Pizzas = _pizzaDao.GetPizzaList();
            Categories = _categoryDao.GetCategories();
            Drinks = _drinkDao.GetDrinks();
            name = HttpContext.Session.GetString("name");
        }

        public IActionResult OnPostAddToCart(int id)
        {
            var memberId = HttpContext.Session.GetInt32("memberId");
            _cartDao.AddToCart(id, memberId, 1);
            return RedirectToPage("/Cart");
        }
    }
}