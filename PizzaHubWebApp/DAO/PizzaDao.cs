using System.Collections.Generic;
using System.Linq;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class PizzaDao
    {
        private PizzaHubContext _pizzaHubContext;

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

        public Category GetCategoryById(int categoryId)
        {
            var cat = _pizzaHubContext.Categories
                .Single(c => c.CategoryId == categoryId);
            return cat;
        }

        public Sauce GetSauceById(int? sauceId)
        {
            var cat = _pizzaHubContext.Sauces
                .SingleOrDefault(c => c.SauceId == sauceId);
            return cat;
        }
    }
}