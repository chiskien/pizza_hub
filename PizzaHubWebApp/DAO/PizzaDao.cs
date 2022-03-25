using System.Collections.Generic;
using System.Linq;
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

        //return all pizzas
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

        public Pizza GetPizzaById(int pizzaId)
        {
            var pizza = _pizzaHubContext.Pizzas
                .Single(p => p.PizzaId == pizzaId);
            return pizza;
        }

        private Category GetCategoryById(int categoryId)
        {
            var cat = _pizzaHubContext.Categories
                .Single(c => c.CategoryId == categoryId);
            return cat;
        }

        private Sauce GetSauceById(int? sauceId)
        {
            var sauce = _pizzaHubContext.Sauces
                .SingleOrDefault(c => c.SauceId == sauceId);
            return sauce;
        }

        public IEnumerable<Pizza> GetPizzasbyCategory(int categoryId)
        {
            var pizzaByCategory = _pizzaHubContext.Pizzas
                .Where(x => x.CategoryId == categoryId)
                .ToList();
            foreach (var pizza in pizzaByCategory)
            {
                pizza.Category = GetCategoryById(pizza.CategoryId);
                pizza.Sauce = GetSauceById(pizza.SauceId);
            }

            return pizzaByCategory;
        }

        public void AddPizza(Pizza newPizza)
        {
            Pizza existedPizza = _pizzaHubContext.Pizzas
                .SingleOrDefault(p => p.PizzaId == newPizza.PizzaId);
            if (existedPizza == null)
            {
                _pizzaHubContext.Pizzas.Add(newPizza);
                _pizzaHubContext.SaveChanges();
            }
        }
    }
}