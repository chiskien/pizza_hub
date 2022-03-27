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

        public PizzaToppingDetailDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
        }
        public List<PizzaToppingDetail> GetToppingByPizzaId(int id)
        {
            return _pizzaHubContext.PizzaToppingDetails.Where(p => p.PizzaId == id).ToList();
        }
        public void AddPizzaTopping(PizzaToppingDetail pizzaTopping)
        {
            _pizzaHubContext.PizzaToppingDetails.Add(pizzaTopping);
            _pizzaHubContext.SaveChanges();
        }
    }
}
