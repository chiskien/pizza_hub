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
    }
}