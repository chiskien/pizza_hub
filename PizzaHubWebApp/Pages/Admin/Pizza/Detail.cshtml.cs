using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Pizza
{
    public class Detail : PageModel
    {
        private readonly PizzaDao _pizzaDao;
        private readonly CategoryDao _categoryDao;

        public Detail(PizzaHubContext context)
        {
            _categoryDao = new CategoryDao(context);
            _pizzaDao = new PizzaDao(context);
        }

        public void OnGet(int id)
        {
        }

        public Models.Pizza PizzaModel { get; set; }
        public IEnumerable<Models.Category> Categories { get; set; }
    }
}