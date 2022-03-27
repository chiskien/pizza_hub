using System;
using System.Collections.Generic;
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
        public IEnumerable<Rank> Rank { get; set; }
        private readonly MemberDao _memberDao;
        private readonly RankDao _rankDao;

        public EditMember(PizzaHubContext pizzaHubContext)
        {
            _memberDao = new MemberDao(pizzaHubContext);
            _rankDao = new RankDao(pizzaHubContext);
        }

        public void OnGet(string id)
        {
            try
            {
                var memberId = int.Parse(id);
                Member = _memberDao.GetMemberById(memberId);
                Rank = _rankDao.GetAllRanks();
            }
            catch (Exception)
            {
                Response.Redirect("/Admin/Members/MemberManagement");
            }
        }

        public IActionResult OnPostEdit(int id, string email, string pass, string role, DateTime dob, string phone, string address,
            string city, string country, int point, int rank, IFormFile ava)
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
                    if (role == "Member")
                    {
                        member.Role = false;
                    }
                    else member.Role = true;
                    if (dob > System.Data.SqlTypes.SqlDateTime.MinValue.Value && dob < System.Data.SqlTypes.SqlDateTime.MaxValue.Value)
                    {
                        member.Dob = dob;
                    }
                    member.PhoneNumber = phone;
                    member.Address = address;
                    member.City = city;
                    member.Country = country;
                    if (ava != null)
                    {
                        try
                        {
                            System.IO.File.Delete(Path.Combine(
                                Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust\\", member.Avatar));
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        ava.CopyTo(new FileStream(
                                    Path.GetPathRoot(@"..\..\..\") + "wwwroot\\Assets\\Avatar\\Bust\\" + ava.FileName,
                                    FileMode.Create));
                        member.Avatar = ava.FileName;
                    }
                    member.Point = point;
                    member.RankId = rank;
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