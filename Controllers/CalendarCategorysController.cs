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
    public class CalendarCategorysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [ChildActionOnly]
        public ActionResult AddCalendarCategory()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddCalendarCategory.cshtml");
        }

        // POST: CalendarCategorys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CalendarCategory calendarCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CalendarCategorys.Add(calendarCategory);
                    db.SaveChanges();
                    return RedirectToAction("Table");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(calendarCategory);
            }
            return View(calendarCategory);
        }

        // POST: CalendarCategorys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CalendarCategory calendarCategory)
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
                if (ModelState.IsValid)
                {
                    db.Entry(calendarCategory).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Table");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return View(calendarCategory);
        }

        // GET: CalendarCategorys/EditCategory/5
        public ActionResult EditCategory(int? id)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                if (id != 0)
                {
                    var edtCategory = db.CalendarCategorys.Where(x => x.CalendarCategoryId == id).FirstOrDefault();
                    var edtCategory1 = new CalendarCategory
                    {
                        CalendarCategoryId = edtCategory.CalendarCategoryId,
                        CategoryName = edtCategory.CategoryName
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditCalendarCategory.cshtml", edtCategory1);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditCalendarCategory.cshtml");
        }


        // GET: CalendarCategorys
        public ActionResult Table() 
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(db.CalendarCategorys.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        //// POST: CalendarCategorys/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CalendarCategory calendarCategory = db.CalendarCategorys.Find(id);
        //    db.CalendarCategorys.Remove(calendarCategory);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


    }
}
