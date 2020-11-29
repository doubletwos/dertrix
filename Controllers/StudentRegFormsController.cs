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
    public class StudentRegFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentRegForms
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

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.StudentRegForm.ToList());
        }

        // GET: StudentRegForms/Details/5
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

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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

        // POST: StudentRegForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentRegForm studentRegForm)
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
            StudentRegForm studentRegForm = db.StudentRegForm.Find(id);
            if (studentRegForm == null)
            {
                return HttpNotFound();
            }
            return View(studentRegForm);
        }

        // POST: StudentRegForms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentRegForm studentRegForm)
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
