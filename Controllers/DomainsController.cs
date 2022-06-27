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
    public class DomainsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [ChildActionOnly]
        public ActionResult AddDomain()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddDomain.cshtml");
        }



        // POST: Domains/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Domain domain)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                if (ModelState.IsValid)
                {
                    db.Domains.Add(domain);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(domain);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(domain);
            }
        }




        // POST: Domains/Delete/5
        public ActionResult DeleteConfirmed(int id)
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
                    Domain domain = db.Domains.Find(id);
                    db.Domains.Remove(domain);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }


        public ActionResult EditDomain(int Id)
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
                    if (Id != 0)
                    {
                        var edtdomain = db.Domains
                            .Where(x => x.DomainId == Id)
                            .FirstOrDefault();
                        var edtdomain1 = new Domain
                        {
                            DomainId = edtdomain.DomainId,
                            DomainName = edtdomain.DomainName
                        };
                        return PartialView("~/Views/Shared/PartialViewsForms/_EditDomain.cshtml", edtdomain1);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }



        // POST: Domains/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Domain domain)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(domain).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }


        // GET: Domains
        [Route("AllDomains")]
        public ActionResult Index()
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
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
                    return View(db.Domains.ToList());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
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