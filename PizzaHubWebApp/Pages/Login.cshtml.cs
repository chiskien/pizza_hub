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
                if (member.Role == true)
                {
                    Response.Redirect("Admin/DashBoard");
                }
                else
                {
                    Response.Redirect("/Index");
                }
            }
            else
            {
                ViewData["LoginMessage"] = "Login failed: email or password is incorrect";
                Response.Redirect("Login");
            }
        }
        public void OnPostSignUp(string phone, string email, string pass)
        {
            Member member = _memberDao.GetMemberByEmail(email);
            if (member == null)
            {
                //HttpContext.Session.SetInt32("member", member.MemberId);
                Member m = new Member();
                m.MobileNumber = phone;
                m.Email = email;
                m.Password = pass;
                _memberDao.AddMember(m);
                ViewData["LoginMessage"] = "Sign Up success";
                Response.Redirect("Index");
            }
            else
            {
                ViewData["LoginMessage"] = "Sign Up failed: email has been used";
                Response.Redirect("Login");
            }
        }
    }
}