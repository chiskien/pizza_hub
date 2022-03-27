using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Toppings
{
    public class ToppingManagement : PageModel
    {
        private readonly ToppingDao _toppingDao;
        public CategoryDao categoryDao;
        public IEnumerable<Topping> Toppings { get; set; }

        public ToppingManagement(PizzaHubContext context)
        {
            _toppingDao = new ToppingDao(context);
            categoryDao = new CategoryDao(context);
        }

        public void OnGet()
        {
            Toppings = _toppingDao.GetToppings();
        }

        public IActionResult OnPostDelete(int id)
        {
            var d = _toppingDao.GetToppingById(id);
            if (d != null)
            {
                _toppingDao.DeleteTopping(d);
            }
            return Redirect("/Admin/Toppings/ToppingManagement");
        }
    }
}