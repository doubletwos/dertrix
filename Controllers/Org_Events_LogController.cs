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



        //  GET: Org_Events_Log/Logs
        [ChildActionOnly]
        public ActionResult Logs()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var logs = db.Org_Events_Logs.Where(x => x.OrgId == i.ToString());
                return PartialView("~/Views/Shared/_Eventlogs.cshtml", logs);
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
