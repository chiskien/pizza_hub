using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

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
            var member = _memberDao.GetMemberLogin(email, pass);
            if (member != null)
            {
                //HttpContext.Session.SetInt32("member", member.MemberId);
                ViewData["LoginMessage"] = "Login success";
                if (member.Role == true)
                    Response.Redirect("Admin/DashBoard");
                else
                    Response.Redirect("/Index");
            }
            else
            {
                ViewData["LoginMessage"] = "Login failed: email or password is incorrect";
                Response.Redirect("Login");
            }
        }

        public void OnPostSignUp(string email, string pass)
        {
            var member = _memberDao.GetMemberByEmail(email);
            if (member == null)
            {
                var m = new Member();
                m.Email = email;
                m.Password = pass;
                _memberDao.AddMember(m);
                HttpContext.Session.SetInt32("member", _memberDao.GetMemberByEmail(email).MemberId);

                var d = new DirectoryInfo(Path.GetPathRoot(@"..\..\..\") +
                                          "PizzaHubWebApp\\wwwroot\\Assets\\Avatar\\Bust\\");
                var Files = d.GetFiles("*.svg");
                var r = new Random();
                m.Avatar = Files[r.Next(0, Files.Length)].Name;
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