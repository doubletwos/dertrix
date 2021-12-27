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
    public class GendersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [ChildActionOnly]
        public ActionResult AddGender()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddGender.cshtml");
        }


        // POST: Genders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gender gender)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                if ((int)Session["OrgId"] != 23)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if ((int)Session["OrgId"] == 23)
                {
                    if (ModelState.IsValid)
                    {
                        db.Genders.Add(gender);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        // POST: Genders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gender gender)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                if ((int)Session["OrgId"] != 23)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if ((int)Session["OrgId"] == 23)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(gender).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        public ActionResult EditGender(int Id)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                if ((int)Session["OrgId"] != 23)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if ((int)Session["OrgId"] == 23)
                {
                    if (Id != 0)
                    {
                        var edtgndr = db.Genders.Where(x => x.GenderId == Id).FirstOrDefault();
                        var edtgndr1 = new Gender
                        {
                            GenderId = edtgndr.GenderId,
                            GenderName = edtgndr.GenderName
                        };
                        return PartialView("~/Views/Shared/PartialViewsForms/_EditGender.cshtml", edtgndr1);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

            return new HttpStatusCodeResult(204);
        }

        // GET: Genders
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
                if ((int)Session["OrgId"] == 23)
                {
                    return View(db.Genders.ToList());
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

        //// POST: Genders/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        if (Session["OrgId"] == null)
        //        {
        //            return RedirectToAction("Signin", "Access");
        //        }
        //        if ((int)Session["OrgId"] != 23)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        if ((int)Session["OrgId"] == 23)
        //        {
        //            Gender gender = db.Genders.Find(id);
        //            db.Genders.Remove(gender);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return Redirect("~/ErrorHandler.html");
        //    }
        //    return new HttpStatusCodeResult(204);
        //}
    }
}