using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Pizza>> GetPizza()
        {
            var pizzas = await _pizzaHubContext.Pizzas.ToListAsync();
            return pizzas;
        }
    }
}