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
    public class RegisteredUsersGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AddMemberToGroup(int Id)
        {
            if (Id != 0)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var grp1 = db.OrgGroups
                    .Where(x => x.OrgGroupId == Id)
                    .FirstOrDefault();
                ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers.Where(x => x.SelectedOrg == i).Where(k => k.StudentRegFormId == null), "RegisteredUserId", "FullName");
                ViewBag.OrgGroupId = new SelectList(db.OrgGroups, "OrgGroupId", "GroupName");
                var grp = new RegisteredUsersGroups
                {
                    OrgGroupId = grp1.OrgGroupId,
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_AddMemberToGroup.cshtml", grp);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_AddMemberToGroup.cshtml");
        }

        public ActionResult RegisteredUsersGroupMembers(int id)
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var membercount = db.RegisteredUsersGroups
                .Where(x => x.OrgGroupId == id)
                .Include(r => r.RegisteredUser)
                .Include(l => l.RegisteredUser.SecondarySchoolUserRole)
                .Include(l => l.RegisteredUser.PrimarySchoolUserRole)
                .ToList();
            return PartialView("_RegisteredUsersGroupMembers", membercount);
        }

        // POST: RegisteredUsersGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisteredUsersGroups registeredUsersGroups)
        {
            if (ModelState.IsValid)
            {
                db.RegisteredUsersGroups.Add(registeredUsersGroups);
                db.SaveChanges();
                return RedirectToAction("Index", "OrgGroups");
            }
            ViewBag.OrgGroupId = new SelectList(db.OrgGroups, "OrgGroupId", "GroupName", registeredUsersGroups.OrgGroupId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", registeredUsersGroups.RegisteredUserId);
            return View(registeredUsersGroups);
        }

        // POST: RegisteredUsersGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisteredUsersGroups registeredUsersGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registeredUsersGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrgGroupId = new SelectList(db.OrgGroups, "OrgGroupId", "GroupName", registeredUsersGroups.OrgGroupId);
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", registeredUsersGroups.RegisteredUserId);
            return View(registeredUsersGroups);
        }

        // POST: RegisteredUsersGroups/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.RegisteredUsersGroups.Where(x => x.RegisteredUserId == id).Select(j => j.RegisteredUsersGroupsId).FirstOrDefault();
            RegisteredUsersGroups registeredUsersGroups = db.RegisteredUsersGroups.Find(user);
            db.RegisteredUsersGroups.Remove(registeredUsersGroups);
            db.SaveChanges();
            return RedirectToAction("Index", "OrgGroups");
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