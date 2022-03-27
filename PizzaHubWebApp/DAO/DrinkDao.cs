using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class DrinkDao
    {
        private readonly PizzaHubContext _pizzaHubContext;

        public DrinkDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
        }

        public IEnumerable<Drink> GetDrinks()
        {
            var drinks = _pizzaHubContext.Drinks.ToList();
            return drinks;
        }
        public Drink GetDrinkById(int id)
        {
            Drink drink = null;
            drink = _pizzaHubContext.Drinks.FirstOrDefault(m => m.DrinkId == id);
            return drink;
        }
        public void AddDrink(Drink drink)
        {
            _pizzaHubContext.Drinks.Add(drink);
            _pizzaHubContext.SaveChanges();
        }
        public void EditDrink(Drink drink)
        {
            _pizzaHubContext.Entry<Drink>(drink).State = EntityState.Modified;
            _pizzaHubContext.SaveChanges();
        }
        public void DeleteDrink(Drink drink)
        {
            _pizzaHubContext.Drinks.Remove(drink);
            _pizzaHubContext.SaveChanges();
        }
    }
}