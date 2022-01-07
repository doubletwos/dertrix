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
            try
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
                        .Where(e => e.SecondarySchoolUserRoleId != null), "RegisteredUserId", "FullName");
                        ViewBag.OrgGroupId = new SelectList(db.OrgGroups, "OrgGroupId", "GroupName");
                    }


                    // Primary School
                    if (orgtype == 3)
                    {
                        ViewBag.RegisteredUserId = new SelectList(db.RegisteredUserOrganisations
                        .Where(x => x.OrgId == i)
                        .Where(e => e.PrimarySchoolUserRoleId != null), "RegisteredUserId", "FullName");
                        ViewBag.OrgGroupId = new SelectList(db.OrgGroups, "OrgGroupId", "GroupName");
                    }

                    // Nursery School
                    if (orgtype == 4)
                    {
                        ViewBag.RegisteredUserId = new SelectList(db.RegisteredUserOrganisations
                        .Where(x => x.OrgId == i)
                        .Where(e => e.NurserySchoolUserRoleId != null), "RegisteredUserId", "FullName");
                        ViewBag.OrgGroupId = new SelectList(db.OrgGroups, "OrgGroupId", "GroupName");
                    }

                    return PartialView("~/Views/Shared/PartialViewsForms/_AddMemberToGroup1.cshtml", grp);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_AddMemberToGroup1.cshtml");

        }






        public ActionResult MyGroups(int id)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var Mygroups = db.RegisteredUsersGroups
                    .Where(x => x.RegisteredUserId == id && x.RegUserOrgId == i)
                    .Include(x => x.OrgGroup)
                    .ToList();

                return PartialView("_MyGroups", Mygroups);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
 
        }





        // Show list of registeredusers in a group 
        public ActionResult RegisteredUsersGroupMembers(int id)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var membercount = db.RegisteredUsersGroups
                    .Where(x => x.OrgGroupId == id)
                    .Where(x => x.RegUserOrgId == i)
                    .Include(r => r.RegisteredUser)
                    .Include(l => l.RegisteredUser.SecondarySchoolUserRole)
                    .Include(l => l.RegisteredUser.PrimarySchoolUserRole)
                    .Include(l => l.RegisteredUser.NurserySchoolUserRole)
                    .ToList();
                return PartialView("_RegisteredUsersGroupMembers", membercount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }




        // POST: RegisteredUsersGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisteredUsersGroups registeredUsersGroups)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var orgid = Convert.ToInt32(Session["OrgId"]);
                    var reguseremail = db.RegisteredUserOrganisations
                        .Where(x => x.OrgId == orgid && x.RegisteredUserId == registeredUsersGroups.RegisteredUserId)
                        .Select(x => x.Email)
                        .FirstOrDefault();

                    registeredUsersGroups.RegUserOrgId = orgid;
                    registeredUsersGroups.Email = reguseremail;
                    db.RegisteredUsersGroups.Add(registeredUsersGroups);
                    db.SaveChanges();
                    var updateclasses = UpdateGroupMemberCount(registeredUsersGroups.OrgGroupId, orgid);

                    return RedirectToAction("Index", "OrgGroups");
                }
                ViewBag.OrgGroupId = new SelectList(db.OrgGroups, "OrgGroupId", "GroupName", registeredUsersGroups.OrgGroupId);
                ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", registeredUsersGroups.RegisteredUserId);
                return View(registeredUsersGroups);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(registeredUsersGroups);
            }
        }



        // POST: RegisteredUsersGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisteredUsersGroups registeredUsersGroups)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }



        public ActionResult UpdateGroupMemberCount(int grpid, int? orgid)
        {
            try
            {
                // get group count
                var grpmembcount = db.RegisteredUsersGroups.Where(x => x.OrgGroupId == grpid).Where(x => x.RegUserOrgId == orgid).Count();

                // locate recently updated group
                var orggroup = db.OrgGroups.AsNoTracking().Where(x => x.OrgId == orgid && x.OrgGroupId == grpid).FirstOrDefault();

                var updategroup = new OrgGroup
                {
                    OrgGroupId = orggroup.OrgGroupId,
                    OrgId = orggroup.OrgId,
                    GroupName = orggroup.GroupName,
                    CreationDate = orggroup.CreationDate,
                    GroupTypeId = orggroup.GroupTypeId,
                    GroupOrgTypeId = orggroup.GroupOrgTypeId,
                    GroupRefNumb = orggroup.GroupRefNumb,
                    IsSelected = orggroup.IsSelected,
                    Group_members_count = grpmembcount,
                };

                orggroup = updategroup;
                db.Entry(orggroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
      
        }







        // POST: RegisteredUsersGroups/Delete/5
        public ActionResult DeleteConfirmed(int id, int grpid)
        {
            try
            {
                var orgid = Convert.ToInt32(Session["OrgId"]);
                var user = db.RegisteredUsersGroups.Where(x => x.RegisteredUserId == id).Select(j => j.RegisteredUsersGroupsId).FirstOrDefault();
                RegisteredUsersGroups registeredUsersGroups = db.RegisteredUsersGroups.Find(user);
                db.RegisteredUsersGroups.Remove(registeredUsersGroups);
                db.SaveChanges();
                var updateclasses = UpdateGroupMemberCount(registeredUsersGroups.OrgGroupId, orgid);
                return RedirectToAction("Index", "OrgGroups");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

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