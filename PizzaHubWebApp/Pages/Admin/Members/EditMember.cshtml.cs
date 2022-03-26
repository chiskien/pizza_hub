using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaHubWebApp.DAO;
using PizzaHubWebApp.Models;

namespace PizzaHubWebApp.Pages.Admin.Members
{
    public class EditMember : PageModel
    {
        public Member Member { get; set; }
        private readonly MemberDao _memberDao;

        public EditMember(PizzaHubContext pizzaHubContext)
        {
            _memberDao = new MemberDao(pizzaHubContext);
        }

        public void OnGet(string id)
        {
            try
            {
                var memberId = int.Parse(id);
                Member = _memberDao.GetMemberById(memberId);
            }
            catch (Exception)
            {
                Response.Redirect("/Admin/Members/MemberManagement");
            }
        }

        public IActionResult OnPostEdit(int id, string email, string pass, DateTime dob, string phone, string address,
            string city, string country, IFormFile ava)
        {
            try
            {
                var member = _memberDao.GetMemberById(id);
                if (member != null)
                {
                    if (email.Trim() != member.Email.Trim())
                    {
                        if (_memberDao.GetMemberByEmail(email.Trim()) != null) throw new Exception("trung email");
                        member.Email = email;
                    }

                    member.Password = pass;
                    member.Dob = dob;
                    member.PhoneNumber = phone;
                    member.Address = address;
                    member.City = city;
                    member.Country = country;
                    if (ava != null)
                        try
                        {
                            System.IO.File.Delete(Path.Combine(
                                Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust\\", member.Avatar));
                            ava.CopyTo(new FileStream(
                                Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust\\" + ava.FileName,
                                FileMode.Create));
                            member.Avatar = ava.FileName;
                        }
                        catch (Exception)
                        {
                            // ignored
                        }

                    _memberDao.EditMember(member);
                    ViewData["AddMessage"] = "Add success";
                    return Redirect("/Admin/Members/MemberManagement");
                }
                else
                {
                    ViewData["AddMessage"] = "Add failed: email has been used";
                    return Redirect("/Admin/Members/MemberManagement");
                }
            }
            catch (Exception)
            {
                return Redirect("/Admin/Members/MemberManagement");
            }
        }
    }
}