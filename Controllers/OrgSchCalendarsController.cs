using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;
using Dertrix.ViewModels;

namespace Dertrix.Controllers
{
    public class OrgSchCalendarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: OrgSchCalendars/Display/
        public ActionResult OrgCalendarDisplay(bool? isarchived)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                if ((int)Session["IsAdmin"] == 1 || Session["IsTester"] != null)
                {
                    if (isarchived == null)
                    {
                        var orgcalendardisplay = (from orgschcal in db.OrgSchCalendars
                                                  where orgschcal.Isarchived == false
                                                  where orgschcal.OrgId == i
                                                  select orgschcal).ToList();

                        return PartialView("~/Views/Shared/DisplayViews/_OrgCalendarDisplay.cshtml", orgcalendardisplay);
                    }
                    if (isarchived == true)
                    {
                        var orgcalendardisplayarchived = (from orgschcal in db.OrgSchCalendars
                                                          where orgschcal.Isarchived == true
                                                          where orgschcal.OrgId == i
                                                          select orgschcal).ToList();
                        return PartialView("~/Views/Shared/DisplayViews/_OrgCalendarDisplay.cshtml", orgcalendardisplayarchived);
                    }
                    if (isarchived == false)
                    {
                        var orgcalendardisplay = (from orgschcal in db.OrgSchCalendars
                                                  where orgschcal.Isarchived == false
                                                  where orgschcal.OrgId == i
                                                  select orgschcal).ToList();
                        return PartialView("~/Views/Shared/DisplayViews/_OrgCalendarDisplay.cshtml", orgcalendardisplay);
                    }

                }
                else
                {
                    if (isarchived == null)
                    {
                        var orgcalendardisplay = (from orgschcal in db.OrgSchCalendars
                                                  join orgschgrp in db.OrgSchCalndrGrps on orgschcal.OrgSchCalendarId equals orgschgrp.OrgSchCalendarId
                                                  join rug in db.RegisteredUsersGroups on orgschgrp.OrgGroupId equals rug.OrgGroupId
                                                  join ru in db.RegisteredUsers on rug.RegisteredUserId equals ru.RegisteredUserId
                                                  where orgschcal.Isarchived == false
                                                  where ru.RegisteredUserId == RegisteredUserId
                                                  where orgschcal.OrgId == i
                                                  select orgschcal)
                                                  .Distinct()
                                                  .ToList();

                        return PartialView("~/Views/Shared/DisplayViews/_OrgCalendarDisplay.cshtml", orgcalendardisplay);
                    }
                    if (isarchived == true)
                    {
                        var orgcalendardisplayarchived = (from orgschcal in db.OrgSchCalendars
                                                          join orgschgrp in db.OrgSchCalndrGrps on orgschcal.OrgSchCalendarId equals orgschgrp.OrgSchCalendarId
                                                          join rug in db.RegisteredUsersGroups on orgschgrp.OrgGroupId equals rug.OrgGroupId
                                                          join ru in db.RegisteredUsers on rug.RegisteredUserId equals ru.RegisteredUserId
                                                          where orgschcal.Isarchived == true
                                                          where ru.RegisteredUserId == RegisteredUserId
                                                          where orgschcal.OrgId == i
                                                          select orgschcal)
                                                          .Distinct()
                                                          .ToList();

                        return PartialView("~/Views/Shared/DisplayViews/_OrgCalendarDisplay.cshtml", orgcalendardisplayarchived);
                    }
                    if (isarchived == false)
                    {
                        var orgcalendardisplay = (from orgschcal in db.OrgSchCalendars
                                                  join orgschgrp in db.OrgSchCalndrGrps on orgschcal.OrgSchCalendarId equals orgschgrp.OrgSchCalendarId
                                                  join rug in db.RegisteredUsersGroups on orgschgrp.OrgGroupId equals rug.OrgGroupId
                                                  join ru in db.RegisteredUsers on rug.RegisteredUserId equals ru.RegisteredUserId
                                                  where orgschcal.Isarchived == false
                                                  where ru.RegisteredUserId == RegisteredUserId
                                                  where orgschcal.OrgId == i
                                                  select orgschcal)
                                                  .Distinct()
                                                  .ToList();

                        return PartialView("~/Views/Shared/DisplayViews/_OrgCalendarDisplay.cshtml", orgcalendardisplay);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return Content("");
        }




        [ChildActionOnly]
        public ActionResult AddEventToOrgCalendar()
        {
            try
            {
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);

                var orgschcalendar = new OrgSchCalendar();
                // Get all the groups from the database
                var grp = db.OrgGroups.Where(c => c.OrgId == i).ToList();
                // Get all the Catergory from the database
                var calendarcategorys = db.CalendarCategorys.ToList();
                // Initialize the view model
                var addeventtoorgcalviewmodel = new AddNewOrgSchCalViewModel
                {
                    OrgSchCalendar = orgschcalendar,
                    CalendarCategorys = calendarcategorys,
                    OrgGroups = grp.Select(x => new OrgGroup()
                    {
                        OrgGroupId = x.OrgGroupId,
                        OrgId = x.OrgId,
                        GroupName = x.GroupName
                    }).ToList()
                };
                ViewBag.CalendarCategoryId = new SelectList(db.CalendarCategorys, "CalendarCategoryId", "CategoryName");
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
                return PartialView("~/Views/Shared/PartialViewsForms/_AddEventToOrgCalendar.cshtml", addeventtoorgcalviewmodel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        public ActionResult EventDisplayPanel()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                var eventsdisplay = (from orgschcal in db.OrgSchCalendars
                                          join orgschgrp in db.OrgSchCalndrGrps on orgschcal.OrgSchCalendarId equals orgschgrp.OrgSchCalendarId
                                          join rug in db.RegisteredUsersGroups on orgschgrp.OrgGroupId equals rug.OrgGroupId
                                          join ru in db.RegisteredUsers on rug.RegisteredUserId equals ru.RegisteredUserId
                                          where orgschcal.Isarchived == false
                                          where ru.RegisteredUserId == RegisteredUserId
                                          where orgschcal.OrgId == i
                                          select orgschcal)
                          .Distinct()
                          .ToList();

                return PartialView("~/Views/Shared/DisplayViews/_EventDisplayPanel.cshtml", eventsdisplay);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        // GET: OrgSchCalendars/EventDetails/5
        public ActionResult EventDetails(int Id)
        {
            try
            {
                var calendarevent = db.OrgSchCalendars.Where(x => x.OrgSchCalendarId == Id);
                ViewBag.OrgSchCalendar = calendarevent;
                return PartialView("_EventDetails");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        // Update past events by sys admin and set the archived glad
        public ActionResult UpdateEvents()
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                if ((int)Session["OrgId"] != 23)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if ((int)Session["OrgId"] == 23)
                {
                    // LOOP THROUGH LIST OF EVENTS IN DB
                    var orgschevnts = db.OrgSchCalendars.Select(x => x.OrgSchCalendarId).ToList();
                    var orgschevntslist = new List<int>(orgschevnts);

                    foreach (var evntid in orgschevnts)
                    {
                        // GET EVENT DATE
                        var eventdate = db.OrgSchCalendars.Where(x => x.OrgSchCalendarId == evntid).Select(x => x.EventDate).FirstOrDefault();
                        // GET VALUE OF ISSCHEDULE
                        var eventid = db.OrgSchCalendars.AsNoTracking().Where(x => x.OrgSchCalendarId == evntid).FirstOrDefault();
                        if (eventdate < DateTime.Now)
                        {
                            var schevnt = new OrgSchCalendar
                            {
                                OrgSchCalendarId = eventid.OrgSchCalendarId,
                                CalendarCategoryId = eventid.CalendarCategoryId,
                                OrgId = eventid.OrgId,
                                Name = eventid.Name,
                                CreatorId = eventid.CreatorId,
                                CreatorFullName = eventid.CreatorFullName,
                                CreationDate = eventid.CreationDate,
                                IsRecurring = eventid.IsRecurring,
                                Frequency = eventid.Frequency,
                                SendAsEmail = eventid.SendAsEmail,
                                EventDate = eventid.EventDate,
                                Description = eventid.Description,
                                EventTime = eventid.EventTime,
                                Isarchived = true
                            };
                            eventid = schevnt;
                            db.Entry(eventid).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                    }
                    return RedirectToAction("SysAdminSetUp", "Home");
                }
                return new HttpStatusCodeResult(204);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        // POST: OrgSchCalendars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddNewOrgSchCalViewModel viewModel)
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
                var SessionId = Convert.ToInt32(Session["SessionId"]);

                viewModel.OrgSchCalendar.CreatorId = RegisteredUserId;
                viewModel.OrgSchCalendar.OrgId = i;
                viewModel.OrgSchCalendar.CreatorFullName = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
                viewModel.OrgSchCalendar.CreationDate = DateTime.Now;
                viewModel.OrgSchCalendar.Isarchived = false;


                if (!(ModelState.IsValid) || ModelState.IsValid)
                {
                    db.OrgSchCalendars.Add(viewModel.OrgSchCalendar);
                    db.SaveChanges();

                    // UPON CREATING A CALNDR EVENT - LOG THE EVENT 
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_SubjectId = viewModel.OrgSchCalendar.OrgSchCalendarId.ToString(),
                        Org_Event_SubjectName = viewModel.OrgSchCalendar.Name,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString(),
                        Org_Events_Types = Org_Events_Types.Calendar_Event_Created
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();

                    var grps = viewModel.OrgGroups.Select(x => x.OrgGroupId).ToList();
                    var grpstolist = new List<int>(grps);

                    foreach (var grp in grps)
                    {
                        // GET VALUE OF IS-SELECTED
                        var isselected = viewModel.OrgGroups.Where(x => grp == x.OrgGroupId).Select(x => x.IsSelected).FirstOrDefault();
                        if (isselected == true)
                        {
                            var orgschcalndrGrps = new OrgSchCalndrGrp()
                            {
                                OrgSchCalendarId = viewModel.OrgSchCalendar.OrgSchCalendarId,
                                OrgGroupId = grp,
                                OrgId = i,
                            };
                            db.OrgSchCalndrGrps.Add(orgschcalndrGrps);
                            db.SaveChanges();
                        }
                    }
                    return Content("");
                }
                ViewBag.CalendarCategoryId = new SelectList(db.CalendarCategorys, "CalendarCategoryId", "CategoryName", viewModel.OrgSchCalendar.CalendarCategoryId);
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", viewModel.OrgSchCalendar.OrgId);
                return Content("");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(viewModel);
            }
        }


        public ActionResult EditOrgSchCal(int Id)
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
                {
                    return RedirectToAction("WrongDevice", "Orgs");

                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);

                var edtcalevent = db.OrgSchCalendars
                .Include(x => x.CalendarCategory)
                .Where(x => x.OrgSchCalendarId == Id)
                .FirstOrDefault();

                var orgschcalendar = new OrgSchCalendar();
                // Get all the groups from the database
                var grp = db.OrgGroups.Where(c => c.OrgId == i).ToList();


                // Get all the Catergory from the database
                var calendarcategorys = db.CalendarCategorys.ToList();
                // Initialize the view model
                var editeventorgcalviewmodel = new EditOrgSchCalViewModel
                {

                    OrgSchCalendarId = edtcalevent.OrgSchCalendarId,
                    CalendarCategoryId = edtcalevent.CalendarCategoryId,
                    OrgId = edtcalevent.OrgId,
                    Name = edtcalevent.Name,
                    CreatorId = edtcalevent.CreatorId,
                    CreatorFullName = edtcalevent.CreatorFullName,
                    Description = edtcalevent.Description,
                    EventDate = edtcalevent.EventDate,
                    EventTime = edtcalevent.EventTime,
                    CreationDate = edtcalevent.CreationDate,
                    IsRecurring = edtcalevent.IsRecurring,
                    Frequency = edtcalevent.Frequency,
                    SendAsEmail = edtcalevent.SendAsEmail,
                    Isarchived = edtcalevent.Isarchived,

                    OrgGroups = grp.Select(x => new OrgGroup()
                    {
                        OrgGroupId = x.OrgGroupId,
                        OrgId = x.OrgId,
                        GroupName = x.GroupName,
                        IsSelected = x.IsSelected
                    }).ToList()
                };

                foreach (var group in editeventorgcalviewmodel.OrgGroups)
                {
                    int groupCount = editeventorgcalviewmodel.OrgGroups.Count();
                }
                foreach (var item in editeventorgcalviewmodel.OrgGroups)
                {
                    var isselected = db.OrgSchCalndrGrps
                        .Where(x => x.OrgGroupId == item.OrgGroupId)
                        .Where(x => x.OrgSchCalendarId == Id)
                        .Where(X => X.OrgId == i)
                        .Select(x => x.OrgSchCalndrGrpId)
                        .Count();

                    if (isselected == 0)
                    {
                        item.IsSelected = false;
                        editeventorgcalviewmodel.CalendarCategoryId = edtcalevent.CalendarCategoryId;
                    }
                    else
                    {
                        item.IsSelected = true;
                        editeventorgcalviewmodel.CalendarCategoryId = edtcalevent.CalendarCategoryId;
                    }
                }
                ViewBag.CalendarCategoryId = new SelectList(db.CalendarCategorys, "CalendarCategoryId", "CategoryName");
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
                return PartialView("~/Views/Shared/PartialViewsForms/_EditOrgSchCal.cshtml", editeventorgcalviewmodel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        //// POST: OrgSchCalendars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(OrgSchCalendar orgSchCalendar)
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
                {
                    return RedirectToAction("WrongDevice", "Orgs");

                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);

                // LOOP THRU LIST OF RECORD IN TABLE AND REMOVE
                var orgschcalid = db.OrgSchCalndrGrps
                    .Where(x => x.OrgSchCalendarId == orgSchCalendar.OrgSchCalendarId)
                    .Where(x => x.OrgId == i)
                    .Select(x => x.OrgSchCalndrGrpId)
                    .ToList();
                var orgschcalidtolist = new List<int>(orgschcalid);

                foreach (var recrd in orgschcalid)
                {
                    var removercrd = db.OrgSchCalndrGrps
                        .Where(x => x.OrgSchCalndrGrpId == recrd)
                        .Where(x => x.OrgId == i)
                        .Select(x => x.OrgSchCalndrGrpId)
                        .FirstOrDefault();

                    OrgSchCalndrGrp orgschcalndrgrp = db.OrgSchCalndrGrps.Find(removercrd);
                    db.OrgSchCalndrGrps.Remove(orgschcalndrgrp);
                }

                // LOOP THRU LIST OF GROUPS PROVIDED
                var grps = orgSchCalendar.OrgGroups.Select(x => x.OrgGroupId).ToList();
                var grpstolist = new List<int>(grps);

                foreach (var grp in grps)
                {
                    // GET VALUE OF IS-SELECTED
                    var isselected = orgSchCalendar.OrgGroups
                        .Where(x => grp == x.OrgGroupId)
                        .Select(x => x.IsSelected)
                        .FirstOrDefault();

                    if (isselected == true)
                    {
                        var orgschcalndrGrps = new OrgSchCalndrGrp()
                        {
                            OrgSchCalendarId = orgSchCalendar.OrgSchCalendarId,
                            OrgGroupId = grp,
                            OrgId = i,
                        };
                        db.OrgSchCalndrGrps.Add(orgschcalndrGrps);
                        db.SaveChanges();
                    }
                }
                if (!(ModelState.IsValid) || ModelState.IsValid)
                {
                    db.Entry(orgSchCalendar).State = EntityState.Modified;
                    db.SaveChanges();

                    //// UPON EDITING A CALNDR EVENT - LOG THE EVENT - 
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_SubjectId = orgSchCalendar.OrgSchCalendarId.ToString(),
                        Org_Event_SubjectName = orgSchCalendar.Name,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString(),
                        Org_Events_Types = Org_Events_Types.Calendar_Event_Edited

                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Orgs", new { id = i });
                }
                ViewBag.CalendarCategoryId = new SelectList(db.CalendarCategorys, "CalendarCategoryId", "CategoryName", orgSchCalendar.CalendarCategoryId);
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgSchCalendar.OrgId);
                return View(orgSchCalendar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        public ActionResult DeleteConfirmed(int? Id)
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);

                OrgSchCalendar orgSchCalendar = db.OrgSchCalendars.Find(Id);
                db.OrgSchCalendars.Remove(orgSchCalendar);
                db.SaveChanges();

                //// UPON DELETING A CALNDR EVENT - LOG THE EVENT 
                var orgeventlog = new Org_Events_Log()
                {
                    Org_Event_SubjectId = orgSchCalendar.OrgSchCalendarId.ToString(),
                    Org_Event_SubjectName = orgSchCalendar.Name,
                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                    Org_Event_Time = DateTime.Now,
                    OrgId = Session["OrgId"].ToString(),
                    Org_Events_Types = Org_Events_Types.Calendar_Event_Deleted
                };
                db.Org_Events_Logs.Add(orgeventlog);
                db.SaveChanges();
                return RedirectToAction("Index", "Orgs", new { id = i });
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
