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
            if (Request.Browser.IsMobileDevice == true)
            {
                return RedirectToAction("WrongDevice", "Orgs");
            }
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
            return View(db.StudentRegForm.ToList());
        }
        [ChildActionOnly]
        public ActionResult AddStudentType()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddStudentType.cshtml");
        }
        public ActionResult EditStudentType(int Id)
        {
            if (Id != 0)
            {
                var edtstutyp = db.StudentRegForm.Where(x => x.StudentRegFormId == Id).FirstOrDefault();
                var edtstutyp1 = new StudentRegForm
                {
                    StudentRegFormId = edtstutyp.StudentRegFormId,
                    Name = edtstutyp.Name
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditStudentType.cshtml", edtstutyp1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditStudentType.cshtml");
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
        // POST: StudentRegForms/Delete/5
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