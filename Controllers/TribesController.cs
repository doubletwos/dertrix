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
    public class TribesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tribes
        public ActionResult Index()
        {
            return View(db.Tribes.ToList());
        }

        // GET: Tribes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tribe tribe = db.Tribes.Find(id);
            if (tribe == null)
            {
                return HttpNotFound();
            }
            return View(tribe);
        }

        // GET: Tribes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tribes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TribeId,TribeName")] Tribe tribe)
        {
            if (ModelState.IsValid)
            {
                db.Tribes.Add(tribe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tribe);
        }

        // GET: Tribes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tribe tribe = db.Tribes.Find(id);
            if (tribe == null)
            {
                return HttpNotFound();
            }
            return View(tribe);
        }

        // POST: Tribes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TribeId,TribeName")] Tribe tribe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tribe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tribe);
        }

        // GET: Tribes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tribe tribe = db.Tribes.Find(id);
            if (tribe == null)
            {
                return HttpNotFound();
            }
            return View(tribe);
        }

        // POST: Tribes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tribe tribe = db.Tribes.Find(id);
            db.Tribes.Remove(tribe);
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
