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
    public class ClassController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Class/SystemAdminIndex
        public ActionResult SystemAdminIndex(int? id)
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
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                id = i;
                var classes = db.Classes.Include(j => j.Org);
                return View(classes.ToList());
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Class/Index
        public ActionResult Index(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }        
            if ((int)Session["OrgId"] != 23)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                id = i;
                var classes = db.Classes
                    .Where(f => f.OrgId == i)
                    .Include(j => j.Org)
                    .Include(x => x.RegisteredUsers);
                return View(classes.ToList());
            }
            else
            return RedirectToAction("SystemAdminIndex");
        }

        public ActionResult ClassDetails(int Id)
        {
            var cla = db.Classes.Where(x => x.ClassId == Id);
            ViewBag.Class = cla;
            return PartialView("~/Views/Shared/PartialViewsForms/_ClassDetails.cshtml");
        }


        [ChildActionOnly]
        public ActionResult AddOrgClass()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.ClassTeacherId = new SelectList(db.RegisteredUsers.Where(x => x.SelectedOrg == i).Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4)), "RegisteredUserId", "FullName");
            return PartialView("~/Views/Shared/PartialViewsForms/_AddOrgClass.cshtml");
        }

        // POST: Class/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(@Class @Class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@Class);
                db.SaveChanges();
                return RedirectToAction("SystemAdminIndex");
            }
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", @Class.OrgId);
            ViewBag.ClassTeacherId = new SelectList(db.RegisteredUsers.Where(x => x.SelectedOrg == i).Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4)), "RegisteredUserId", "FullName");
            return View(@Class);
        }

        public ActionResult EditClass(int Id)
        {
            if (Id != 0)
            {
                var edtcla = db.Classes.Where(x => x.ClassId == Id).FirstOrDefault();
                @Class @Class = db.Classes.Find(Id);
                var edt1 = new Class
                {
                    ClassId = edtcla.ClassId,
                    ClassIsActive = edtcla.ClassIsActive,
                    ClassName = edtcla.ClassName,
                    ClassRefNumb = edtcla.ClassRefNumb,
                    ClassTeacherId = edtcla.ClassTeacherId,
                    ClassTeacherFullName = edtcla.ClassTeacherFullName,
                    OrgId = edtcla.OrgId
                };
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", @Class.OrgId);
                return PartialView("~/Views/Shared/PartialViewsForms/_EditClass.cshtml", edt1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditClass.cshtml");
        }

        // This action is used at school level to assign teachers to class
        // GET: Class/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
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
            var classroom = db.Classes.Where(x => x.ClassId == id).FirstOrDefault();
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var cr = new Class
            {
                ClassId = classroom.ClassId,
                ClassName = classroom.ClassName,
                ClassIsActive = classroom.ClassIsActive,
                OrgId = classroom.OrgId,
                ClassRefNumb = classroom.ClassRefNumb,
                ClassTeacherId = classroom.ClassTeacherId,
                ClassTeacherFullName = classroom.ClassTeacherFullName
            };
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", @Class.OrgId);
            ViewBag.ClassTeacherId = new SelectList(db.RegisteredUsers.Where(x => x.SelectedOrg == i).Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4)), "RegisteredUserId", "FullName", classroom.ClassTeacherId);
            return View(@Class);
        }

        // This action is used at school level to assign teachers to class
        // POST: Class/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(@Class @Class)
        {
            if (ModelState.IsValid)
            {
                var teachersName = db.RegisteredUsers.Where(x => x.RegisteredUserId == Class.ClassTeacherId).Select(x => x.FullName).FirstOrDefault();
                Class.ClassTeacherFullName = teachersName;
                db.Entry(@Class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", @Class.OrgId);
            ViewBag.ClassTeacherId = new SelectList(db.RegisteredUsers.Where(x => x.SelectedOrg == i).Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4)), "RegisteredUserId", "FullName");
            return View(@Class);
        }


        // POST: Class/Delete/5
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