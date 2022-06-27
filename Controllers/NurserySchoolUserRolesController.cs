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
    public class NurserySchoolUserRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NurserySchoolUserRoles
        [Route("NurserySchoolUserRoles")]
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
                return View(db.NurserySchoolUserRoles.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        [ChildActionOnly]
        public ActionResult AddNursSchRole()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddNursSchRole.cshtml");
        }

        public ActionResult EditNursSchRole(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var edtsecschrl = db.NurserySchoolUserRoles.Where(x => x.NurserySchoolUserRoleId == Id).FirstOrDefault();
                    var edtsecschrl1 = new NurserySchoolUserRole
                    {
                        NurserySchoolUserRoleId = edtsecschrl.NurserySchoolUserRoleId,
                        RoleName = edtsecschrl.RoleName
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditNursSchRole.cshtml", edtsecschrl1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }





        // POST: NurserySchoolUserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NurserySchoolUserRole nurserySchoolUserRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.NurserySchoolUserRoles.Add(nurserySchoolUserRole);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(nurserySchoolUserRole);
            }
            return View(nurserySchoolUserRole);
        }



        // POST: NurserySchoolUserRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NurserySchoolUserRole nurserySchoolUserRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(nurserySchoolUserRole).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nurserySchoolUserRole);
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

        //// POST: NurserySchoolUserRoles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    NurserySchoolUserRole nurserySchoolUserRole = db.NurserySchoolUserRoles.Find(id);
        //    db.NurserySchoolUserRoles.Remove(nurserySchoolUserRole);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
