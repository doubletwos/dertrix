﻿using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;
namespace Dertrix.Controllers
{
    public class OrgsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orgs
        public ActionResult Index(int? id)
        {
            //if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null) 
            //{
            //    return RedirectToAction("WrongDevice","Orgs");

            //}
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Org org = db.Orgs.Find(id);
            //if (org == null)
            //{
            //    return HttpNotFound();
            //}
            var orgs = db.Orgs.Include(o => o.Domain).Include(o => o.OrgBrand).Include(o => o.OrgType);
            var isTester = Convert.ToInt32(Session["IsTester"]);
            if (isTester == 1)
            {
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
                Session.Clear();
                Session["IsTester"] = isTester;
                Session["RegisteredUserId"] = RegisteredUserId;
                Session["OrgId"] = id;
                Session["OrgType"] = db.Orgs.Where(x => x.OrgId == id).Select(x => x.OrgTypeId).FirstOrDefault();
                Session["OrgName"] = db.Orgs.Where(x => x.OrgId == id).Select(x => x.OrgName).FirstOrDefault();
                Session["FullName"] = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
                Session["RegisteredUserTypeId"] = db.RegisteredUsers.Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.RegisteredUserTypeId).FirstOrDefault();
                Session["IsTester"] = db.RegisteredUsers.Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.IsTester).FirstOrDefault();
                Session["regUserOrgBrand"] = db.Orgs.Where(x => x.OrgId == id).Select(x => x.OrgBrandId).FirstOrDefault();
                var orgbrand = db.Orgs.Where(x => x.OrgId == id).Select(x => x.OrgBrandId).FirstOrDefault();
                Session["regUserOrgBrandBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandBar).FirstOrDefault();
                Session["regUserOrgNavBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavigationBar).FirstOrDefault();
                Session["regUserOrgNavTextColor"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavBarTextColour).FirstOrDefault();
                Session["regOrgBrandButtonColour"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandButtonColour).FirstOrDefault();
                Session["regOrgLogo"] = db.Files.Where(x => x.OrgBrandId == orgbrand).Select(x => x.Content).FirstOrDefault();
                Session["IsAdmin"] = 15;
                var orgs1 = db.Orgs.Include(o => o.Domain).Include(o => o.OrgBrand).Include(o => o.OrgType);
                return View(orgs1.ToList());
            }
      
            return View(orgs.ToList());
        }


        public ActionResult StaffSchCentre1(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(Id);
            if (org == null)
            {
                return HttpNotFound();
            }
            //if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
            //{
            //    return RedirectToAction("WrongDevice", "Orgs");

            //}
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            var isTester = Convert.ToInt32(Session["IsTester"]);
            if (isTester != 1)
            {
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
                Session.Clear();
                Session["RegisteredUserId"] = RegisteredUserId;
                Session["OrgId"] = Id;
                Session["OrgType"] = db.Orgs.Where(x => x.OrgId == Id).Select(x => x.OrgTypeId).FirstOrDefault();
                Session["OrgName"] = db.Orgs.Where(x => x.OrgId == Id).Select(x => x.OrgName).FirstOrDefault();
                Session["FullName"] = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
                Session["RegisteredUserTypeId"] = db.RegisteredUsers.Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.RegisteredUserTypeId).FirstOrDefault();
                Session["IsTester"] = db.RegisteredUsers.Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.IsTester).FirstOrDefault();
                Session["regUserOrgBrand"] = db.Orgs.Where(x => x.OrgId == Id).Select(x => x.OrgBrandId).FirstOrDefault();
                var orgbrand = db.Orgs.Where(x => x.OrgId == Id).Select(x => x.OrgBrandId).FirstOrDefault();
                Session["regUserOrgBrandBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandBar).FirstOrDefault();
                Session["regUserOrgNavBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavigationBar).FirstOrDefault();
                Session["regUserOrgNavTextColor"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavBarTextColour).FirstOrDefault();
                Session["regOrgBrandButtonColour"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandButtonColour).FirstOrDefault();
                Session["regOrgLogo"] = db.Files.Where(x => x.OrgBrandId == orgbrand).Select(x => x.Content).FirstOrDefault();
                Session["IsAdmin"] = db.RegisteredUsersGroups.Where(x => x.RegisteredUserId == RegisteredUserId).Where(x => x.RegUserOrgId == Id).Select(x => x.GroupTypeId).FirstOrDefault();
                var orgs2 = db.Orgs.Include(o => o.Domain).Include(o => o.OrgBrand).Include(o => o.OrgType);
                return View(orgs2.ToList());

            }
            return View();
        }


