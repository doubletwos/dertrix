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
    public class SubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subjects
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
                    var rr = (int)Session["OrgId"];
                    int i = Convert.ToInt32(rr);
                    var subjects = db.Subjects
                        .Where(s => s.SubjectOrgId == rr)
                        .Include(s => s.Class);
                    return View(subjects.ToList());
                }
                else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }




        [ChildActionOnly]
        public ActionResult AddSubject()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                ViewBag.ClassTeacherId = new SelectList(
                from x in db.RegisteredUserOrganisations
                .Where(x => x.OrgId == i)
                .Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4))
                select new { x.RegisteredUserId, x.FullName, Name_Id = x.FullName + " " + "[" + x.RegisteredUserId + "]" },
                "RegisteredUserId", "Name_Id");

                ViewBag.ClassId = new SelectList(
                from x in db.Classes
                .Where(x => x.OrgId == i)
                .OrderBy(w => w.ClassRefNumb)
                .ToList()
                select new { x.ClassId, x.ClassName, Name_Id = x.ClassName + " " + "[" + x.ClassId + "]" },
                "ClassId", "Name_Id");


                //ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.OrgId == i)
                //    .OrderBy(w => w.ClassRefNumb)
                //    .ToList(), "ClassId", "ClassName");

                return PartialView("~/Views/Shared/PartialViewsForms/_AddSubject.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }

        public ActionResult EditSubject(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);

                    var edtsubject = db.Subjects.Where(x => x.SubjectId == Id).FirstOrDefault();

                    var edtsubject1 = new Subject
                    {
                        SubjectId = edtsubject.SubjectId,
                        SubjectName = edtsubject.SubjectName,
                        ClassId = edtsubject.ClassId,
                        ClassTeacherId = edtsubject.ClassTeacherId,
                        First_Term_Exam_MaxGrade = edtsubject.First_Term_Exam_MaxGrade,
                        Second_Term_Exam_MaxGrade = edtsubject.Second_Term_Exam_MaxGrade,
                        Third_Term_Exam_MaxGrade = edtsubject.Third_Term_Exam_MaxGrade,
                        First_Term_Test_MaxGrade = edtsubject.First_Term_Test_MaxGrade,
                        Second_Term_Test_MaxGrade = edtsubject.Second_Term_Test_MaxGrade,
                        Third_Term_Test_MaxGrade = edtsubject.Third_Term_Test_MaxGrade,
                        SubjectOrgId = edtsubject.SubjectOrgId,
                        Created_date = edtsubject.Created_date,
                        Creator_Id = edtsubject.Creator_Id,

                    };

                    ViewBag.ClassTeacherId = new SelectList(db.RegisteredUserOrganisations.Where(x => x.OrgId == i).Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4)), "RegisteredUserId", "FullName", edtsubject1.ClassTeacherId);
                    ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.OrgId == i).OrderBy(w => w.ClassRefNumb).ToList(), "ClassId", "ClassName", edtsubject.ClassId);

                    return PartialView("~/Views/Shared/PartialViewsForms/_EditSubject.cshtml", edtsubject1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditSubject.cshtml");
        }



        // POST: Subjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                if (ModelState.IsValid)
                {
                    //throw new HttpException(400, "A custom message for an application specific exception");

                    var taughtby = db.RegisteredUsers.Where(x => x.RegisteredUserId == subject.ClassTeacherId).Select(x => x.FullName).FirstOrDefault();
                    subject.TaughtBy = taughtby;
                    var classref = db.Classes.Where(x => x.ClassId == subject.ClassId).Select(x => x.ClassRefNumb).FirstOrDefault();
                    var orgid = subject.SubjectOrgId = (int)Session["OrgId"];
                    subject.Creator_Id = Session["RegisteredUserId"].ToString();
                    subject.Created_date = DateTime.Now;
                    db.Subjects.Add(subject);
                    db.SaveChanges();
                    var classid = subject.ClassId;
                    var subid = subject.SubjectId;

                    // CALL METHOD TO UPDATE EXISTING STUDENTS
                    var otherController1 = DependencyResolver.Current.GetService<StudentSubjectGradeController>();
                    var result1 = otherController1.AddNewSubjectToExistingStudents(classid, orgid, subid, classref);

                }
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);


                ViewBag.ClassTeacherId = new SelectList(db.RegisteredUsers
                    .Where(x => x.SelectedOrg == i)
                    .Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4)), "RegisteredUserId", "FullName");
                ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", subject.ClassId);

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(subject);
            }
            return new HttpStatusCodeResult(204);

        }




        [ChildActionOnly]
        public ActionResult MySubjectsCount()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                var mysubjectsCount = db.Subjects
                    .Where(x => x.SubjectOrgId == i)
                    .Where(j => j.ClassTeacherId == RegisteredUserId)
                    .ToList();
                return PartialView("_MySubjectsCount", mysubjectsCount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
 
        }


        public ActionResult MySubjectsList()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                var mysubjectsCount = db.Subjects
                    .Where(x => x.SubjectOrgId == i)
                    .Where(j => j.ClassTeacherId == RegisteredUserId)
                    .Include(s => s.Class)
                    .ToList();
                return PartialView("_MySubjectsList", mysubjectsCount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }








        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subject subject)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                if (ModelState.IsValid)
                {
                    var taughtby = db.RegisteredUsers.Where(x => x.RegisteredUserId == subject.ClassTeacherId).Select(x => x.FullName).FirstOrDefault();
                    subject.TaughtBy = taughtby;

                    db.Entry(subject).State = EntityState.Modified;
                    db.SaveChanges();

                    var orgid = subject.SubjectOrgId;
                    var subid = subject.SubjectId;
                    var classid = subject.ClassId;

                    // Call Method to update students linked to subject
                    var foreignController = DependencyResolver.Current.GetService<StudentSubjectGradeController>();
                    var result = foreignController.UpdateStudentSubjectData(subid, classid, orgid);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", subject.ClassId);
            return View(subject);
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //// POST: Subjects/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    if (Session["OrgId"] == null)
        //    {
        //        return RedirectToAction("Signin", "Access");
        //    }

        //    Subject subject = db.Subjects.Find(id);
        //    db.Subjects.Remove(subject);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
