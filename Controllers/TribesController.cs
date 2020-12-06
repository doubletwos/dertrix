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
    public class TribesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Tribes
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
            return View(db.Tribes.ToList());
        }
        [ChildActionOnly]
        public ActionResult AddTribe()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddTribe.cshtml");
        }
        public ActionResult EditTribe(int Id)
        {
            if (Id != 0)
            {
                var edttrb = db.Tribes.Where(x => x.TribeId == Id).FirstOrDefault();
                var edttrb1 = new Tribe
                {
                    TribeId = edttrb.TribeId,
                    TribeName = edttrb.TribeName
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditTribe.cshtml", edttrb1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditTribe.cshtml");
        }
        // POST: Tribes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tribe tribe)
        {
            if (ModelState.IsValid)
            {
                db.Tribes.Add(tribe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tribe);
        }
        // POST: Tribes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tribe tribe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tribe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tribe);
        }
        // POST: Tribes/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Tribe tribe = db.Tribes.Find(id);
            db.Tribes.Remove(tribe);
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