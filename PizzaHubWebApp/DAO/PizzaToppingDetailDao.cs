using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class PizzaToppingDetailDao
    {
        private readonly PizzaHubContext _pizzaHubContext;
        private readonly ToppingDao _toppingDao;

        public PizzaToppingDetailDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
            _toppingDao = new ToppingDao(pizzaHubContext);
        }

        public List<PizzaToppingDetail> GetToppingByPizzaId(int id)
        {
            var pizzaToppingDetails = _pizzaHubContext.PizzaToppingDetails
                .Where(p => p.PizzaId == id)
                .ToList();
            foreach (var pizzaToppingDetail in pizzaToppingDetails)
                pizzaToppingDetail.Topping = _toppingDao.GetToppingById(pizzaToppingDetail.ToppingId);

            return pizzaToppingDetails;
        }

        public void AddPizzaTopping(PizzaToppingDetail pizzaTopping)
        {
            _pizzaHubContext.PizzaToppingDetails.Add(pizzaTopping);
            _pizzaHubContext.SaveChanges();
        }
    }
}