using System.Collections.Generic;
using PizzaHubWebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PizzaHubWebApp.DAO
{
    public class ToppingDao
    {
        private readonly PizzaHubContext _pizzaHubContext;

        public ToppingDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
        }

        public IEnumerable<Topping> GetToppings()
        {
            var toppings = _pizzaHubContext.Toppings.ToList();
            return toppings;
        }
        public Topping GetToppingById(int id)
        {
            Topping topping = null;
            topping = _pizzaHubContext.Toppings.FirstOrDefault(m => m.ToppingId == id);
            return topping;
        }
        public void AddTopping(Topping topping)
        {
            _pizzaHubContext.Toppings.Add(topping);
            _pizzaHubContext.SaveChanges();
        }
        public void EditTopping(Topping topping)
        {
            _pizzaHubContext.Entry<Topping>(topping).State = EntityState.Modified;
            _pizzaHubContext.SaveChanges();
        }
        public void DeleteTopping(Topping topping)
        {
            _pizzaHubContext.Toppings.Remove(topping);
            _pizzaHubContext.SaveChanges();
        }
    }
}