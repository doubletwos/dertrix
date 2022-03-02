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
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
 
        }

        [ChildActionOnly]
        public ActionResult AddSecSchRole()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddSecSchRole.cshtml");
        }

        public ActionResult EditSecSchRole(int Id)
        {
            try
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }


        // POST: SecondarySchoolUserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SecondarySchoolUserRole secondarySchoolUserRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.SecondarySchoolUserRoles.Add(secondarySchoolUserRole);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(secondarySchoolUserRole);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(secondarySchoolUserRole);
            }
        }


        // POST: SecondarySchoolUserRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SecondarySchoolUserRole secondarySchoolUserRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(secondarySchoolUserRole).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(secondarySchoolUserRole);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // POST: SecondarySchoolUserRoles/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SecondarySchoolUserRole secondarySchoolUserRole = db.SecondarySchoolUserRoles.Find(id);
        //    db.SecondarySchoolUserRoles.Remove(secondarySchoolUserRole);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}