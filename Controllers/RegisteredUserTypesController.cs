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
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(db.RegisteredUserTypes.ToList());
        }


        // GET: RegisteredUserTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 3)
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
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        // POST: RegisteredUserTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisteredUserType registeredUserType)
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
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 3)
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
            RegisteredUserType registeredUserType = db.RegisteredUserTypes.Find(id);
            if (registeredUserType == null)
            {
                return HttpNotFound();
            }
            return View(registeredUserType);
        }

        // POST: RegisteredUserTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisteredUserType registeredUserType)
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
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 3)
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
