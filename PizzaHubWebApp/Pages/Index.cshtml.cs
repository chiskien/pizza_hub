using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Pizza> Pizzas { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        private readonly PizzaDao _pizzaDao;
        private readonly CategoryDao _categoryDao;

        public IndexModel(PizzaHubContext pizzaHubContext)
        {
            _pizzaDao = new PizzaDao(pizzaHubContext);
            _categoryDao = new CategoryDao(pizzaHubContext);
        }

        public async Task OnGet()
        {
            Pizzas = await _pizzaDao.GetPizzaList();
            Categories = await _categoryDao.GetCategories();
        }
    }
}