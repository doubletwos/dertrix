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
    public class PrimarySchoolUserRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrimarySchoolUserRoles
        public ActionResult Index()
        {
            return View(db.PrimarySchoolUserRoles.ToList());
        }

        // GET: PrimarySchoolUserRoles/Details/5
        public ActionResult Details(int? id)
        {
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
            return View();
        }

        // POST: PrimarySchoolUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrimarySchoolUserRoleID,RoleName")] PrimarySchoolUserRole primarySchoolUserRole)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrimarySchoolUserRoleID,RoleName")] PrimarySchoolUserRole primarySchoolUserRole)
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