        // GET: Orgs/Home/5
        public ActionResult PGSchCentre1(int? Id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Welcome", "Access");
            }
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Org org = db.Orgs.Find(Id);
            if (org == null)
            {
                return HttpNotFound();
            }
            var isTester = Convert.ToInt32(Session["IsTester"]);
            if (isTester != 1)
            {
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
                Session.Clear();
                Session["RegisteredUserId"] = RegisteredUserId;
                Session["OrgId"] = Id;
                Session["OrgType"] = db.Orgs.Where(x => x.OrgId == Id).Select(x => x.OrgTypeId).FirstOrDefault();
                Session["OrgName"] = db.Orgs.Where(x => x.OrgId == Id).Select(x => x.OrgName).FirstOrDefault();
                Session["FullName"] = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
                Session["RegisteredUserTypeId"] = db.RegisteredUsers.Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.RegisteredUserTypeId).FirstOrDefault();
                Session["IsTester"] = db.RegisteredUsers.Where(j => j.RegisteredUserId == RegisteredUserId).Select(x => x.IsTester).FirstOrDefault();
                Session["regUserOrgBrand"] = db.Orgs.Where(x => x.OrgId == Id).Select(x => x.OrgBrandId).FirstOrDefault();
                var orgbrand = db.Orgs.Where(x => x.OrgId == Id).Select(x => x.OrgBrandId).FirstOrDefault();
                Session["regUserOrgBrandBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandBar).FirstOrDefault();
                Session["regUserOrgNavBar"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavigationBar).FirstOrDefault();
                Session["regUserOrgNavTextColor"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgNavBarTextColour).FirstOrDefault();
                Session["regOrgBrandButtonColour"] = db.OrgBrands.Where(x => x.OrgBrandId == orgbrand).Select(x => x.OrgBrandButtonColour).FirstOrDefault();
                Session["regOrgLogo"] = db.Files.Where(x => x.OrgBrandId == orgbrand).Select(x => x.Content).FirstOrDefault();
                Session["IsParent/Guardian"] = db.StudentGuardians.Where(x => x.RegisteredUserId == RegisteredUserId && x.OrgId == Id).Select(x => x.GuardianEmailAddress).FirstOrDefault();
                Session["IsAdmin"] = db.RegisteredUsersGroups.Where(x => x.RegisteredUserId == RegisteredUserId).Where(x => x.RegUserOrgId == Id).Select(x => x.GroupTypeId).FirstOrDefault();
        
                var orgs = db.Orgs.Include(o => o.Domain).Include(o => o.OrgBrand).Include(o => o.OrgType);


            }

