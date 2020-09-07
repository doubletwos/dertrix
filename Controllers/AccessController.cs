using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zeus.Models;

namespace Zeus.Controllers
{
    public class AccessController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Access/Welcome
        public ActionResult Welcome()
        {
            return View();
        }



        // GET: Access/Index
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(RegisteredUser registeredUser)
        {
            var reguserdetails = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email && x.Password == registeredUser.Password).FirstOrDefault();
            var orgredirect = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email.ToString()).Select(x => x.SelectedOrg).FirstOrDefault();
            var reguserorg = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Select(x => x.OrgName).FirstOrDefault();
            var regUserOrgBrand = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Select(x => x.RegUserOrgBrand).FirstOrDefault();
            if (reguserdetails == null)
            {
                registeredUser.LoginErrorMsg = "Invalid Email or Password";
                return View("Index", registeredUser);
            }
            else
            {
                Session["RegisteredUserId"] = reguserdetails.RegisteredUserId.ToString();
                Session["Email"] = reguserdetails.Email.ToString();
                Session["ContactFullName"] = reguserdetails.ContactFullName;
                Session["RegisteredUserTypeId"] = reguserdetails.RegisteredUserTypeId;
                Session["OrgId"] = orgredirect;
                Session["OrgName"] = reguserorg;
                Session["regUserOrgBrand"] = regUserOrgBrand;
                Session["regUserOrgBrandBar"] = db.OrgBrands.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.OrgBrandBar).FirstOrDefault();
                Session["regUserOrgNavBar"] = db.OrgBrands.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.OrgNavigationBar).FirstOrDefault();
                Session["regUserOrgNavTextColor"] = db.OrgBrands.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.OrgNavBarTextColour).FirstOrDefault();
                Session["OrgLogo"] = db.Files.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.Content).FirstOrDefault();
            }
            if (orgredirect == 23)
            {
                return RedirectToAction("SystemAdminIndex", "Orgs", new { id = orgredirect });
            }
            else
            {
                return RedirectToAction("Index", "Orgs", new { id = orgredirect });
            }
        }



        public ActionResult LogOut()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Access");
        }


    }













}
