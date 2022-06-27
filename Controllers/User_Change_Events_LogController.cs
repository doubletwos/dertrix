using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;

namespace Dertrix.Controllers
{
    public class User_Change_Events_LogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User_Change_Events_Log
        public ActionResult Index()
        {
            return View(db.User_Change_Events_Logs.ToList());
        }



        // POST: User_Change_Events_Log/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( User_Change_Events_Log user_Change_Events_Log)
        {
            if (ModelState.IsValid)
            {
                db.User_Change_Events_Logs.Add(user_Change_Events_Log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_Change_Events_Log);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
