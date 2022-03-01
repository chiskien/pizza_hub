using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class CategoryDao
    {
        private readonly PizzaHubContext _pizzaHubContext;

        public CategoryDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _pizzaHubContext.Categories.ToListAsync();
            return categories;
        }
    }
}