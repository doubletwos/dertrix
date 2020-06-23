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
    public class OrgTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgTypes
        public ActionResult Index()
        {
            return View(db.OrgTypes.ToList());
        }

        // GET: OrgTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgType orgType = db.OrgTypes.Find(id);
            if (orgType == null)
            {
                return HttpNotFound();
            }
            return View(orgType);
        }

        // GET: OrgTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrgTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrgTypeId,OrgTypeName")] OrgType orgType)
        {
            if (ModelState.IsValid)
            {
                db.OrgTypes.Add(orgType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orgType);
        }

        // GET: OrgTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgType orgType = db.OrgTypes.Find(id);
            if (orgType == null)
            {
                return HttpNotFound();
            }
            return View(orgType);
        }

        // POST: OrgTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrgTypeId,OrgTypeName")] OrgType orgType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orgType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgType);
        }

        // GET: OrgTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgType orgType = db.OrgTypes.Find(id);
            if (orgType == null)
            {
                return HttpNotFound();
            }
            return View(orgType);
        }

        // POST: OrgTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
