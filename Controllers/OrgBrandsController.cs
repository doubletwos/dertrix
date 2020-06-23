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
    public class OrgBrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgBrands
        public ActionResult Index()
        {
            return View(db.OrgBrands.ToList());
        }

        // GET: OrgBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgBrand orgBrand = db.OrgBrands.Find(id);
            if (orgBrand == null)
            {
                return HttpNotFound();
            }
            return View(orgBrand);
        }

        // GET: OrgBrands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrgBrands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrgBrandId,OrgBrandName")] OrgBrand orgBrand)
        {
            if (ModelState.IsValid)
            {
                db.OrgBrands.Add(orgBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orgBrand);
        }

        // GET: OrgBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgBrand orgBrand = db.OrgBrands.Find(id);
            if (orgBrand == null)
            {
                return HttpNotFound();
            }
            return View(orgBrand);
        }

        // POST: OrgBrands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrgBrandId,OrgBrandName")] OrgBrand orgBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orgBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgBrand);
        }

        // GET: OrgBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgBrand orgBrand = db.OrgBrands.Find(id);
            if (orgBrand == null)
            {
                return HttpNotFound();
            }
            return View(orgBrand);
        }

        // POST: OrgBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrgBrand orgBrand = db.OrgBrands.Find(id);
            db.OrgBrands.Remove(orgBrand);
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
