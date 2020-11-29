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

            if ((int)Session["OrgId"] != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(db.Tribes.ToList());
        }

        // GET: Tribes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            if ((int)Session["OrgId"] != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tribe tribe = db.Tribes.Find(id);
            if (tribe == null)
            {
                return HttpNotFound();
            }
            return View(tribe);
        }

        // GET: Tribes/Create
        public ActionResult Create()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            if ((int)Session["OrgId"] != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
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

        // GET: Tribes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            if ((int)Session["OrgId"] != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tribe tribe = db.Tribes.Find(id);
            if (tribe == null)
            {
                return HttpNotFound();
            }
            return View(tribe);
        }

        // POST: Tribes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Tribe tribe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tribe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tribe);
        }

        // GET: Tribes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            if ((int)Session["OrgId"] != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tribe tribe = db.Tribes.Find(id);
            if (tribe == null)
            {
                return HttpNotFound();
            }
            return View(tribe);
        }

        // POST: Tribes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
