using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Zeus.Models;

namespace Zeus.Controllers
{
    public class RegisteredUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegisteredUsers/Index
        public ActionResult Index(int? id)
        {
            /* Redirect back to Log in Page if session == null*/
            if (Session["Email"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            /* Populate user list for non superusers if session is not null*/
            if (Session["Email"] != null)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                id = i;
            }

            return View(db.RegisteredUsers
                 .Where(j => j.SelectedOrg == id)
                 .Include(t => t.RegisteredUserType)
                 .ToList());
          
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


        [ChildActionOnly]
        public ActionResult AddUser()
        {
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            return PartialView("_AddUser");
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
        public ActionResult Create(RegisteredUser registeredUser)
        {
            /*Accepting all state of model*/
            if (!(ModelState.IsValid)  || ModelState.IsValid)
            {
                /*When users are added via registration form*/
                if (registeredUser.SelectedOrgList != null)
                {
                    var zeususer = registeredUser.SelectedOrgList.FirstOrDefault().ToString();
                    int i = Convert.ToInt32(zeususer);
                    registeredUser.SelectedOrg = i;
                }

                /*When users are added at school level*/
                if (registeredUser.SelectedOrgList == null)
                {
                    registeredUser.SelectedOrg = (int)Session["OrgId"];
                    var pwd = "iamanewuser";
                    registeredUser.Password = pwd;
                    registeredUser.ConfirmPassword = pwd;
                }
                db.RegisteredUsers.Add(registeredUser);
                db.SaveChanges();

                /*Adding users to the RegUserOrg(Many to Many)*/
                var objRegisteredUserOrganisations = new RegisteredUserOrganisation()
                {
                    OrgId = registeredUser.SelectedOrg,
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
