using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
            var isTester = Convert.ToInt32(Session["IsTester"]);

            if (isTester == 1)
            {
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
                Session.Clear();
                Session["IsTester"] = isTester;
                Session["RegisteredUserId"] = RegisteredUserId;
                Session["OrgName"] = db.RegisteredUserOrganisations.Where(x => x.OrgId == id).Select(x => x.OrgName).FirstOrDefault();
                Session["OrgId"] = id;
                Session["FullName"] = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
                Session["RegisteredUserTypeId"] = db.RegisteredUserOrganisations.Where(x => x.OrgId == id).Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.RegisteredUserTypeId).FirstOrDefault();
                Session["IsTester"] = db.RegisteredUserOrganisations.Where(x => x.OrgId == id).Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.IsTester).FirstOrDefault();
                Session["regUserOrgBrand"] = db.RegisteredUserOrganisations.Where(x => x.OrgId == id).Select(x => x.RegUserOrgBrand).FirstOrDefault();
                var orgbrand = db.Orgs.Where(x => x.OrgId == id).Select(x => x.OrgBrandId).FirstOrDefault();
                Session["regUserOrgBrandBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandBar).FirstOrDefault();
                Session["regUserOrgNavBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavigationBar).FirstOrDefault();
                Session["regUserOrgNavTextColor"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavBarTextColour).FirstOrDefault();
                Session["regOrgBrandButtonColour"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandButtonColour).FirstOrDefault();
                Session["regOrgLogo"] = db.Files.Where(x => x.OrgBrandId == orgbrand).Select(x => x.Content).FirstOrDefault();
                var orgs1 = db.Orgs.Include(o => o.Domain).Include(o => o.OrgBrand).Include(o => o.OrgType);
                return View(orgs1.ToList());
            }
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







        public ActionResult JumpToOrg(int id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        

           var orgName = Session["OrgName"] = db.Orgs.Where(x => x.OrgId == id).Select(x => x.OrgName.FirstOrDefault());
           var brandId = Session["brandId"] = db.Orgs.Where(x => x.OrgId == id).Select(x => x.OrgBrandId).FirstOrDefault().ToString();
            var logo = Session["regOrgLogo"] = db.Files.Where(x => x.OrgBrandId.ToString() == brandId.ToString()).Select(x => x.Content).FirstOrDefault();
            var orgBrand = Session["regUserOrgBrand"] = db.OrgBrands.Where(x => x.OrgBrandId.ToString() == brandId.ToString()).Select(x => x.OrgBrandBar).FirstOrDefault();
            var regUserOrgNavBar = Session["regUserOrgNavBar"] = db.OrgBrands.Where(x => x.OrgBrandId.ToString() == brandId.ToString()).Select(x => x.OrgNavigationBar).FirstOrDefault();
            var IsTesterOrgId = Session["IsTesterOrgId"] = id;
            var IsTester = Session["IsTester"] = true; 


            return RedirectToAction("Index", "Orgs", new { id  });


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




        public ActionResult OrgDetails(int Id)
        {
            var stud = db.Orgs.Where(x => x.OrgId == Id);

            ViewBag.Org = stud;

            return PartialView("_OrgDetails");
        }


        public ActionResult EditOrg(int Id)
        {
            if (Id != 0)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var edtorg = db.Orgs
                    .Include(d => d.Domain)
                    .Include(o => o.OrgBrand)
                    .Include(k => k.OrgType)
                    .Where(x => x.OrgId == Id)
                    .FirstOrDefault();
                ViewBag.DomainId = new SelectList(db.Domains, "DomainId", "DomainName", edtorg.DomainId);
                ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName", edtorg.OrgTypeId);
                ViewBag.OrgBrandId = new SelectList(db.OrgBrands, "OrgBrandId", "OrgBrandName", edtorg.OrgBrandId);


                var edtorg1 = new Org
                {
                    OrgId = edtorg.OrgId,
                    OrgName = edtorg.OrgName,
                    OrgAddress = edtorg.OrgAddress,
                    CreationDate = edtorg.CreationDate,
                    DomainId = edtorg.DomainId,
                    OrgTypeId = edtorg.OrgTypeId,
                    OrgBrandId = edtorg.OrgBrandId,
                    

                };
                return PartialView("_EditOrg", edtorg1);
            }
            return PartialView("_EditOrg");
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


        public ActionResult SystemAdminIndex(string searchname, string searchid)
        {
            var isTester = Convert.ToInt32(Session["IsTester"]);
            var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);


            if (isTester == 1 && ((int)Session["OrgId"] != 23))
            {
                Session.Clear();
                Session["OrgId"] = 23;
                Session["RegisteredUserId"] = RegisteredUserId;
                Session["OrgName"] = db.RegisteredUserOrganisations.Where(x => x.OrgId == 23).Select(x => x.OrgName).FirstOrDefault();

                Session["FullName"] = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
                Session["RegisteredUserTypeId"] = db.RegisteredUserOrganisations.Where(x => x.OrgId == 23).Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.RegisteredUserTypeId).FirstOrDefault();
                Session["IsTester"] = db.RegisteredUserOrganisations.Where(x => x.OrgId == 23).Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.IsTester).FirstOrDefault();
                Session["regUserOrgBrand"] = db.RegisteredUserOrganisations.Where(x => x.OrgId == 23).Select(x => x.RegUserOrgBrand).FirstOrDefault();
                var orgbrand = db.Orgs.Where(x => x.OrgId == 23).Select(x => x.OrgBrandId).FirstOrDefault();
                Session["regUserOrgBrandBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandBar).FirstOrDefault();
                Session["regUserOrgNavBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavigationBar).FirstOrDefault();
                Session["regUserOrgNavTextColor"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavBarTextColour).FirstOrDefault();
                Session["regOrgBrandButtonColour"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandButtonColour).FirstOrDefault();
                Session["regOrgLogo"] = db.Files.Where(x => x.OrgBrandId == orgbrand).Select(x => x.Content).FirstOrDefault();

                var org1 = db.Orgs.Where(s => s.OrgAddress == searchname).Include(t => t.OrgType).ToList();
                return View(org1);






            }



            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var org = db.Orgs.Where(x => x.OrgTypeId != 5).Include(t => t.OrgType).ToList();

            // returns null at page load
            if (string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid))
            {
                return View(org);
            }

            // returns org if id is selected & searchname is null
            else if (string.IsNullOrWhiteSpace(searchname) && !(string.IsNullOrWhiteSpace(searchid)))
            {
                return View(db.Orgs.Where(i => i.OrgId.ToString() == searchid).Include(t => t.OrgType).ToList());
            }

            // returns org if searchname is selected & id is null
            else if (!(string.IsNullOrWhiteSpace(searchname) && (string.IsNullOrWhiteSpace(searchid))))
            {
                return View(db.Orgs.Where(n => n.OrgName == searchname).Include(t => t.OrgType).ToList());
            }

            return View(org);
        }


        public JsonResult AutoCompleteSchool(string prefix)
        {
            var schoollist = (from org in db.Orgs
                              where org.OrgName.StartsWith(prefix)
                              select new
                              {
                                  label = org.OrgName,
                                  Val = org.OrgId
                              }).ToList();

            return Json(schoollist);
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
