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
    public class Org_Events_LogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Org_Events_Log
        public ActionResult Index()
        {
            return View(db.Org_Events_Logs.ToList());
        }





        //  GET: Org_Events_Log/Logs
        [ChildActionOnly]
        public ActionResult Logs()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);


            var logs = db.Org_Events_Logs.Where(x => x.OrgId == i.ToString());


            return PartialView("~/Views/Shared/_Eventlogs.cshtml", logs);
        }






        // POST: Org_Events_Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Org_Events_Log org_Events_Log = db.Org_Events_Logs.Find(id);
            db.Org_Events_Logs.Remove(org_Events_Log);
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
