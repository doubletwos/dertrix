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
    public class RegisteredUserTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: RegisteredUserTypes
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
            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.RegisteredUserTypes.ToList());
        }
        [ChildActionOnly]
        public ActionResult AddRegisteredUserType()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddRegisteredUserType.cshtml");
        }

        public ActionResult EditRegUserType(int Id)
        {
            if (Id != 0)
            {
                var edtregusetype = db.RegisteredUserTypes
                    .Where(x => x.RegisteredUserTypeId == Id)
                    .FirstOrDefault();
                var regusrtype1 = new RegisteredUserType
                {
                    RegisteredUserTypeId = edtregusetype.RegisteredUserTypeId,
                    RegisteredUserTypeName = edtregusetype.RegisteredUserTypeName
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditRegUserType.cshtml", regusrtype1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditRegUserType.cshtml");
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
        // POST: RegisteredUserTypes/Delete/5
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