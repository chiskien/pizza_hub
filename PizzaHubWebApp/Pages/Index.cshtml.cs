using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Pizza> Pizzas { get; private set; }
        private readonly PizzaDao _pizzaDao;

        public IndexModel(PizzaHubContext pizzaHubContext)
        {
            _pizzaDao = new PizzaDao(pizzaHubContext);
        }

        public void OnGet()
        {
            Pizzas = _pizzaDao.GetPizzaList();
        }
    }
}