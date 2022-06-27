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
    public class ReligionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Religions
        [Route("AllReligions")]
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
                return View(db.Religions.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }


        }
        [ChildActionOnly]
        public ActionResult AddReligion()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddReligion.cshtml");
        }

        public ActionResult EditReligion(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var edtrlg = db.Religions.Where(x => x.ReligionId == Id).FirstOrDefault();
                    var edtrlg1 = new Religion
                    {
                        ReligionId = edtrlg.ReligionId,
                        ReligionName = edtrlg.ReligionName
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditReligion.cshtml", edtrlg1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }
        // POST: Religions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Religion religion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Religions.Add(religion);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(religion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(religion);
            }

        }
        // POST: Religions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Religion religion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(religion).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(religion);
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

        //// POST: Religions/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Religion religion = db.Religions.Find(id);
        //    db.Religions.Remove(religion);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}