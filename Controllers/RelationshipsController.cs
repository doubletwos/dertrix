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
    public class RelationshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Relationships
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
            return View(db.Relationships.ToList());
        }

        [ChildActionOnly]
        public ActionResult AddRelationship()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddRelationship.cshtml");
        }
        public ActionResult EditRelationship(int Id)
        {
            if (Id != 0)
            {
                var edtrls = db.Relationships.Where(x => x.RelationshipId == Id).FirstOrDefault();
                var edtrls1 = new Relationship
                {
                    RelationshipId = edtrls.RelationshipId,
                    RelationshipName = edtrls.RelationshipName
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditRelationship.cshtml", edtrls1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditRelationship.cshtml");
        }

        // GET: Relationships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relationship relationship = db.Relationships.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        // GET: Relationships/Create
        public ActionResult Create()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: Relationships/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Relationship relationship)
        {
            if (ModelState.IsValid)
            {
                db.Relationships.Add(relationship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(relationship);
        }

        // GET: Relationships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relationship relationship = db.Relationships.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        // POST: Relationships/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RelationshipId,RelationshipName")] Relationship relationship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relationship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(relationship);
        }

        // GET: Relationships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relationship relationship = db.Relationships.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        // POST: Relationships/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Relationship relationship = db.Relationships.Find(id);
            db.Relationships.Remove(relationship);
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
