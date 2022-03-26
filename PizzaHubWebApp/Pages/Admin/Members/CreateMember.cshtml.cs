using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Members
{
    public class CreateMember : PageModel
    {
        private readonly MemberDao _memberDao;

        public CreateMember(PizzaHubContext pizzaHubContext)
        {
            _memberDao = new MemberDao(pizzaHubContext);
        }

        public void OnGet()
        {
        }

        public void OnPost(string email, string pass, DateTime dob, string phone, string address, string city,
            string country, IFormFile ava)
        {
            var member = _memberDao.GetMemberByEmail(email);
            if (member == null)
            {
                var m = new Member();
                m.Email = email;
                m.Password = pass;
                m.Dob = dob;
                m.PhoneNumber = phone;
                m.Address = address;
                m.City = city;
                m.Country = country;
                if (ava != null)
                {
                    ava.CopyTo(new FileStream(
                        Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust\\" + ava.FileName,
                        FileMode.Create));
                    m.Avatar = ava.FileName;
                }
                else
                {
                    var d = new DirectoryInfo(Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust");
                    var Files = d.GetFiles("*.svg");
                    var r = new Random();
                    m.Avatar = Files[r.Next(0, Files.Length)].Name;
                }

                _memberDao.AddMember(m);
                ViewData["AddMessage"] = "Add success";
                Response.Redirect("/Admin/Member/MemberManagement");
            }
            else
            {
                ViewData["AddMessage"] = "Add failed: email has been used";
                Response.Redirect("/Admin/Member/MemberManagement");
            }
        }
    }
}