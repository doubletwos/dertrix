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

        [ChildActionOnly]
        public ActionResult AddGroupType()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddGroupType.cshtml");
        }

        // POST: GroupTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupType groupType)
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
                        groupType.Creator_Id = Session["RegisteredUserId"].ToString();
                        groupType.Created_date = DateTime.Now;
                        db.GroupTypes.Add(groupType);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(groupType);
            }
            return View(groupType);

        }

        // POST: GroupTypes/Delete/5
        public ActionResult DeleteConfirmed(int id)
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
                    GroupType groupType = db.GroupTypes.Find(id);
                    db.GroupTypes.Remove(groupType);
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


        // POST: GroupTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupType groupType)
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
                        db.Entry(groupType).State = EntityState.Modified;
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


        public ActionResult EditGroupType(int Id)
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
                        var edtgrptye = db.GroupTypes.Where(x => x.GroupTypeId == Id).FirstOrDefault();
                        GroupType grouptype = db.GroupTypes.Find(Id);
                        var edtgrptye1 = new GroupType
                        {
                            GroupTypeId = edtgrptye.GroupTypeId,
                            GroupOrgTypeId = edtgrptye.GroupOrgTypeId,
                            GroupRefNumb = edtgrptye.GroupRefNumb,
                            GroupTypeName = edtgrptye.GroupTypeName,
                            Created_date = edtgrptye.Created_date,
                            Creator_Id  = edtgrptye.Creator_Id,
                        };
                        return PartialView("~/Views/Shared/PartialViewsForms/_EditGroupType.cshtml", edtgrptye1);
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


        // GET: GroupTypes
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
                    return View(db.GroupTypes.ToList());
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