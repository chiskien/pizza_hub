using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages
{
    public class Detail : PageModel
    {
        private readonly PizzaDao _pizzaDao;
        private readonly PizzaToppingDetailDao _pizzaToppingDetailDao;

        public Detail(PizzaHubContext context)
        {
            _pizzaToppingDetailDao = new PizzaToppingDetailDao(context);
            _pizzaDao = new PizzaDao(context);
        }

        public Pizza PizzaModel { get; set; }
        public Cart CartModel { get; set; }

        public IEnumerable<PizzaToppingDetail> ToppingByPizza { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<PizzaBasis> Bases { get; set; }
        public decimal TotalPrice { get; set; }

        public void OnGet(int id)
        {
            ToppingByPizza = _pizzaToppingDetailDao.GetToppingByPizzaId(id);
            PizzaModel = _pizzaDao.GetPizzaById(id);
            Sizes = _pizzaDao.GetAllSize();
            Bases = _pizzaDao.GetAllBase();
            TotalPrice = PizzaModel.Price;
        }

        public IActionResult OnPostCheckout(int id, int size, int pizzabase, int quantity)
        {
            PizzaModel = _pizzaDao.GetPizzaById(id);
            Sizes = _pizzaDao.GetAllSize();
            Bases = _pizzaDao.GetAllBase();
            TotalPrice = PizzaModel.Price;

            ToppingByPizza = _pizzaToppingDetailDao.GetToppingByPizzaId(id);
            HttpContext.Session.SetInt32("pizza", id);
            HttpContext.Session.SetInt32("size", size);
            HttpContext.Session.SetInt32("pizzabase", pizzabase);
            HttpContext.Session.SetInt32("quantity", quantity);
            return RedirectToPage("Cart");
        }
    }
}