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
    [RoutePrefix("")]
    public class StudentRegFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: StudentRegForms
        [Route("AllStudentTypes")]
        public ActionResult Index()
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }
        [ChildActionOnly]
        public ActionResult AddStudentType()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddStudentType.cshtml");
        }
        public ActionResult EditStudentType(int Id)
        {
            try
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditStudentType.cshtml");
        }
        // POST: StudentRegForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentRegForm studentRegForm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.StudentRegForm.Add(studentRegForm);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(studentRegForm);
            }
            return View(studentRegForm);
        }


        // POST: StudentRegForms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentRegForm studentRegForm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(studentRegForm).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return View(studentRegForm);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //// POST: StudentRegForms/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    StudentRegForm studentRegForm = db.StudentRegForm.Find(id);
        //    db.StudentRegForm.Remove(studentRegForm);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}