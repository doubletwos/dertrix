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
    public class RelationshipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Relationships
        [Route("AllRelationships")]
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
                return View(db.Relationships.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        [ChildActionOnly]
        public ActionResult AddRelationship()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddRelationship.cshtml");
        }

        public ActionResult EditRelationship(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var edtrls = db.Relationships.Where(x => x.RelationshipId == Id).FirstOrDefault();
                    var edtrls1 = new Relationship
                    {
                        RelationshipId = edtrls.RelationshipId,
                        RelationshipName = edtrls.RelationshipName
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditRelationship.cshtml", edtrls1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        // POST: Relationships/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Relationship relationship)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                if (ModelState.IsValid)
                {
                    db.Relationships.Add(relationship);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(relationship);
            }
            return View(relationship);
        }


        // POST: Relationships/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Relationship relationship)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(relationship).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(relationship);
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

        //// POST: Relationships/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        Relationship relationship = db.Relationships.Find(id);
        //        db.Relationships.Remove(relationship);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return Redirect("~/ErrorHandler.html");
        //    }

        //}
    }
}
