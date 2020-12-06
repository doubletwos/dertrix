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
                return RedirectToAction("Signin", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.Domains.ToList());
        }

        [ChildActionOnly]
        public ActionResult AddDomain()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddDomain.cshtml");
        }

        public ActionResult EditDomain(int Id)
        {
            if (Id != 0)
            {
                var edtdomain = db.Domains
                    .Where(x => x.DomainId == Id)
                    .FirstOrDefault();
                var edtdomain1 = new Domain
                {
                    DomainId = edtdomain.DomainId,
                    DomainName = edtdomain.DomainName
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditDomain.cshtml", edtdomain1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditDomain.cshtml");
        }

        // POST: Domains/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Domain domain)
        {
            if (ModelState.IsValid)
            {
                db.Domains.Add(domain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(domain);
        }

        // POST: Domains/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Domain domain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(domain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(domain);
        }
        
        // POST: Domains/Delete/5
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