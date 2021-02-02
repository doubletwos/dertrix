using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Dertrix.Models;
namespace Dertrix.Controllers
{
    public class AccessController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Access/Welcome
        public ActionResult Welcome()
        {
            return View();
        }

        // GET: Access/Signin
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(RegisteredUser registeredUser)
        {
            var reguserdetails = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email && x.Password == registeredUser.Password).FirstOrDefault();
            var orgredirect = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email.ToString()).Select(x => x.OrgId).FirstOrDefault();
            var reguserorg = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Select(x => x.OrgName).FirstOrDefault();
            var regUserOrgBrand = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Select(x => x.RegUserOrgBrand).FirstOrDefault();
            if (reguserdetails == null)
            {
                registeredUser.LoginErrorMsg = "Invalid Email or Password";
                return View("Signin", registeredUser);
            }
            else
            {
                Session["RegisteredUserId"] = reguserdetails.RegisteredUserId.ToString();
                Session["Email"] = reguserdetails.Email.ToString();
                Session["FullName"] = reguserdetails.FullName;
                Session["RegisteredUserTypeId"] = reguserdetails.RegisteredUserTypeId;
                Session["OrgId"] = orgredirect;
                Session["OrgName"] = reguserorg;
                Session["regUserOrgBrand"] = regUserOrgBrand;
                Session["regUserOrgBrandBar"] = db.OrgBrands.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.OrgBrandBar).FirstOrDefault();
                Session["regUserOrgNavBar"] = db.OrgBrands.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.OrgNavigationBar).FirstOrDefault();
                Session["regUserOrgNavTextColor"] = db.OrgBrands.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.OrgNavBarTextColour).FirstOrDefault();
                Session["regOrgBrandButtonColour"] = db.OrgBrands.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.OrgBrandButtonColour).FirstOrDefault();
                Session["regOrgLogo"] = db.Files.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.Content).FirstOrDefault();
                Session["IsTester"] = reguserdetails.IsTester;
                Session["OrgType"] = db.Orgs.Where(x => x.OrgId == orgredirect).Select(x => x.OrgTypeId).FirstOrDefault();
                Session["IsAdmin"] = db.RegisteredUsersGroups.Where(x => x.RegisteredUserId == reguserdetails.RegisteredUserId).Where(x => x.RegUserOrgId == orgredirect).Select(x => x.GroupTypeId).FirstOrDefault();
                Session["IsParent/Guardian"] = db.StudentGuardians.Where(x => x.RegisteredUserId == reguserdetails.RegisteredUserId && x.OrgId == orgredirect).Select(x => x.GuardianEmailAddress).FirstOrDefault();
            }
            var reguseraccessLogs = new RegUsersAccessLog
            {
                OrgId = orgredirect,
                SessionId = HttpContext.Session.SessionID.ToString(),
                RegUserId = reguserdetails.RegisteredUserId,
                UserFullName = reguserdetails.FullName,
                LogInTime = DateTime.Now,
                LogOutTime = null
            };
            db.RegUsersAccessLogs.Add(reguseraccessLogs);
            db.SaveChanges();
            if (orgredirect == 23)
            {
                return RedirectToAction("SystemAdminIndex", "Orgs", new { id = orgredirect });
            }
            if (Session["IsParent/Guardian"] != null)
            {
                return RedirectToAction("Home", "Orgs", new { id = orgredirect });
            }
            else
            {
                return RedirectToAction("Index", "Orgs", new { id = orgredirect });
            }
        }

        public ActionResult LogOut(string id)
        {
            if (id == null)
            {
                Session.Abandon();
                return RedirectToAction("Signin", "Access");
            }
            else
            {
                var logouttime = DateTime.Now;
                var getsession = db.RegUsersAccessLogs.AsNoTracking().Where(x => x.SessionId == id).FirstOrDefault();
                var getsession1 = new RegUsersAccessLog
                {
                    RegUsersAccessLogId = getsession.RegUsersAccessLogId,
                    OrgId = getsession.OrgId,
                    SessionId = getsession.SessionId,
                    RegUserId = getsession.RegUserId,
                    UserFullName = getsession.UserFullName,
                    LogInTime = getsession.LogInTime,
                    LogOutTime = logouttime
                };
                getsession = getsession1;
                db.Entry(getsession).State = EntityState.Modified;
                db.SaveChanges();
                Session.Abandon();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                return RedirectToAction("Signin", "Access");
            }
        }
    }
}