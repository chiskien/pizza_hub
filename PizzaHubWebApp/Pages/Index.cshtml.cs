using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PizzaHubContext _context;

        public IndexModel(PizzaHubContext context)
        {
            _context = context;
        }

        public IEnumerable<Pizza> Pizzas { get; set; }

        public async Task OnGetAsync()
        {
            Pizzas = await _context.Pizzas.ToListAsync();
            foreach (var pizza in Pizzas) pizza.Category = await GetCategorybyId(pizza.CategoryId);
        }

        public async Task<Category> GetCategorybyId(int categoryId)
        {
            var cat =
                await _context.Categories.SingleOrDefaultAsync(cat => cat.CategoryId == categoryId);
            return cat;
        }
    }
}