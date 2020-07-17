using Microsoft.CSharp.RuntimeBinder;
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
    public class OrgsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       // GET: Orgs
        public ActionResult Index(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }


            var orgs = db.Orgs.Include(o => o.Domain).Include(o => o.OrgBrand).Include(o => o.OrgType);
            return View(orgs.ToList());
        }


        // GET: Orgs/SystemAdminIndex
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
                var orgs = db.Orgs
                    .Include(o => o.Domain)
                    .Include(o => o.OrgBrand);
                return View(orgs.ToList());

            }
            else

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }



        [ChildActionOnly]
        public ActionResult AddOrg()
        {
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.DomainId = new SelectList(db.Domains, "DomainId", "DomainName");
            ViewBag.OrgBrandId = new SelectList(db.OrgBrands, "OrgBrandId", "OrgBrandName");
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName");
            return PartialView("_AddOrg");
        }




        // GET: Orgs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            return View(org);
        }

        // GET: Orgs/Create
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



            ViewBag.DomainId = new SelectList(db.Domains, "DomainId", "DomainName");
            ViewBag.OrgBrandId = new SelectList(db.OrgBrands, "OrgBrandId", "OrgBrandName");
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName");
            return View();
        }

        // POST: Orgs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Org org)
        {
            var rud = Session["Email"].ToString();
            var loggedinuser = db.RegisteredUsers.Where(x => x.Email ==  rud).Select(x => x.Email).SingleOrDefault();
            var orgredirect = db.RegisteredUserOrganisations.Where(x => x.Email == rud).Select(x => x.OrgId).FirstOrDefault();


            if ( rud != loggedinuser)
            
            {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            if (ModelState.IsValid)
            {
                org.CreatedBy = Session["RegisteredUserId"].ToString();
                org.CreationDate = DateTime.Now;
                db.Orgs.Add(org);
                db.SaveChanges();


                var orgOrgType = new OrgOrgType()
                {
                    OrgId = org.OrgId,
                    OrgName = org.OrgName,
                    OrgTypeId = (int)org.OrgTypeId

                };

                db.OrgOrgTypes.Add(orgOrgType);
                db.SaveChanges();

                return RedirectToAction("Index", "Orgs", new { id = orgredirect });
            }

            ViewBag.DomainId = new SelectList(db.Domains, "DomainId", "DomainName", org.DomainId);
            ViewBag.OrgBrandId = new SelectList(db.OrgBrands, "OrgBrandId", "OrgBrandName", org.OrgBrandId);
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName", org.OrgTypeId);
            return View(org);
        }

        // GET: Orgs/Edit/5
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            ViewBag.DomainId = new SelectList(db.Domains, "DomainId", "DomainName", org.DomainId);
            ViewBag.OrgBrandId = new SelectList(db.OrgBrands, "OrgBrandId", "OrgBrandName", org.OrgBrandId);
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName", org.OrgTypeId);
            return View(org);
        }

        // POST: Orgs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Org org)
        {
            if (ModelState.IsValid)
            {
                db.Entry(org).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SystemAdminIndex");
            }
            ViewBag.DomainId = new SelectList(db.Domains, "DomainId", "DomainName", org.DomainId);
            ViewBag.OrgBrandId = new SelectList(db.OrgBrands, "OrgBrandId", "OrgBrandName", org.OrgBrandId);
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName", org.OrgTypeId);

            return View(org);
        }

        // GET: Orgs/Delete/5
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
            Org org = db.Orgs.Find(id);
            if (org == null)
            {
                return HttpNotFound();
            }
            return View(org);
        }

        // POST: Orgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Org org = db.Orgs.Find(id);
            db.Orgs.Remove(org);
            db.SaveChanges();
            return RedirectToAction("SystemAdminIndex");
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
