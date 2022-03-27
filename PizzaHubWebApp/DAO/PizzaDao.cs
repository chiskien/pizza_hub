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
        private readonly PizzaToppingDetailDao _pizzaToppingDetailDao;

        public PizzaDao(PizzaHubContext pizzaHubContext)
        {
            _pizzaHubContext = pizzaHubContext;
            _statusDao = new StatusDao(pizzaHubContext);
            _sauceDao = new SauceDao(pizzaHubContext);
            _categoryDao = new CategoryDao(pizzaHubContext);
            _pizzaToppingDetailDao = new PizzaToppingDetailDao(pizzaHubContext);
        }

        //return all pizzas
        public IEnumerable<Pizza> GetPizzaList()
        {
            var pizzaList = _pizzaHubContext.Pizzas.ToList();
            foreach (var pizza in pizzaList)
            {
                pizza.Category = _categoryDao.GetCategoryById(pizza.CategoryId.Value);
                pizza.Sauce = _sauceDao.GetSauceById(pizza.SauceId);
                pizza.Status = _statusDao.GetStatusById(pizza.StatusId);
            }

            return pizzaList;
        }

        public Pizza GetPizzaById(int pizzaId)
        {
            var pizza = _pizzaHubContext.Pizzas
                .Single(p => p.PizzaId == pizzaId);
            pizza.Category = _categoryDao.GetCategoryById(pizza.CategoryId.Value);
            return pizza;
        }

        public IEnumerable<Pizza> GetPizzaByName(string name)
        {
            var pizza = _pizzaHubContext.Pizzas.Where(p => p.PizzaName.Contains(name.Trim())).ToList();
            foreach (var p in pizza)
            {
                p.Category = _categoryDao.GetCategoryById(p.CategoryId.Value);
                p.Sauce = _sauceDao.GetSauceById(p.SauceId);
                p.Status = _statusDao.GetStatusById(p.StatusId);
            }
            return pizza;
        }
        public Pizza GetPizzaByIdNoTracking(int pizzaId)
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
                pizza.Category = _categoryDao.GetCategoryById(pizza.CategoryId.Value);
                pizza.Sauce = _sauceDao.GetSauceById(pizza.SauceId);
                pizza.Status = _statusDao.GetStatusById(pizza.StatusId);
            }

            return pizzaByCategory;
        }

        public void AddPizza(Pizza newPizza, List<int> selected)
        {
            _pizzaHubContext.Pizzas.Add(newPizza);
            _pizzaHubContext.SaveChanges();

            if (selected.Count > 0)
                foreach (var i in selected)
                {
                    var pizzaTopping = new PizzaToppingDetail();
                    pizzaTopping.PizzaId = newPizza.PizzaId;
                    pizzaTopping.ToppingId = i;
                    _pizzaToppingDetailDao.AddPizzaTopping(pizzaTopping);
                }
        }

        public void EditPizza(Pizza pizza, List<int> selected)
        {
            var existedPizza = GetPizzaByIdNoTracking(pizza.PizzaId);
            if (existedPizza != null)
            {
                _pizzaHubContext.Entry(pizza).State = EntityState.Modified;
                _pizzaHubContext.SaveChanges();

                if (selected.Count > 0)
                {
                    foreach (var i in _pizzaToppingDetailDao.GetToppingByPizzaId(pizza.PizzaId))
                    {
                        PizzaToppingDetail pizzaTopping = null;
                        pizzaTopping = _pizzaHubContext.PizzaToppingDetails.FirstOrDefault(p => p.PizzaTopping == i.PizzaTopping);
                        if(pizzaTopping != null)
                        {
                            _pizzaHubContext.Remove(pizzaTopping);
                            _pizzaHubContext.SaveChanges();
                        }
                    }

                    foreach (var i in selected)
                    {
                        var pizzaTopping = new PizzaToppingDetail();
                        pizzaTopping.PizzaId = pizza.PizzaId;
                        pizzaTopping.ToppingId = i;
                        _pizzaToppingDetailDao.AddPizzaTopping(pizzaTopping);
                    }
                }
            }
        }

        public void DeletePizza(Pizza pizza)
        {
            var existedPizza = GetPizzaByIdNoTracking(pizza.PizzaId);
            if (existedPizza != null)
            {
                _pizzaHubContext.Pizzas.Remove(pizza);
                _pizzaHubContext.SaveChanges();
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