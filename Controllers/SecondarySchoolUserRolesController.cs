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
            return View(db.SecondarySchoolUserRoles.ToList());
        }
        [ChildActionOnly]
        public ActionResult AddSecSchRole()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddSecSchRole.cshtml");
        }
        public ActionResult EditSecSchRole(int Id)
        {
            if (Id != 0)
            {
                var edtsecschrl = db.SecondarySchoolUserRoles.Where(x => x.SecondarySchoolUserRoleId == Id).FirstOrDefault();
                var edtsecschrl1 = new SecondarySchoolUserRole
                {
                    SecondarySchoolUserRoleId = edtsecschrl.SecondarySchoolUserRoleId,
                    RoleName = edtsecschrl.RoleName
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditSecSchRole.cshtml", edtsecschrl1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditSecSchRole.cshtml");
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
        // POST: SecondarySchoolUserRoles/Delete/5
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