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

        public async Task<IEnumerable<Pizza>> GetPizzaList()
        {
            var pizzaList = await _pizzaHubContext.Pizzas.ToListAsync();
            foreach (var pizza in pizzaList)
            {
                pizza.Category = await GetCategoryById(pizza.CategoryId);
                pizza.Sauce = await GetSauceById(pizza.SauceId);
            }

            return pizzaList;
        }

        private async Task<Category> GetCategoryById(int categoryId)
        {
            var cat = await _pizzaHubContext.Categories
                .SingleAsync(c => c.CategoryId == categoryId);
            return cat;
        }

        private async Task<Sauce> GetSauceById(int? sauceId)
        {
            var cat = await _pizzaHubContext.Sauces
                .SingleOrDefaultAsync(c => c.SauceId == sauceId);
            return cat;
        }

        public async Task<Pizza> GetPizzaById(int pizzaId)
        {
            var pizza = await _pizzaHubContext.Pizzas
                .SingleAsync(p => p.PizzaId == pizzaId);
            return pizza;
        }
    }
}