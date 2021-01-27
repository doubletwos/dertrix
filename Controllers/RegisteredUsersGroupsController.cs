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



        [ChildActionOnly]
        public ActionResult AddMemberToGroup1(int Id)
        {
 
          if (Id != 0)
           {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

            var grp1 = db.OrgGroups
                .Where(x => x.OrgGroupId == Id)
                .Where(x => x.OrgId == i)
                .FirstOrDefault();

            var grp = new RegisteredUsersGroups
            {
                OrgGroupId = grp1.OrgGroupId,
                GroupTypeId = grp1.GroupTypeId

            };

            var orgtype = db.OrgOrgTypes.Where(x => x.OrgId == i).Select(x => x.OrgTypeId).FirstOrDefault();

            // Secondary School
            if (orgtype == 2)
            {
             ViewBag.RegisteredUserId = new SelectList(db.RegisteredUserOrganisations
             .Where(x => x.OrgId == i)
             .Where(k => k.SecondarySchoolUserRoleId != 5)
             .Where(e => e.SecondarySchoolUserRoleId != null), "RegisteredUserId", "FullName");
             ViewBag.OrgGroupId = new SelectList(db.OrgGroups, "OrgGroupId", "GroupName");
            }


             // Primary School
            if (orgtype == 3)
            {
             ViewBag.RegisteredUserId = new SelectList(db.RegisteredUserOrganisations
             .Where(x => x.OrgId == i)
             .Where(k => k.PrimarySchoolUserRoleId != 5)
             .Where(e => e.PrimarySchoolUserRoleId != null), "RegisteredUserId", "FullName");
             ViewBag.OrgGroupId = new SelectList(db.OrgGroups, "OrgGroupId", "GroupName");
            }  

            return PartialView("~/Views/Shared/PartialViewsForms/_AddMemberToGroup1.cshtml", grp);

          }
            return PartialView("~/Views/Shared/PartialViewsForms/_AddMemberToGroup1.cshtml");

        }



        public ActionResult MyGroups(int id)
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var Mygroups = db.RegisteredUsersGroups
                .Where(x => x.RegisteredUserId == id && x.RegUserOrgId == i)
                .Include(x => x.OrgGroup)
                .ToList();

            return PartialView("_MyGroups", Mygroups);
        }








        public ActionResult RegisteredUsersGroupMembers(int id)
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var membercount = db.RegisteredUsersGroups
                .Where(x => x.OrgGroupId == id)
                .Where(x => x.RegUserOrgId == i)
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
                var orgid = Convert.ToInt32(Session["OrgId"]);
                var reguseremail = db.RegisteredUserOrganisations.Where(x => x.OrgId == orgid && x.RegisteredUserId == registeredUsersGroups.RegisteredUserId).Select(x => x.Email).FirstOrDefault();

                registeredUsersGroups.RegUserOrgId = orgid;
                registeredUsersGroups.Email = reguseremail;
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