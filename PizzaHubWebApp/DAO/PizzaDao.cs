using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.DAO
{
    public class PizzaDao
    {
        private readonly PizzaHubContext _pizzaHubContext;
        private readonly CategoryDao _categoryDao;
        private readonly SauceDao _sauceDao;
        private readonly StatusDao _statusDao;

        public PizzaDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
            _statusDao = new StatusDao(pizzaHubContext);
            _sauceDao = new SauceDao(pizzaHubContext);
            _categoryDao = new CategoryDao(pizzaHubContext);
        }

        //return all pizzas
        public IEnumerable<Pizza> GetPizzaList()
        {
            var pizzaList = _pizzaHubContext.Pizzas.ToList();
            foreach (var pizza in pizzaList)
            {
                pizza.Category = _categoryDao.GetCategoryById(pizza.CategoryId);
                pizza.Sauce = _sauceDao.GetSauceById(pizza.SauceId);
                pizza.Status = _statusDao.GetStatusById(pizza.StatusId);
            }

            return pizzaList;
        }

        public Pizza GetPizzaById(int pizzaId)
        {
            var pizza = _pizzaHubContext.Pizzas
                .AsNoTracking()
                .Single(p => p.PizzaId == pizzaId);
            return pizza;
        }


        public IEnumerable<Pizza> GetPizzasbyCategory(int categoryId)
        {
            var pizzaByCategory = _pizzaHubContext.Pizzas
                .Where(x => x.CategoryId == categoryId)
                .ToList();
            foreach (var pizza in pizzaByCategory)
            {
                pizza.Category = _categoryDao.GetCategoryById(pizza.CategoryId);
                pizza.Sauce = _sauceDao.GetSauceById(pizza.SauceId);
                pizza.Status = _statusDao.GetStatusById(pizza.StatusId);
            }

            return pizzaByCategory;
        }

        public void AddPizza(Pizza newPizza)
        {
            _pizzaHubContext.Pizzas.Add(newPizza);
            _pizzaHubContext.SaveChanges();
        }

        public void EditPizza(Pizza pizza)
        {
            try
            {
                var existedPizza = GetPizzaById(pizza.PizzaId);
                if (existedPizza != null)
                {
                    _pizzaHubContext.Entry(pizza).State = EntityState.Modified;
                    _pizzaHubContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeletePizza(Pizza pizza)
        {
            try
            {
                var existedPizza = GetPizzaById(pizza.PizzaId);
                if (existedPizza != null)
                {
                    _pizzaHubContext.Pizzas.Remove(pizza);
                    _pizzaHubContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<Size> GetAllSize()
        {
            return _pizzaHubContext.Sizes.ToList();
        }

        public IEnumerable<PizzaBasis> GetAllBase()
        {
            return _pizzaHubContext.PizzaBases.ToList();
        }
    }
}