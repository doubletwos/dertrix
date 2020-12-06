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
    public class GendersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Genders
        public ActionResult Index()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.Genders.ToList());
        }

        [ChildActionOnly]
        public ActionResult AddGender()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddGender.cshtml");
        }

        public ActionResult EditGender(int Id)
        {
            if (Id != 0)
            {
                var edtgndr = db.Genders.Where(x => x.GenderId == Id).FirstOrDefault();
                var edtgndr1 = new Gender
                {
                    GenderId = edtgndr.GenderId,
                    GenderName = edtgndr.GenderName
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditGender.cshtml", edtgndr1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditGender.cshtml");
        }


        // POST: Genders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gender gender)
        {
            if (ModelState.IsValid)
            {
                db.Genders.Add(gender);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gender);
        }


        // POST: Genders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gender gender)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gender).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gender);
        }


        // POST: Genders/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Gender gender = db.Genders.Find(id);
            db.Genders.Remove(gender);
            db.SaveChanges();
            return RedirectToAction("Index");
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