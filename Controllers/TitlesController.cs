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
    public class TitlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Titles
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
                return View(db.Titles.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }



        public ActionResult EditTitle(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var edttitle = db.Titles
                        .Where(x => x.TitleId == Id)
                        .FirstOrDefault();
                    var edttitle1 = new Title
                    {
                        TitleId = edttitle.TitleId,
                        TitleName = edttitle.TitleName
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditTitle.cshtml", edttitle1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditTitle.cshtml");
        }


        [ChildActionOnly]
        public ActionResult AddTitle()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddTitle.cshtml");
        }





        // POST: Titles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Title title)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Titles.Add(title);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return View(title);
        }



        // POST: Titles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Title title)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(title).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

            return View(title);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // POST: Titles/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Title title = db.Titles.Find(id);
        //    db.Titles.Remove(title);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
