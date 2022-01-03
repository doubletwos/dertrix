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

        [ChildActionOnly]
        public ActionResult AddOrgBrand()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddOrgBrand.cshtml");
        }

        // GET: OrgBrands
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
                    return RedirectToAction("Signin", "Access");
                }
                if ((int)Session["OrgId"] != 23)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if ((int)Session["OrgId"] == 23)
                {
                    return View(db.OrgBrands.ToList());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        public ActionResult OrgBrandDetails(int Id)
        {
            try
            {
                var stud = db.OrgBrands.Where(x => x.OrgBrandId == Id);
                ViewBag.OrgBrand = stud;
                return PartialView("_OrgBrandDetails");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        public ActionResult EditOrgBrand(int Id)
        {
            try
            {
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
                            OrgBrandButtonColour = edtorgbrand.OrgBrandButtonColour,
                        };
                        return PartialView("~/Views/Shared/PartialViewsForms/_EditOrgBrand.cshtml", edtorgBrand1);
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

        // POST: OrgBrands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrgBrand orgBrand, HttpPostedFileBase upload)
        {
            try
            {
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }


        // POST: OrgBrands/Edit/5
        [HttpPost]
        public ActionResult Edit(OrgBrand orgBrand, HttpPostedFileBase Logo)
        {
            try
            {
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
                    if (ModelState.IsValid)
                    {
                        var orgBrandInDb = db.OrgBrands.Include(f => f.Files).Single(c => c.OrgBrandId == orgBrand.OrgBrandId);
                        orgBrandInDb.OrgBrandName = orgBrand.OrgBrandName;
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

        // POST: OrgBrands/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    OrgBrand orgBrand = db.OrgBrands.Find(id);
        //    db.OrgBrands.Remove(orgBrand);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}