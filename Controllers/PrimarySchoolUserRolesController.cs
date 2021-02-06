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
            return View(db.PrimarySchoolUserRoles.ToList());
        }

        [ChildActionOnly]
        public ActionResult AddPriSchRole()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddPriSchRole.cshtml");
        }

        public ActionResult EditPriSchRole(int Id)
        {
            if (Id != 0)
            {
                var edtprischrl = db.PrimarySchoolUserRoles.Where(x => x.PrimarySchoolUserRoleID == Id).FirstOrDefault();
                var edtprischrl1 = new PrimarySchoolUserRole
                {
                    PrimarySchoolUserRoleID = edtprischrl.PrimarySchoolUserRoleID,
                    RoleName = edtprischrl.RoleName
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditPriSchRole.cshtml", edtprischrl1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditPriSchRole.cshtml");
        }

        // POST: PrimarySchoolUserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrimarySchoolUserRole primarySchoolUserRole)
        {
            if (ModelState.IsValid)
            {
                db.PrimarySchoolUserRoles.Add(primarySchoolUserRole);
                db.SaveChanges();
                return RedirectToAction("Index");
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

        // POST: PrimarySchoolUserRoles/Delete/5
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