using System;
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
        [BindProperty] public List<int> Selected { get; set; }
        private readonly PizzaDao _pizzaDao;
        private readonly CategoryDao _categoryDao;
        private readonly SauceDao _sauceDao;
        private readonly StatusDao _statusDao;
        private readonly ToppingDao _toppingDao;
        private readonly PizzaToppingDetailDao _pizzaToppingDetailDao;

        public EditPizza(PizzaHubContext context)
        {
            _statusDao = new StatusDao(context);
            _sauceDao = new SauceDao(context);
            _categoryDao = new CategoryDao(context);
            _pizzaDao = new PizzaDao(context);
            _toppingDao = new ToppingDao(context);
            _pizzaToppingDetailDao = new PizzaToppingDetailDao(context);
        }

        public void OnGet(int id)
        {
            PizzaModel = _pizzaDao.GetPizzaById(id);
            Categories = _categoryDao.GetCategories();
            Sauces = _sauceDao.GetAllSauces();
            Statuses = _statusDao.GetAllStatus();
            Toppings = _toppingDao.GetToppings();
            SelectedToppings = new List<int>();
            foreach (var i in _pizzaToppingDetailDao.GetToppingByPizzaId(id))
            {
                SelectedToppings.Add(i.ToppingId.Value);
            }
            
        }

        public IActionResult OnPost(IFormFile pizzaImg)
        {
            if (pizzaImg != null)
            {
                System.IO.File.Delete(Path.Combine(
                    Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Category\\", PizzaModel.Image));
                pizzaImg.CopyTo(new FileStream(
                    Path.GetPathRoot(@"..\..\..\") +
                    "wwwroot\\Assets\\Images\\Pizza\\" + pizzaImg.FileName,
                    FileMode.Create));
                PizzaModel.Image = pizzaImg.FileName;
            }

            _pizzaDao.EditPizza(PizzaModel, Selected);
            return RedirectToPage("/Admin/DashBoard");
        }

        [BindProperty] public Pizza PizzaModel { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Sauce> Sauces { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<Topping> Toppings { get; set; }
        public List<int> SelectedToppings { get; set; }

    }
}