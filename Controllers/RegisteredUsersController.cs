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
    public class RegisteredUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegisteredUsers
        public ActionResult Index(int? id)
        {

           

            var registeredUsers = db.RegisteredUsers.Include(r => r.RegisteredUserType);
            return View(registeredUsers.ToList());
        }



        [ChildActionOnly]
        public ActionResult Regs()
        {
            return PartialView("_Secure");
        }


        [ChildActionOnly]
        public ActionResult Nav()
        {
            return PartialView("_Nav");
        }

        // GET: RegisteredUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            return View(registeredUser);
        }

        // GET: RegisteredUsers/Create
        public ActionResult Create()
        {
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            ViewBag.SelectedOrgList = new SelectList(db.Orgs, "OrgId", "OrgName");
            return View();
        }

        // POST: RegisteredUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( RegisteredUser registeredUser)
        {
            if (ModelState.IsValid)
            {


                var test = registeredUser.SelectedOrgList.FirstOrDefault().ToString();
                int i = Convert.ToInt32(test);
           
                registeredUser.SelectedOrg = i;






                db.RegisteredUsers.Add(registeredUser);
                db.SaveChanges();

               


                var objRegisteredUserOrganisations = new RegisteredUserOrganisation()
                {
                    OrgId = registeredUser.SelectedOrgList.SingleOrDefault(),
                   RegisteredUserId = registeredUser.RegisteredUserId,
                   Email = registeredUser.Email
                };

                db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            ViewBag.SelectedOrgList = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName", registeredUser.RegisteredUserTypeId);
            return View(registeredUser);
        }

        // GET: RegisteredUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName", registeredUser.RegisteredUserTypeId);
            return View(registeredUser);
        }

        // POST: RegisteredUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( RegisteredUser registeredUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registeredUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName", registeredUser.RegisteredUserTypeId);
            return View(registeredUser);
        }

        // GET: RegisteredUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            return View(registeredUser);
        }

        // POST: RegisteredUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            db.RegisteredUsers.Remove(registeredUser);
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
