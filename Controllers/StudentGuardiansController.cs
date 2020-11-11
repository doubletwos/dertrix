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
    public class StudentGuardiansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentGuardians
        public ActionResult Index()
        {
            var studentGuardians = db.StudentGuardians.Include(s => s.Guardian).Include(s => s.RegisteredUser);
            return View(studentGuardians.ToList());
        }

        // GET: StudentGuardians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGuardian studentGuardian = db.StudentGuardians.Find(id);
            if (studentGuardian == null)
            {
                return HttpNotFound();
            }
            return View(studentGuardian);
        }

        // GET: StudentGuardians/Create
        public ActionResult Create()
        {
            ViewBag.GuardianId = new SelectList(db.Guardians, "GuardianId", "GuardianFirstName");
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName");
            return View();
        }

        // POST: StudentGuardians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentGuardian studentGuardian)
        {
            if (ModelState.IsValid)
            {
                db.StudentGuardians.Add(studentGuardian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuardianId = new SelectList(db.Guardians, "GuardianId", "GuardianFirstName", studentGuardian.GuardianId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", studentGuardian.RegisteredUserId);
            return View(studentGuardian);
        }

        // GET: StudentGuardians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGuardian studentGuardian = db.StudentGuardians.Find(id);
            if (studentGuardian == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuardianId = new SelectList(db.Guardians, "GuardianId", "GuardianFirstName", studentGuardian.GuardianId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", studentGuardian.RegisteredUserId);
            return View(studentGuardian);
        }

        // POST: StudentGuardians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentGuardianId,RegisteredUserId,GuardianId,GuardianFirstName,GuardianLastName,GuardianFullName,GuardianEmailAddress,DateAdded")] StudentGuardian studentGuardian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentGuardian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuardianId = new SelectList(db.Guardians, "GuardianId", "GuardianFirstName", studentGuardian.GuardianId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", studentGuardian.RegisteredUserId);
            return View(studentGuardian);
        }

        // GET: StudentGuardians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGuardian studentGuardian = db.StudentGuardians.Find(id);
            if (studentGuardian == null)
            {
                return HttpNotFound();
            }
            return View(studentGuardian);
        }

        // POST: StudentGuardians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentGuardian studentGuardian = db.StudentGuardians.Find(id);
            db.StudentGuardians.Remove(studentGuardian);
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
