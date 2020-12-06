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
    public class GroupTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: GroupTypes
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
            return View(db.GroupTypes.ToList());
        }

        [ChildActionOnly]
        public ActionResult AddGroupType()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddGroupType.cshtml");
        }

        public ActionResult EditGroupType(int Id)
        {
            if (Id != 0)
            {
                var edtgrptye = db.GroupTypes.Where(x => x.GroupTypeId == Id).FirstOrDefault();
                GroupType grouptype = db.GroupTypes.Find(Id);
                var edtgrptye1 = new GroupType
                {
                    GroupTypeId = edtgrptye.GroupTypeId,
                    GroupOrgTypeId = edtgrptye.GroupOrgTypeId,
                    GroupRefNumb = edtgrptye.GroupRefNumb,
                    GroupTypeName = edtgrptye.GroupTypeName,
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditGroupType.cshtml", edtgrptye1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditGroupType.cshtml");
        }

        // POST: GroupTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupType groupType)
        {
            if (ModelState.IsValid)
            {
                db.GroupTypes.Add(groupType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupType);
        }



        // POST: GroupTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupType groupType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupType);
        }


        // POST: GroupTypes/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            GroupType groupType = db.GroupTypes.Find(id);
            db.GroupTypes.Remove(groupType);
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