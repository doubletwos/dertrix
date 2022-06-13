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
    public class RegisteredUserOrganisationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

    
        [ChildActionOnly]
        public ActionResult MyOrgCount()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                var myorgCount = db.RegisteredUserOrganisations
                    .Where(j => j.RegisteredUserId == RegisteredUserId)
                    .ToList();
                return PartialView("_MyOrgCount", myorgCount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }

        public ActionResult MyOrgList()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                var myorgCount = db.RegisteredUserOrganisations
                    .Where(j => j.RegisteredUserId == RegisteredUserId)
                    .ToList();
                return PartialView("_MyOrgList", myorgCount);
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
