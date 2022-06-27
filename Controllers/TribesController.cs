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
    public class TribesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Tribes
        [Route("AllTribes")]
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
                return View(db.Tribes.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }
        [ChildActionOnly]
        public ActionResult AddTribe()
        {
            try
            {
                return PartialView("~/Views/Shared/PartialViewsForms/_AddTribe.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }
        public ActionResult EditTribe(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var edttrb = db.Tribes.Where(x => x.TribeId == Id).FirstOrDefault();
                    var edttrb1 = new Tribe
                    {
                        TribeId = edttrb.TribeId,
                        TribeName = edttrb.TribeName
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditTribe.cshtml", edttrb1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditTribe.cshtml");
        }
        // POST: Tribes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tribe tribe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Tribes.Add(tribe);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(tribe);
            }

            return View(tribe);
        }

        // POST: Tribes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tribe tribe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tribe).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

            return View(tribe);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // POST: Tribes/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Tribe tribe = db.Tribes.Find(id);
        //    db.Tribes.Remove(tribe);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}