using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Pizza
{
    public class CreatePizza : PageModel
    {
        [BindProperty] public Models.Pizza PizzaModel { get; set; }
        private readonly CategoryDao _categoryDao;
        private readonly SauceDao _sauceDao;
        private readonly PizzaSizeDao _pizzaSizeDao;
        private readonly PizzaBaseDao _pizzaBaseDao;

        public CreatePizza(PizzaHubContext context)
        {
            _sauceDao = new SauceDao(context);
            _categoryDao = new CategoryDao(context);
            _pizzaBaseDao = new PizzaBaseDao(context);
            _pizzaSizeDao = new PizzaSizeDao(context);
        }

        public IEnumerable<Models.Category> Categories { get; set; }
        public IEnumerable<Sauce> Sauces { get; set; }
        public List<PizzaSize> PizzaSizes { get; set; }
        public List<PizzaBase> PizzaBases { get; set; }

        public void OnGet()
        {
            Categories = _categoryDao.GetCategories();
            Sauces = _sauceDao.GetAllSauces();
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Admin/DashBoard");
        }
    }
}