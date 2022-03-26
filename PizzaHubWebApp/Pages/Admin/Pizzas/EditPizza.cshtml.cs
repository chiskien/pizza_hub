using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Pizzas
{
    public class EditPizza : PageModel
    {
        private readonly PizzaDao _pizzaDao;
        private readonly CategoryDao _categoryDao;
        private readonly SauceDao _sauceDao;
        private readonly StatusDao _statusDao;

        public EditPizza(PizzaHubContext context)
        {
            _statusDao = new StatusDao(context);
            _sauceDao = new SauceDao(context);
            _categoryDao = new CategoryDao(context);
            _pizzaDao = new PizzaDao(context);
        }

        public void OnGet(int id)
        {
            PizzaModel = _pizzaDao.GetPizzaById(id);
            Categories = _categoryDao.GetCategories();
            Sauces = _sauceDao.GetAllSauces();
            Statuses = _statusDao.GetAllStatus();
        }

        public IActionResult OnPost(IFormFile pizzaImg)
        {
            if (!ModelState.IsValid) return Page();
            pizzaImg.CopyTo(new FileStream(
                Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Pizza\\" + pizzaImg.FileName,
                FileMode.Create));
            PizzaModel.Image = pizzaImg.FileName;
            _pizzaDao.EditPizza(PizzaModel);
            return RedirectToPage("/Admin/DashBoard");
        }

        [BindProperty] public Pizza PizzaModel { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Sauce> Sauces { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
    }
}