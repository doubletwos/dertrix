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

        // GET: Access/Index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signin(RegisteredUser registeredUser)
        {
            var rud = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email && x.Password == registeredUser.Password).FirstOrDefault();
            var orgredirect = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email.ToString()).Select(x => x.SelectedOrg).FirstOrDefault();
            if (rud == null)
            {
                registeredUser.LoginErrorMsg = "Invalid Email or Password";
                return View("Index", registeredUser);
            }

            

            else
            {
                Session["RegisteredUserId"] = rud.RegisteredUserId.ToString();
                Session["Email"] = rud.Email.ToString();
                Session["OrgId"] = orgredirect;
            }


            if (orgredirect == 3)
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

            return RedirectToAction("Index", "Home");
        }


    }













}
