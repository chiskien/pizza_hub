using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Pizza
{
    public class EditPizza : PageModel
    {
        private readonly PizzaDao _pizzaDao;
        private readonly CategoryDao _categoryDao;
        private readonly SauceDao _sauceDao;

        public EditPizza(PizzaHubContext context)
        {
            _sauceDao = new SauceDao(context);
            _categoryDao = new CategoryDao(context);
            _pizzaDao = new PizzaDao(context);
        }

        public void OnGet(int id)
        {
            PizzaModel = _pizzaDao.GetPizzaById(id);
            Categories = _categoryDao.GetCategories();
            Sauces = _sauceDao.GetAllSauces();
        }

        public void OnPost()
        {
        }

        [BindProperty] public Models.Pizza PizzaModel { get; set; }
        public IEnumerable<Models.Category> Categories { get; set; }
        public IEnumerable<Sauce> Sauces { get; set; }
    }
}