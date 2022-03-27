using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Pizzas
{
    public class CreatePizza : PageModel
    {
        [BindProperty] public Pizza PizzaModel { get; set; }
        [BindProperty] public List<int> Selected { get; set; }
        private readonly CategoryDao _categoryDao;
        private readonly SauceDao _sauceDao;
        private readonly PizzaDao _pizzaDao;
        private readonly StatusDao _statusDao;
        private readonly ToppingDao _toppingDao;

        public CreatePizza(PizzaHubContext context)
        {
            _statusDao = new StatusDao(context);
            _pizzaDao = new PizzaDao(context);
            _sauceDao = new SauceDao(context);
            _categoryDao = new CategoryDao(context);
            _toppingDao = new ToppingDao(context);
        }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Sauce> Sauces { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<Topping> Toppings { get; set; }

        public void OnGet()
        {
            Categories = _categoryDao.GetCategories();
            Sauces = _sauceDao.GetAllSauces();
            Statuses = _statusDao.GetAllStatus();
            Toppings = _toppingDao.GetToppings();
        }

        public IActionResult OnPost(IFormFile pizzaImg)
        {
            if (!ModelState.IsValid) return Page();
            if (pizzaImg != null)
            {
                pizzaImg.CopyTo(new FileStream(
                Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Pizza\\" + pizzaImg.FileName,
                FileMode.Create));
                PizzaModel.Image = pizzaImg.FileName;
            }
            _pizzaDao.AddPizza(PizzaModel, Selected);
            return RedirectToPage("/Admin/DashBoard");
        }
    }
}