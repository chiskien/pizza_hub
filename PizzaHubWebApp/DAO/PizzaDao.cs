using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class PizzaDao
    {
        private readonly PizzaHubContext _pizzaHubContext;

        public PizzaDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
        }

        public IEnumerable<Pizza> GetPizzaList()
        {
            var pizzaList = _pizzaHubContext.Pizzas.ToList();
            foreach (var pizza in pizzaList)
            {
                pizza.Category = GetCategoryById(pizza.CategoryId);
                pizza.Sauce = GetSauceById(pizza.SauceId);
            }

            return pizzaList;
        }

        private Category GetCategoryById(int categoryId)
        {
            var cat = _pizzaHubContext.Categories
                .Single(c => c.CategoryId == categoryId);
            return cat;
        }

        private Sauce GetSauceById(int? sauceId)
        {
            var cat = _pizzaHubContext.Sauces
                .SingleOrDefault(c => c.SauceId == sauceId);
            return cat;
        }

        public Pizza GetPizzaById(int pizzaId)
        {
            var pizza = _pizzaHubContext.Pizzas
                .Single(p => p.PizzaId == pizzaId);
            return pizza;
        }

        public async Task<IEnumerable<Pizza>> GetPizzasbyCategory(int categoryId)
        {
            var pizzaByCategory = await _pizzaHubContext.Pizzas
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
            return pizzaByCategory;
        }
    }
}