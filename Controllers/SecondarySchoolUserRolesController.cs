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
    public class SecondarySchoolUserRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SecondarySchoolUserRoles
        public ActionResult Index()
        {
            return View(db.SecondarySchoolUserRoles.ToList());
        }

        // GET: SecondarySchoolUserRoles/Details/5
        public ActionResult Details(int? id)
        {
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
            return View();
        }

        // POST: SecondarySchoolUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SecondarySchoolUserRoleId,RoleName")] SecondarySchoolUserRole secondarySchoolUserRole)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SecondarySchoolUserRoleId,RoleName")] SecondarySchoolUserRole secondarySchoolUserRole)
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
