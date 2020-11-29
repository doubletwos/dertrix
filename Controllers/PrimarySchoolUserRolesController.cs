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
    public class PrimarySchoolUserRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrimarySchoolUserRoles
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
            return View(db.PrimarySchoolUserRoles.ToList());
        }

        // GET: PrimarySchoolUserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrimarySchoolUserRole primarySchoolUserRole = db.PrimarySchoolUserRoles.Find(id);
            if (primarySchoolUserRole == null)
            {
                return HttpNotFound();
            }
            return View(primarySchoolUserRole);
        }

        // GET: PrimarySchoolUserRoles/Create
        public ActionResult Create()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: PrimarySchoolUserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( PrimarySchoolUserRole primarySchoolUserRole)
        {
            if (ModelState.IsValid)
            {
                db.PrimarySchoolUserRoles.Add(primarySchoolUserRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(primarySchoolUserRole);
        }

        // GET: PrimarySchoolUserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            if ((int)Session["OrgId"] != 23)
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
            PrimarySchoolUserRole primarySchoolUserRole = db.PrimarySchoolUserRoles.Find(id);
            if (primarySchoolUserRole == null)
            {
                return HttpNotFound();
            }
            return View(primarySchoolUserRole);
        }

        // POST: PrimarySchoolUserRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PrimarySchoolUserRole primarySchoolUserRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(primarySchoolUserRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(primarySchoolUserRole);
        }

        // GET: PrimarySchoolUserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            if ((int)Session["OrgId"] != 23)
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
            PrimarySchoolUserRole primarySchoolUserRole = db.PrimarySchoolUserRoles.Find(id);
            if (primarySchoolUserRole == null)
            {
                return HttpNotFound();
            }
            return View(primarySchoolUserRole);
        }

        // POST: PrimarySchoolUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrimarySchoolUserRole primarySchoolUserRole = db.PrimarySchoolUserRoles.Find(id);
            db.PrimarySchoolUserRoles.Remove(primarySchoolUserRole);
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
