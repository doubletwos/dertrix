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
    public class OrgEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgEvents
        public ActionResult Index()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            return View(db.OrgEvents.ToList());
        }

        [ChildActionOnly]
        public ActionResult AddOrgEvent()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddOrgEvent.cshtml");
        }


        public ActionResult EditEvent(int Id)
        {
            if (Id != 0)
            {
                var edtevent = db.OrgEvents
                    .Where(x => x.OrgEventId == Id)
                    .FirstOrDefault();
                var edtevent1 = new OrgEvent
                {
                 OrgEventId = edtevent.OrgEventId,
                  EventName = edtevent.EventName,
                  OrgId = edtevent.OrgId,
                  CreatedBy = edtevent.CreatedBy,
                  CreatorName = edtevent.CreatorName,
                  EventDescription = edtevent.EventDescription,
                  EventDate = edtevent.EventDate,
                  SendAsEmail = edtevent.SendAsEmail                
                };

                return PartialView("~/Views/Shared/PartialViewsForms/_EditEvent.cshtml", edtevent1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditEvent.cshtml");
        }


        //  GET: OrgEvents/EventDisplayPanel
        [ChildActionOnly]
        public ActionResult EventDisplayPanel()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var orgevent = db.OrgEvents.Where(x => x.OrgId == i);
            return PartialView("~/Views/Shared/PartialViewsForms/_EventDisplayPanel.cshtml", orgevent);
        }

        // GET: OrgEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgEvent orgEvent = db.OrgEvents.Find(id);
            if (orgEvent == null)
            {
                return HttpNotFound();
            }
            return View(orgEvent);
        }

        // GET: OrgEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrgEvents/Create
        [HttpPost]
        public ActionResult Create(OrgEvent orgEvent)
        {
           
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                orgEvent.OrgId = i;
                orgEvent.CreatedBy = RegisteredUserId.ToString();
                orgEvent.CreatorName = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();

                db.OrgEvents.Add(orgEvent);
                db.SaveChanges();
                return RedirectToAction("Index");

        }

        // GET: OrgEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgEvent orgEvent = db.OrgEvents.Find(id);
            if (orgEvent == null)
            {
                return HttpNotFound();
            }
            return View(orgEvent);
        }

        // POST: OrgEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrgEventId,EventName,OrgId,CreatedBy,CreatorName,EventDescription,EventDate,SendAsEmail")] OrgEvent orgEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orgEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgEvent);
        }

        // GET: OrgEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrgEvent orgEvent = db.OrgEvents.Find(id);
            if (orgEvent == null)
            {
                return HttpNotFound();
            }
            return View(orgEvent);
        }

        // POST: OrgEvents/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrgEvent orgEvent = db.OrgEvents.Find(id);
            db.OrgEvents.Remove(orgEvent);
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
