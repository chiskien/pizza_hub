using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin
{
    public class DashBoard : PageModel
    {
        private readonly PizzaDao _pizzaDao;
        private readonly CategoryDao _categoryDao;
        private readonly MemberDao _memberDao;
        private readonly DrinkDao _drinkDao;

        [BindProperty] public int CategoryId { get; set; }
        public bool Status { get; set; }

        public DashBoard(PizzaHubContext context)
        {
            _pizzaDao = new PizzaDao(context);
            _categoryDao = new CategoryDao(context);
            _drinkDao = new DrinkDao(context);
            _memberDao = new MemberDao(context);
        }

        public IEnumerable<Models.Category> Categories { get; set; }
        public IEnumerable<Models.Pizza> Pizzas { get; set; }

        public void OnGet()
        {
            Categories = _categoryDao.GetCategories();
            Pizzas = _pizzaDao.GetPizzaList();
        }

        public void OnPost()
        {
            if (CategoryId != 0)
                Pizzas = _pizzaDao.GetPizzasbyCategory(CategoryId);
            else
                Pizzas = _pizzaDao.GetPizzaList();
            Categories = _categoryDao.GetCategories();
        }
    }
}