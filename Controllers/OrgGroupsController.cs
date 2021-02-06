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
    public class OrgGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgGroups
        public ActionResult Index()
        {
            if (Request.Browser.IsMobileDevice == true)
            {
                return RedirectToAction("WrongDevice", "Orgs");
            }
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Welcome", "Access");
            }
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var orgGroups = db.OrgGroups.Where(x => x.OrgId == i).Include(o => o.GroupType).Include(o => o.Org);
            return View(orgGroups.ToList());
        }

        // GET: OrgGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgGroup orgGroup = db.OrgGroups.Find(id);
            if (orgGroup == null)
            {
                return HttpNotFound();
            }
            return View(orgGroup);
        }

        // GET: OrgGroups/Create
        public ActionResult Create()
        {
            ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "GroupTypeId", "GroupTypeName");
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            return View();
        }

        // POST: OrgGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrgGroup orgGroup)
        {
            if (ModelState.IsValid)
            {
                db.OrgGroups.Add(orgGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "GroupTypeId", "GroupTypeName", orgGroup.GroupTypeId);
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgGroup.OrgId);
            return View(orgGroup);
        }

        // GET: OrgGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgGroup orgGroup = db.OrgGroups.Find(id);
            if (orgGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "GroupTypeId", "GroupTypeName", orgGroup.GroupTypeId);
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgGroup.OrgId);
            return View(orgGroup);
        }

        // POST: OrgGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( OrgGroup orgGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orgGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "GroupTypeId", "GroupTypeName", orgGroup.GroupTypeId);
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgGroup.OrgId);
            return View(orgGroup);
        }

        // GET: OrgGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgGroup orgGroup = db.OrgGroups.Find(id);
            if (orgGroup == null)
            {
                return HttpNotFound();
            }
            return View(orgGroup);
        }

        // POST: OrgGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrgGroup orgGroup = db.OrgGroups.Find(id);
            db.OrgGroups.Remove(orgGroup);
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
