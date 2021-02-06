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
    public class OrgImportantDatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgImportantDates
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

            return View(db.OrgImportantDates.ToList());
        }


        [ChildActionOnly]
        public ActionResult AddOrgImpDate()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddOrgImpDate.cshtml");
        }

        public ActionResult EditImpDate(int Id)
        {
            if (Id != 0)
            {
                var edtimpdate = db.OrgImportantDates
                    .Where(x => x.OrgImportantDateId == Id)
                    .FirstOrDefault();
                var edtimpdate1 = new OrgImportantDate
                {
                    OrgImportantDateId = edtimpdate.OrgImportantDateId,
                     ImportantDateName = edtimpdate.ImportantDateName,
                     OrgId = edtimpdate.OrgId,
                     CreatedBy = edtimpdate.CreatedBy,
                     CreatorName = edtimpdate.CreatorName,
                    FromImportanttDate = edtimpdate.FromImportanttDate,
                    ToImportanttDate = edtimpdate.ToImportanttDate
                };

                return PartialView("~/Views/Shared/PartialViewsForms/_EditImpDate.cshtml", edtimpdate1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditImpDate.cshtml");
        }



        //  GET: OrgImportantDates/ImpDateDisplayPanel
        [ChildActionOnly]
        public ActionResult ImpDateDisplayPanel()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var impdate = db.OrgImportantDates.Where(x => x.OrgId == i);
            return PartialView("~/Views/Shared/PartialViewsForms/_ImpDateDisplayPanel.cshtml", impdate);
        }



        // GET: OrgImportantDates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgImportantDate orgImportantDate = db.OrgImportantDates.Find(id);
            if (orgImportantDate == null)
            {
                return HttpNotFound();
            }
            return View(orgImportantDate);
        }

        // GET: OrgImportantDates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrgImportantDates/Create
        [HttpPost]
        public ActionResult Create(OrgImportantDate orgImportantDate)
        {
          
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
                orgImportantDate.OrgId = i;
                orgImportantDate.CreatedBy = RegisteredUserId.ToString();
                orgImportantDate.CreatorName = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
                db.OrgImportantDates.Add(orgImportantDate);
                db.SaveChanges();
                return RedirectToAction("Index");
      
        }

        // GET: OrgImportantDates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgImportantDate orgImportantDate = db.OrgImportantDates.Find(id);
            if (orgImportantDate == null)
            {
                return HttpNotFound();
            }
            return View(orgImportantDate);
        }

        // POST: OrgImportantDates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrgImportantDate orgImportantDate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orgImportantDate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgImportantDate);
        }

        // GET: OrgImportantDates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgImportantDate orgImportantDate = db.OrgImportantDates.Find(id);
            if (orgImportantDate == null)
            {
                return HttpNotFound();
            }
            return View(orgImportantDate);
        }

        // POST: OrgImportantDates/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            OrgImportantDate orgImportantDate = db.OrgImportantDates.Find(id);
            db.OrgImportantDates.Remove(orgImportantDate);
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
