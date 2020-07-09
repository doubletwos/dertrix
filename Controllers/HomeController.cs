using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Zeus.Controllers
{
    public class HomeController : Controller
    {
     


        public ActionResult SysAdminSetUp()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if ((int)Session["RegisteredUserTypeId"] != 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }


            return View();
        }
    }
}

























//public ActionResult Index()
//{
//    return View();
//}

//public ActionResult About()
//{
//    ViewBag.Message = "Your application description page.";

//    return View();
//}

//public ActionResult Contact()
//{
//    ViewBag.Message = "Your contact page.";

//    return View();
//}