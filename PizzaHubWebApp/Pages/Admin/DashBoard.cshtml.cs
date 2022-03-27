using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin
{
    public class DashBoard : PageModel
    {
        private readonly PizzaDao _pizzaDao;
        private readonly CategoryDao _categoryDao;
        private readonly StatusDao _statusDao;

        [BindProperty] public int CategoryId { get; set; }
        public int StatusId { get; set; }

        public DashBoard(PizzaHubContext context)
        {
            _statusDao = new StatusDao(context);
            _pizzaDao = new PizzaDao(context);
            _categoryDao = new CategoryDao(context);
        }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Pizza> Pizzas { get; set; }
        public IEnumerable<Status> Statuses { get; set; }

        public void OnGet()
        {
            Categories = _categoryDao.GetCategories();
            Pizzas = _pizzaDao.GetPizzaList();
            Statuses = _statusDao.GetAllStatus();
            ViewData["name"] = HttpContext.Session.GetString("name");
        }

        public void OnPost()
        {
            if (CategoryId != 0)
                Pizzas = _pizzaDao.GetPizzasbyCategory(CategoryId);
            else
                Pizzas = _pizzaDao.GetPizzaList();
            Categories = _categoryDao.GetCategories();
            Statuses = _statusDao.GetAllStatus();
        }

        public IActionResult OnPostDelete(int id)
        {
            var pizza = _pizzaDao.GetPizzaById(id);
            _pizzaDao.DeletePizza(pizza);
            return RedirectToPage("/Admin/DashBoard");
        }

        public IActionResult OnPostSearch(string search)
        {
            if (string.IsNullOrEmpty(search))
                Pizzas = _pizzaDao.GetPizzaList();
            else
                Pizzas = _pizzaDao.GetPizzaByName(search);
            Categories = _categoryDao.GetCategories();
            Statuses = _statusDao.GetAllStatus();
            return Page();
        }
    }
}