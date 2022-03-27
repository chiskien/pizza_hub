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
        public Category GetCategoryById2(int categoryId)
        {
            Category category = null;
            category = _pizzaHubContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            return category;
        }
        public void AddCategory(Category category)
        {
            _pizzaHubContext.Categories.Add(category);
            _pizzaHubContext.SaveChanges();
        }
        public void EditCategory(Category category)
        {
            _pizzaHubContext.Entry<Category>(category).State = EntityState.Modified;
            _pizzaHubContext.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            _pizzaHubContext.Categories.Remove(category);
            _pizzaHubContext.SaveChanges();
        }
    }
}