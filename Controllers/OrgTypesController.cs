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
    public class OrgTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgTypes
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
            return View(db.OrgTypes.ToList());
        }


        [ChildActionOnly]
        public ActionResult AddOrgType()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddOrgType.cshtml");
        }

        public ActionResult EditOrgType(int Id)
        {
            if (Id != 0)
            {
                var edtorgtype = db.OrgTypes
                    .Where(x => x.OrgTypeId == Id)
                    .FirstOrDefault();
                var edtorgtype1 = new OrgType
                {
                    OrgTypeId = edtorgtype.OrgTypeId,
                    OrgTypeName = edtorgtype.OrgTypeName
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditOrgType.cshtml", edtorgtype1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditOrgType.cshtml");
        }

        // POST: OrgTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrgType orgType)
        {
            if (ModelState.IsValid)
            {
                db.OrgTypes.Add(orgType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgType);
        }

        // POST: OrgTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrgType orgType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orgType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgType);
        }


        public ActionResult DeleteConfirmed(int id)
        {
            OrgType orgType = db.OrgTypes.Find(id);
            db.OrgTypes.Remove(orgType);
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