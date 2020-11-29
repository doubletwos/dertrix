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
    public class SecondarySchoolUserRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SecondarySchoolUserRoles
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
            return View(db.SecondarySchoolUserRoles.ToList());
        }

        // GET: SecondarySchoolUserRoles/Details/5
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
            SecondarySchoolUserRole secondarySchoolUserRole = db.SecondarySchoolUserRoles.Find(id);
            if (secondarySchoolUserRole == null)
            {
                return HttpNotFound();
            }
            return View(secondarySchoolUserRole);
        }

        // GET: SecondarySchoolUserRoles/Create
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

        // POST: SecondarySchoolUserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SecondarySchoolUserRole secondarySchoolUserRole)
        {
            if (ModelState.IsValid)
            {
                db.SecondarySchoolUserRoles.Add(secondarySchoolUserRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(secondarySchoolUserRole);
        }

        // GET: SecondarySchoolUserRoles/Edit/5
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
            SecondarySchoolUserRole secondarySchoolUserRole = db.SecondarySchoolUserRoles.Find(id);
            if (secondarySchoolUserRole == null)
            {
                return HttpNotFound();
            }
            return View(secondarySchoolUserRole);
        }

        // POST: SecondarySchoolUserRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SecondarySchoolUserRole secondarySchoolUserRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secondarySchoolUserRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secondarySchoolUserRole);
        }

        // GET: SecondarySchoolUserRoles/Delete/5
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
            SecondarySchoolUserRole secondarySchoolUserRole = db.SecondarySchoolUserRoles.Find(id);
            if (secondarySchoolUserRole == null)
            {
                return HttpNotFound();
            }
            return View(secondarySchoolUserRole);
        }

        // POST: SecondarySchoolUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SecondarySchoolUserRole secondarySchoolUserRole = db.SecondarySchoolUserRoles.Find(id);
            db.SecondarySchoolUserRoles.Remove(secondarySchoolUserRole);
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
