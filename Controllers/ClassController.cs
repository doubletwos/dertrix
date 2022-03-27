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

        [ChildActionOnly]
        public ActionResult AddOrgClass()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
                return PartialView("~/Views/Shared/PartialViewsForms/_AddOrgClass.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        public ActionResult AllClassList()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var Allclasslist = db.Classes
                    .Where(x => x.OrgId == i)
                    .Include(x => x.Title)
                    .ToList();
                return PartialView("_AllClassList", Allclasslist);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }

        // Partial view to display list of class a teacher
        [ChildActionOnly]
        public ActionResult AllClassCount()
        {
            try
            {

                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var AllclassCount = db.Classes
                    .Where(x => x.OrgId == i)
                    .ToList();
                return PartialView("_AllClassCount", AllclassCount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        // This action is used at school level to assign teachers to class
        public ActionResult AssignClassTeacher(int? id)
        {

            try
            {
                if (id != 0)
                {
                    @Class @Class = db.Classes.Find(id);
                    if (@Class == null)
                    {
                        return HttpNotFound();
                    }
                    var classroom = db.Classes
                        .Where(x => x.ClassId == id)
                        .FirstOrDefault();

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
                        TitleId = classroom.TitleId,
                        ClassTeacherFullName = classroom.ClassTeacherFullName,
                        Students_Count = classroom.Students_Count,
                        Female_Students_Count = classroom.Female_Students_Count,
                        Male_Students_Count = classroom.Male_Students_Count
                    };
                    ViewBag.ClassTeacherId = new SelectList(db.RegisteredUserOrganisations
                        .Where(x => x.OrgId == i)
                        .Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4) || (j.NurserySchoolUserRoleId == 3 )), "RegisteredUserId", "FullName", classroom.ClassTeacherId);

                    return PartialView("~/Views/Shared/PartialViewsForms/_AssignClassTeacher.cshtml", cr);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(id);
            }
            return new HttpStatusCodeResult(204);
        }

        public ActionResult ClassDetails(int Id)
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
                    var cla = db.Classes.Where(x => x.ClassId == Id);
                    ViewBag.Class = cla;
                    return PartialView("~/Views/Shared/DisplayViews/_ClassDetails.cshtml");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        // POST: Class/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(@Class @Class)
        {
            try
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
                return RedirectToAction("SystemAdminIndex");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(@Class);
            }
        }

        // This action is used at school level to assign teachers to class
        // POST: Class/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(@Class @Class)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var teacherstitle = db.RegisteredUsers
                        .Where(x => x.RegisteredUserId == Class.ClassTeacherId)
                        .Select(x => x.TitleId)
                        .FirstOrDefault();

                    var teachersName = db.RegisteredUsers
                        .Where(x => x.RegisteredUserId == Class.ClassTeacherId)
                        .Select(x => x.FullName)
                        .FirstOrDefault();

                    Class.ClassTeacherFullName = teachersName;
                    Class.TitleId = teacherstitle;

                    db.Entry(@Class).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", @Class.OrgId);
                ViewBag.ClassTeacherId = new SelectList(db.RegisteredUsers
                    .Where(x => x.SelectedOrg == i)
                    .Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4) || (j.NurserySchoolUserRoleId == 3)), "RegisteredUserId", "FullName");
                return View(@Class);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(@Class);
            }

        }

        public ActionResult EditClass(int Id)
        {
            try
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
                        TitleId = edtcla.TitleId,
                        ClassTeacherFullName = edtcla.ClassTeacherFullName,
                        OrgId = edtcla.OrgId,
                        Students_Count = edtcla.Students_Count,
                        Female_Students_Count = edtcla.Female_Students_Count,
                        Male_Students_Count = edtcla.Male_Students_Count
                    };
                    ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", @Class.OrgId);
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditClass.cshtml", edt1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        // GET: Class/Index
        //School admins to manage class
        public ActionResult Index(int? id)
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
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);
                    id = i;
                    var classes = db.Classes
                        .Where(f => f.OrgId == i)
                        .Include(j => j.Org)
                        .Include(x => x.RegisteredUsers)
                        .Include(x => x.Title);
                    return View(classes.ToList());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        [ChildActionOnly]
        public ActionResult MyClassCount()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                var myclassCount = db.Classes
                    .Where(x => x.OrgId == i)
                    .Where(j => j.ClassTeacherId == RegisteredUserId)
                    .ToList();
                return PartialView("_MyClassCount", myclassCount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        // Partial view to display list of class a teacher
        public ActionResult MyClassList()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                var myclasslist = db.Classes
                    .Where(x => x.OrgId == i)
                    .Where(j => j.ClassTeacherId == RegisteredUserId)
                    .Include(x => x.Title)
                    .ToList();
                return PartialView("_MyClassList", myclasslist);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        // GET: Class/SystemAdminIndex
        //Sys admin to manager classes
        public ActionResult SystemAdminIndex(int? id)
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
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);
                    id = i;
                    var classes = db.Classes.Include(j => j.Org);
                    return View(classes.ToList());
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


        //// POST: Class/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    @Class @Class = db.Classes.Find(id);
        //    db.Classes.Remove(@Class);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


    }
}