            return View();

        }


        // GET: Orgs/Home/5
        public ActionResult PGSchCentre(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Welcome", "Access");
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

            return View();
        }


        // GET: Orgs/Home/5
        public ActionResult SchCalendar()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Welcome", "Access");
            }
            if (Session["IsParent/Guardian"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }




        // GET: Orgs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Welcome", "Access");
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
            return PartialView("~/Views/Shared/PartialViewsForms/_OrgDetails.cshtml");
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
                    CreatedBy = edtorg.CreatedBy
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditOrg.cshtml", edtorg1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditOrg.cshtml");
        }

        [ChildActionOnly]
        public ActionResult AddOrg()
        {
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.DomainId = new SelectList(db.Domains, "DomainId", "DomainName");
            ViewBag.OrgBrandId = new SelectList(db.OrgBrands, "OrgBrandId", "OrgBrandName");
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName");
            return PartialView("~/Views/Shared/PartialViewsForms/_AddOrg.cshtml");
        }

        // POST: Orgs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Org org)
        {
            var rud = Session["Email"];
            var loggedinuser = db.RegisteredUsers.Where(x => x.Email == rud.ToString()).Select(x => x.Email).FirstOrDefault();
            var orgredirect = db.RegisteredUserOrganisations.Where(x => x.Email == rud.ToString()).Select(x => x.OrgId).FirstOrDefault();
            if (rud.ToString() != loggedinuser)
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

                // list of groups
                var groups = db.GroupTypes.Where(x => (x.GroupOrgTypeId == org.OrgTypeId) || x.GroupOrgTypeId == null).Select(g => g.GroupTypeId).ToList();
                var grouptypeid = new List<int>(groups);
                foreach (var newgrp in groups)
                {
                    var groupname = db.GroupTypes.Where(x => x.GroupTypeId == newgrp).Select(s => s.GroupTypeName).FirstOrDefault();
                    var grouporgtypeid = db.GroupTypes.Where(x => x.GroupTypeId == newgrp).Select(s => s.GroupOrgTypeId).FirstOrDefault();
                    var grouprefnumb = db.GroupTypes.Where(x => x.GroupTypeId == newgrp).Select(s => s.GroupRefNumb).FirstOrDefault();
                    var orggroup = new OrgGroup()
                    {
                        OrgId = org.OrgId,
                        GroupName = groupname,
                        CreationDate = DateTime.Now,
                        GroupTypeId = newgrp,
                        GroupOrgTypeId = grouporgtypeid,
                        GroupRefNumb = grouprefnumb
                    };
                    db.OrgGroups.Add(orggroup);
                    db.SaveChanges();
                }

                return RedirectToAction("SystemAdminIndex", "Orgs", new { id = orgredirect });

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

                var changedorg = db.OrgOrgTypes.AsNoTracking().Where(x => x.OrgId == org.OrgId).FirstOrDefault();
                var orgorgtype = new OrgOrgType
                {
                     OrgOrgTypeId = changedorg.OrgOrgTypeId,
                     OrgId = changedorg.OrgId,
                     OrgTypeId = changedorg.OrgTypeId,
                     OrgName = org.OrgName
                };
                changedorg = orgorgtype;
                db.Entry(changedorg).State = EntityState.Modified;
                db.SaveChanges();

                //Update RegisteredUserOrganisations.Orgname 
                var userlinkedtoorg = db.RegisteredUserOrganisations.Where(x => x.OrgId == changedorg.OrgId).Select(p => p.RegisteredUserId).ToList();
                var listofusers = new List<int>(userlinkedtoorg);

                foreach (var usr in userlinkedtoorg)
                {
                    var getuser = db.RegisteredUserOrganisations.AsNoTracking().Where(x => x.RegisteredUserId == usr).FirstOrDefault();

                    var reguserorg = new RegisteredUserOrganisation
                    {
                        RegisteredUserOrganisationId = getuser.RegisteredUserOrganisationId,
                        RegisteredUserId = getuser.RegisteredUserId,
                        OrgId = getuser.OrgId,
                        Email = getuser.Email,
                        FirstName = getuser.FirstName,
                        LastName = getuser.LastName,
                        OrgName = org.OrgName,
                        RegUserOrgBrand = getuser.RegUserOrgBrand,
                        IsTester = getuser.IsTester,
                        RegisteredUserTypeId = getuser.RegisteredUserId,
                        PrimarySchoolUserRoleId = getuser.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = getuser.SecondarySchoolUserRoleId,
                        EnrolmentDate = getuser.EnrolmentDate,
                        CreatedBy = getuser.CreatedBy,
                        FullName = getuser.FullName,
                        TitleId = getuser.TitleId,
                        LastLogOn = getuser.LastLogOn,
                        RegistrationFlags = getuser.RegistrationFlags,
                    };
                    getuser = reguserorg;
                    db.Entry(getuser).State = EntityState.Modified;
                    db.SaveChanges();

                }

                return RedirectToAction("SystemAdminIndex");
            }
            ViewBag.DomainId = new SelectList(db.Domains, "DomainId", "DomainName", org.DomainId);
            ViewBag.OrgBrandId = new SelectList(db.OrgBrands, "OrgBrandId", "OrgBrandName", org.OrgBrandId);
            ViewBag.OrgTypeId = new SelectList(db.OrgTypes, "OrgTypeId", "OrgTypeName", org.OrgTypeId);
            return View(org);
        }



        // POST: Orgs/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Org org = db.Orgs.Find(id);
            db.Orgs.Remove(org);
            db.SaveChanges();
            return RedirectToAction("SystemAdminIndex");
        }

        public ActionResult SystemAdminIndex()
        {
            if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
            {
                return RedirectToAction("WrongDevice", "Orgs");

            }
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
            
            }
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var org = db.Orgs.Where(x => x.OrgTypeId != 5).Include(t => t.OrgType).ToList();

   
            return View(org);
        }


        public ActionResult WrongDevice()
        {
            return View();
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