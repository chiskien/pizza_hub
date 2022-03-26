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

        public IEnumerable<Category> GetCategories()
        {
            var categories = _pizzaHubContext.Categories.ToList();
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            var cat = _pizzaHubContext.Categories
                .Single(c => c.CategoryId == categoryId);
            return cat;
        }
    }
}