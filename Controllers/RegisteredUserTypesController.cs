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
    public class RegisteredUserTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegisteredUserTypes
        public ActionResult Index()
        {
            return View(db.RegisteredUserTypes.ToList());
        }

        // GET: RegisteredUserTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUserType registeredUserType = db.RegisteredUserTypes.Find(id);
            if (registeredUserType == null)
            {
                return HttpNotFound();
            }
            return View(registeredUserType);
        }

        // GET: RegisteredUserTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisteredUserTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegisteredUserTypeId,RegisteredUserTypeName")] RegisteredUserType registeredUserType)
        {
            if (ModelState.IsValid)
            {
                db.RegisteredUserTypes.Add(registeredUserType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registeredUserType);
        }

        // GET: RegisteredUserTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUserType registeredUserType = db.RegisteredUserTypes.Find(id);
            if (registeredUserType == null)
            {
                return HttpNotFound();
            }
            return View(registeredUserType);
        }

        // POST: RegisteredUserTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegisteredUserTypeId,RegisteredUserTypeName")] RegisteredUserType registeredUserType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registeredUserType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registeredUserType);
        }

        // GET: RegisteredUserTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUserType registeredUserType = db.RegisteredUserTypes.Find(id);
            if (registeredUserType == null)
            {
                return HttpNotFound();
            }
            return View(registeredUserType);
        }

        // POST: RegisteredUserTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisteredUserType registeredUserType = db.RegisteredUserTypes.Find(id);
            db.RegisteredUserTypes.Remove(registeredUserType);
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
