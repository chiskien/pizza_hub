using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzaHubWebApp.Pages
{
    public class Cart : PageModel
    {
        [BindProperty]
        public Cart CartModel { get; set; }
        public void OnGet()
        {
            
        }
    }
}