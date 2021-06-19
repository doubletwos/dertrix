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

        // GET: OrgSchCalendars
        public ActionResult Index()
        {
            var orgSchCalendars = db.OrgSchCalendars.Include(o => o.CalendarCategory).Include(o => o.Org);
            return View(orgSchCalendars.ToList());
        }

        // GET: OrgSchCalendars/Display/
        public ActionResult OrgCalendarDisplay() 
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var orgcalendardisplay = db.OrgSchCalendars.Where(x => x.OrgId == i).ToList();

            return PartialView("~/Views/Shared/_OrgCalendarDisplay.cshtml", orgcalendardisplay);
        }


        [ChildActionOnly]
        public ActionResult AddEventToOrgCalendar()
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







        // GET: OrgSchCalendars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgSchCalendar orgSchCalendar = db.OrgSchCalendars.Find(id);
            if (orgSchCalendar == null)
            {
                return HttpNotFound();
            }
            return View(orgSchCalendar);
        }

        // GET: OrgSchCalendars/Create
        public ActionResult Create()
        {
            ViewBag.CalendarCategoryId = new SelectList(db.CalendarCategorys, "CalendarCategoryId", "CategoryName");
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            return View();
        }

        // POST: OrgSchCalendars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddNewOrgSchCalViewModel viewModel)
        {

            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
            var SessionId = Convert.ToInt32(Session["SessionId"]);

            viewModel.OrgSchCalendar.CreatorId = RegisteredUserId;
            viewModel.OrgSchCalendar.OrgId = i; 
            viewModel.OrgSchCalendar.CreatorFullName = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
            viewModel.OrgSchCalendar.CreationDate = DateTime.Now;

            if (!(ModelState.IsValid) || ModelState.IsValid)
            {
                db.OrgSchCalendars.Add(viewModel.OrgSchCalendar);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.CalendarCategoryId = new SelectList(db.CalendarCategorys, "CalendarCategoryId", "CategoryName", viewModel.OrgSchCalendar.CalendarCategoryId);
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", viewModel.OrgSchCalendar.OrgId);
            return View(viewModel);
        }

        // GET: OrgSchCalendars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgSchCalendar orgSchCalendar = db.OrgSchCalendars.Find(id);
            if (orgSchCalendar == null)
            {
                return HttpNotFound();
            }
            ViewBag.CalendarCategoryId = new SelectList(db.CalendarCategorys, "CalendarCategoryId", "CategoryName", orgSchCalendar.CalendarCategoryId);
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgSchCalendar.OrgId);
            return View(orgSchCalendar);
        }

        // POST: OrgSchCalendars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrgSchCalendarId,CalendarCategoryId,OrgId,Name,CreatorId,CreatorFullName,CreationDate,IsRecurring,Frequency,SendAsEmail")] OrgSchCalendar orgSchCalendar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orgSchCalendar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CalendarCategoryId = new SelectList(db.CalendarCategorys, "CalendarCategoryId", "CategoryName", orgSchCalendar.CalendarCategoryId);
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", orgSchCalendar.OrgId);
            return View(orgSchCalendar);
        }

        // GET: OrgSchCalendars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgSchCalendar orgSchCalendar = db.OrgSchCalendars.Find(id);
            if (orgSchCalendar == null)
            {
                return HttpNotFound();
            }
            return View(orgSchCalendar);
        }

        // POST: OrgSchCalendars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrgSchCalendar orgSchCalendar = db.OrgSchCalendars.Find(id);
            db.OrgSchCalendars.Remove(orgSchCalendar);
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
