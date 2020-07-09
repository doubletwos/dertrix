using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zeus.Models;

namespace Zeus.Controllers
{
    public class OrgOrgTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgOrgTypes
        public ActionResult Index()
        {
            var orgOrgTypes = db.OrgOrgTypes.Include(o => o.Org).Include(o => o.OrgType);
            return View(orgOrgTypes.ToList());
        }

        // GET: OrgOrgTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgOrgType orgOrgType = db.OrgOrgTypes.Find(id);
            if (orgOrgType == null)
            {
                return HttpNotFound();
            }
            return View(orgOrgType);
        }

        // GET: OrgOrgTypes/Create
        public ActionResult Create()
        {
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName");
            return View();
        }

        // POST: OrgOrgTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrgOrgType orgOrgType)
        {
            if (ModelState.IsValid)
            {
                db.OrgOrgTypes.Add(orgOrgType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgOrgType.OrgId);
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName", orgOrgType.OrgTypeId);
            return View(orgOrgType);
        }

        // GET: OrgOrgTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgOrgType orgOrgType = db.OrgOrgTypes.Find(id);
            if (orgOrgType == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgOrgType.OrgId);
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName", orgOrgType.OrgTypeId);
            return View(orgOrgType);
        }

        // POST: OrgOrgTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrgOrgTypeId,OrgId,OrgTypeId,OrgName")] OrgOrgType orgOrgType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orgOrgType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgOrgType.OrgId);
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName", orgOrgType.OrgTypeId);
            return View(orgOrgType);
        }

        // GET: OrgOrgTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgOrgType orgOrgType = db.OrgOrgTypes.Find(id);
            if (orgOrgType == null)
            {
                return HttpNotFound();
            }
            return View(orgOrgType);
        }

        // POST: OrgOrgTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrgOrgType orgOrgType = db.OrgOrgTypes.Find(id);
            db.OrgOrgTypes.Remove(orgOrgType);
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
