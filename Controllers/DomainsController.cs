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
    public class DomainsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Domains
        public ActionResult Index()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            return View(db.Domains.ToList());
        }

        // GET: Domains/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain domain = db.Domains.Find(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        // GET: Domains/Create
        public ActionResult Create()
        {
            //if (Session["OrgId"] == null)
            //{
            //    return RedirectToAction("Index", "Access");
            //}

            //if ((int)Session["OrgId"] != 23)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            return View();
        }

        // POST: Domains/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Domain domain)
        {
            if (ModelState.IsValid)
            {
                db.Domains.Add(domain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(domain);
        }

        // GET: Domains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain domain = db.Domains.Find(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        // POST: Domains/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Domain domain)
        {
       
            if (ModelState.IsValid)
            {
                db.Entry(domain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(domain);
        }

        // GET: Domains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain domain = db.Domains.Find(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        // POST: Domains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Domain domain = db.Domains.Find(id);
            db.Domains.Remove(domain);
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
