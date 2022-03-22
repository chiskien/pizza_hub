using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace PizzaHubWebApp.Pages
{
    public class Login : PageModel
    {
        private readonly MemberDao _memberDao;
        public Login(PizzaHubContext pizzaHubContext)
        {
            _memberDao = new MemberDao(pizzaHubContext);
        }
        public void OnGet()
        {
        }
        public void OnPostLogin(string email, string pass)
        {
            Member member = _memberDao.GetMemberLogin(email, pass);
            if (member != null){
                //HttpContext.Session.SetInt32("member", member.MemberId);
                ViewData["LoginMessage"] = "Login success";
                Response.Redirect("Index");
            }
            else
            {
                ViewData["LoginMessage"] = "Login failed: username or password is incorrect";
                Response.Redirect("Login");
            }
        }
        public void OnPostSignUp(string name, string email, string pass)
        {
            Member member = _memberDao.GetMemberLogin(email, pass);
            if (member != null)
            {
                //HttpContext.Session.SetInt32("member", member.MemberId);
                ViewData["LoginMessage"] = "Login success";
                Response.Redirect("Index");
            }
            else
            {
                ViewData["LoginMessage"] = "Login failed: username or password is incorrect";
                Response.Redirect("Login");
            }
        }
    }
}