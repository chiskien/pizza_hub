using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Categories
{
    public class EditCategory : PageModel
    {
        public Category Category { get; set; }
        private readonly CategoryDao _categoryDao;

        public EditCategory(PizzaHubContext pizzaHubContext)
        {
            _categoryDao = new CategoryDao(pizzaHubContext);
        }

        public void OnGet(string id)
        {
            try
            {
                var catid = int.Parse(id);
                Category = _categoryDao.GetCategoryById2(catid);
            }
            catch (Exception)
            {
                Response.Redirect("/Admin/Categories/CategoryManagement");
            }
        }

        public IActionResult OnPostEdit(int id, string name, IFormFile image)
        {
            try
            {
                var category = _categoryDao.GetCategoryById(id);
                if (category != null)
                {
                    category.CategoryName = name;
                    if (image != null)
                    {
                        try
                        {
                            System.IO.File.Delete(Path.Combine(
                                Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Category\\", category.Image));
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        image.CopyTo(new FileStream(
                                    Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Images\\Category\\" + image.FileName,
                                    FileMode.Create));
                        category.Image = image.FileName;
                    }
                    _categoryDao.EditCategory(category);
                    return Redirect("/Admin/Categories/CategoryManagement");
                }
                else
                {
                    return Redirect("/Admin/Categories/CategoryManagement");
                }
            }
            catch (Exception)
            {
                return Redirect("/Admin/Categories/CategoryManagement");
            }
        }
    }
}