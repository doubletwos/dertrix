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
    public class Students_Grades_LogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students_Grades_Log
        //public ActionResult Index()
        //{
        //    var students_Grades_Logs = db.Students_Grades_Logs.Include(s => s.Class).Include(s => s.RegisteredUser).Include(s => s.Subject);
        //    return View(students_Grades_Logs.ToList());
        //}

        // GET: Students_Grades_Log/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students_Grades_Log students_Grades_Log = db.Students_Grades_Logs.Find(id);
            if (students_Grades_Log == null)
            {
                return HttpNotFound();
            }
            return View(students_Grades_Log);
        }

        // GET: Students_Grades_Log/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: Students_Grades_Log/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Students_Grades_Log students_Grades_Log)
        {
            if (ModelState.IsValid)
            {
                db.Students_Grades_Logs.Add(students_Grades_Log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", students_Grades_Log.ClassId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", students_Grades_Log.RegisteredUserId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", students_Grades_Log.SubjectId);
            return View(students_Grades_Log);
        }

        // GET: Students_Grades_Log/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students_Grades_Log students_Grades_Log = db.Students_Grades_Logs.Find(id);
            if (students_Grades_Log == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", students_Grades_Log.ClassId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", students_Grades_Log.RegisteredUserId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", students_Grades_Log.SubjectId);
            return View(students_Grades_Log);
        }

        // POST: Students_Grades_Log/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Students_Grades_Log students_Grades_Log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(students_Grades_Log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", students_Grades_Log.ClassId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", students_Grades_Log.RegisteredUserId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", students_Grades_Log.SubjectId);
            return View(students_Grades_Log);
        }

        // GET: Students_Grades_Log/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students_Grades_Log students_Grades_Log = db.Students_Grades_Logs.Find(id);
            if (students_Grades_Log == null)
            {
                return HttpNotFound();
            }
            return View(students_Grades_Log);
        }

        //// POST: Students_Grades_Log/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Students_Grades_Log students_Grades_Log = db.Students_Grades_Logs.Find(id);
        //    db.Students_Grades_Logs.Remove(students_Grades_Log);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
