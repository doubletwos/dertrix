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
                return View(db.PrimarySchoolUserRoles.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        [ChildActionOnly]
        public ActionResult AddPriSchRole()
        {
            try
            {
                return PartialView("~/Views/Shared/PartialViewsForms/_AddPriSchRole.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        public ActionResult EditPriSchRole(int Id)
        {
            try
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }


        // POST: PrimarySchoolUserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrimarySchoolUserRole primarySchoolUserRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PrimarySchoolUserRoles.Add(primarySchoolUserRole);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(primarySchoolUserRole);
            }
            return View(primarySchoolUserRole);
        }

        // POST: PrimarySchoolUserRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PrimarySchoolUserRole primarySchoolUserRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(primarySchoolUserRole).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //// POST: PrimarySchoolUserRoles/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        PrimarySchoolUserRole primarySchoolUserRole = db.PrimarySchoolUserRoles.Find(id);
        //        db.PrimarySchoolUserRoles.Remove(primarySchoolUserRole);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return Redirect("~/ErrorHandler.html");
        //    }
        //}
    }
}