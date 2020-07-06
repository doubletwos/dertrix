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
    public class StudentRegFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentRegForms
        public ActionResult Index()
        {
            return View(db.StudentRegForm.ToList());
        }

        // GET: StudentRegForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRegForm studentRegForm = db.StudentRegForm.Find(id);
            if (studentRegForm == null)
            {
                return HttpNotFound();
            }
            return View(studentRegForm);
        }

        // GET: StudentRegForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentRegForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentRegFormId,Name")] StudentRegForm studentRegForm)
        {
            if (ModelState.IsValid)
            {
                db.StudentRegForm.Add(studentRegForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentRegForm);
        }

        // GET: StudentRegForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRegForm studentRegForm = db.StudentRegForm.Find(id);
            if (studentRegForm == null)
            {
                return HttpNotFound();
            }
            return View(studentRegForm);
        }

        // POST: StudentRegForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentRegFormId,Name")] StudentRegForm studentRegForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentRegForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentRegForm);
        }

        // GET: StudentRegForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRegForm studentRegForm = db.StudentRegForm.Find(id);
            if (studentRegForm == null)
            {
                return HttpNotFound();
            }
            return View(studentRegForm);
        }

        // POST: StudentRegForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentRegForm studentRegForm = db.StudentRegForm.Find(id);
            db.StudentRegForm.Remove(studentRegForm);
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
