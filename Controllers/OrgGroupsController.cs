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
    [RoutePrefix("")]
    public class OrgGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgGroups
        [Route("AllGroups")]
        public ActionResult Index()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            try
            {
                if (Request.Browser.IsMobileDevice == true)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Welcome", "Access");
                }
                else
                {
                    var orgGroups = db.OrgGroups.Where(x => x.OrgId == i).Include(o => o.GroupType).Include(o => o.Org);
                    return View(orgGroups.ToList());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        [ChildActionOnly]
        public ActionResult AddCustomGroup()
        {
            try
            {
                ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "GroupTypeId", "GroupTypeName");
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");

                return PartialView("~/Views/Shared/PartialViewsForms/_AddCustomGroup.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }



        // POST: OrgGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrgGroup orgGroup)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);


                orgGroup.OrgId = i;
                orgGroup.GroupName = orgGroup.GroupName;
                orgGroup.CreationDate = DateTime.Now;
                orgGroup.GroupTypeId = 18;
                orgGroup.Group_members_count = 0;
                db.OrgGroups.Add(orgGroup);
                db.SaveChanges();

                // UPON CREATING A GROUP - LOG THE EVENT 
                var orgeventlog = new Org_Events_Log()
                {
                    Org_Event_SubjectId = orgGroup.OrgGroupId.ToString(),
                    Org_Event_SubjectName = orgGroup.GroupName,
                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                    Org_Event_Time = DateTime.Now,
                    OrgId = Session["OrgId"].ToString(),
                    Org_Events_Types = Org_Events_Types.Created_Group
                };
                db.Org_Events_Logs.Add(orgeventlog);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(orgGroup);
            }
            return new HttpStatusCodeResult(204);

        }


        public ActionResult EditCustomGroup(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var edtcusmgrp = db.OrgGroups.Where(x => x.OrgGroupId == Id).FirstOrDefault();
                    var edtcusmgrp1 = new OrgGroup
                    {
                        OrgGroupId = edtcusmgrp.OrgGroupId,
                        OrgId = edtcusmgrp.OrgId,
                        GroupName = edtcusmgrp.GroupName,
                        CreationDate = edtcusmgrp.CreationDate,
                        GroupOrgTypeId = edtcusmgrp.GroupOrgTypeId,
                        GroupTypeId = edtcusmgrp.GroupTypeId,
                        GroupRefNumb = edtcusmgrp.GroupRefNumb,
                        IsSelected = edtcusmgrp.IsSelected,
                        Group_members_count = edtcusmgrp.Group_members_count
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditCustomGroup.cshtml", edtcusmgrp1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }





        // POST: OrgGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrgGroup orgGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(orgGroup).State = EntityState.Modified;
                    db.SaveChanges();

                    // UPON CREATING A GROUP - LOG THE EVENT 
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_SubjectId = orgGroup.OrgGroupId.ToString(),
                        Org_Event_SubjectName = orgGroup.GroupName,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString(),
                        Org_Events_Types = Org_Events_Types.Edited_Group
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.GroupTypeId = new SelectList(db.GroupTypes, "GroupTypeId", "GroupTypeName", orgGroup.GroupTypeId);
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgGroup.OrgId);
                return View(orgGroup);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        // POST: OrgGroups/Delete/5     
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                OrgGroup orgGroup = db.OrgGroups.Find(id);
                db.OrgGroups.Remove(orgGroup);
                db.SaveChanges();

                // UPON DELETING A GROUP - LOG THE EVENT 
                var orgeventlog = new Org_Events_Log()
                {
                    Org_Event_SubjectId = id.ToString(),
                    Org_Event_SubjectName = orgGroup.GroupName,
                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                    Org_Event_Time = DateTime.Now,
                    OrgId = Session["OrgId"].ToString(),
                    Org_Events_Types = Org_Events_Types.Deleted_Group
                };
                db.Org_Events_Logs.Add(orgeventlog);
                db.SaveChanges();
                return RedirectToAction("Index");
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
