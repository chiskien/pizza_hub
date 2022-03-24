using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzaHubWebApp.Pages.Admin.Pizza
{
    public class CreatePizza : PageModel
    {
        [BindProperty] public Models.Pizza PizzaModel { get; set; }

        public void OnGet()
        {
        }
    }
}