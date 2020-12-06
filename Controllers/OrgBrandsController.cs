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
    public class OrgBrandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrgBrands
        public ActionResult Index()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.OrgBrands.ToList());
        }

        public ActionResult OrgBrandDetails(int Id)
        {
            var stud = db.OrgBrands.Where(x => x.OrgBrandId == Id);
            ViewBag.OrgBrand = stud;
            return PartialView("_OrgBrandDetails");
        }

        public ActionResult EditOrgBrand(int Id)
        {
            if (Id != 0)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var edtorgbrand = db.OrgBrands
                    .Include(f => f.Files)
                    .Where(x => x.OrgBrandId == Id)
                    .FirstOrDefault();
                var edtorgBrand1 = new OrgBrand
                {
                    OrgBrandId = edtorgbrand.OrgBrandId,
                    OrgBrandName = edtorgbrand.OrgBrandName,
                    OrgBrandBar = edtorgbrand.OrgBrandBar,
                    OrgNavigationBar = edtorgbrand.OrgNavigationBar,
                    OrgNavBarTextColour = edtorgbrand.OrgNavBarTextColour,
                    OrgBrandButtonColour = edtorgbrand.OrgBrandButtonColour,
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditOrgBrand.cshtml", edtorgBrand1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditOrgBrand.cshtml");
        }


        [ChildActionOnly]
        public ActionResult AddOrgBrand()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddOrgBrand.cshtml");
        }

        // POST: OrgBrands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrgBrand orgBrand, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Logo,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    orgBrand.Files = new List<File> { avatar };
                }
                db.OrgBrands.Add(orgBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgBrand);
        }

        // POST: OrgBrands/Edit/5
        [HttpPost]
        public ActionResult Edit(OrgBrand orgBrand, HttpPostedFileBase Logo)
        {
            if (ModelState.IsValid)
            {
                var orgBrandInDb = db.OrgBrands.Include(f => f.Files).Single(c => c.OrgBrandId == orgBrand.OrgBrandId);
                orgBrandInDb.OrgBrandName = orgBrand.OrgBrandName;
                orgBrandInDb.OrgBrandBar = orgBrand.OrgBrandBar;
                orgBrandInDb.OrgNavigationBar = orgBrand.OrgNavigationBar;
                orgBrandInDb.OrgNavBarTextColour = orgBrand.OrgNavBarTextColour;
                orgBrandInDb.OrgBrandButtonColour = orgBrand.OrgBrandButtonColour;
                if (Logo != null && Logo.ContentLength > 0)
                {
                    if (orgBrandInDb.Files.Any(f => f.FileType == FileType.Logo))
                    {
                        db.Files.Remove(orgBrandInDb.Files.First(f => f.FileType == FileType.Logo));
                    }
                    var logo = new File
                    {
                        FileName = System.IO.Path.GetFileName(Logo.FileName),
                        FileType = FileType.Logo,
                        ContentType = Logo.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(Logo.InputStream))
                    {
                        logo.Content = reader.ReadBytes(Logo.ContentLength);
                    }
                    orgBrandInDb.Files = new List<File> { logo };
                }
                db.Entry(orgBrandInDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orgBrand);
        }

        // POST: OrgBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrgBrand orgBrand = db.OrgBrands.Find(id);
            db.OrgBrands.Remove(orgBrand);
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