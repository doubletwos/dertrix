using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zeus.Models;

namespace Zeus.Controllers
{
    public class ClassController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();





        // GET: Class/SystemAdminIndex
        public ActionResult SystemAdminIndex(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if ((int)Session["OrgId"] == 23)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                id = i;
                var classes = db.Classes.Include(j => j.Org);
                return View(classes.ToList());
            }
            else

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }






        // GET: Class/Create
        public ActionResult Create()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            return View();
        }



        // POST: Class/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( @Class @Class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@Class);
                db.SaveChanges();
                return RedirectToAction("SystemAdminIndex");
            }

            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", @Class.OrgId);
            return View(@Class);
        }

        // GET: Class/Edit/5
        public ActionResult Edit(int? id)
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



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            @Class @Class = db.Classes.Find(id);
            if (@Class == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", @Class.OrgId);
            return View(@Class);
        }

        // POST: Class/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(@Class @Class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@Class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", @Class.OrgId);
            return View(@Class);
        }

        // GET: Class/Delete/5
        public ActionResult Delete(int? id)
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


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            @Class @Class = db.Classes.Find(id);
            if (@Class == null)
            {
                return HttpNotFound();
            }
            return View(@Class);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            @Class @Class = db.Classes.Find(id);
            db.Classes.Remove(@Class);
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




















// GET: Class
//public ActionResult Index(int? id)
//{
//    if (Session["OrgId"] == null)
//    {
//        return RedirectToAction("Index", "Access");
//    }

//    if (Session["OrgId"] != null)
//    {
//        var rr = Session["OrgId"].ToString();
//        int i = Convert.ToInt32(rr);
//        id = i;
//    }

//    return View(db.Classes
//        .Where(k => k.OrgId == id)
//        .Include(l => l.Org)
//        .Include(f => f.RegisteredUsers)

//        .ToList());
//}



// GET: Class/Details/5
//public ActionResult Details(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    @Class @Class = db.Classes.Find(id);
//    if (@Class == null)
//    {
//        return HttpNotFound();
//    }
//    return View(@Class);
//}