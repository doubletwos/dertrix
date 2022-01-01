using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace Dertrix.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult SysAdminSetUp()
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                if ((int)Session["OrgId"] != 23)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if ((int)Session["OrgId"] == 23)
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        public ActionResult SysSetUp()
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }
    }
}