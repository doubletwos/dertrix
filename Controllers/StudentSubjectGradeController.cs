﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;
using Dertrix.ViewModels;


namespace Dertrix.Controllers
{
    public class StudentSubjectGradeController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentSubjectGrade/Grades
        public ActionResult Grades()
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
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var studentSubject = db.StudentSubjectGrades
                    .Where(p => p.OrgId == i)
                    .Include(s => s.RegisteredUser)
                    .Include(s => s.Subject);

                return View(studentSubject.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }        
        }


        public ActionResult YourChild(int id)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var yourchild = db.StudentSubjectGrades.Where(p => p.OrgId == i).Where(x => x.RegisteredUserId == id);
                return View(yourchild.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }



        [ChildActionOnly]
        public ActionResult StudentUpdateSubject()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            return PartialView("~/Views/Shared/PartialViewsForms/_StudentUpdateSubject.cshtml");
        }



        // Create subjects and Grades for newly added students 
        [HttpPost]
        public ActionResult CreateStudentModules(int? classid,int studid,int? classref,int i6) 
        {
            try
            {
                // Locate list of subjects linked to class
                var subjects = db.Subjects.Where(x => x.ClassId == classid).Where(x => x.SubjectOrgId == i6).Select(x => x.SubjectId).ToList();
                var listofsubjects = new List<int>(subjects);

                foreach (var sb in subjects)
                {
                    var subjectname = db.Subjects.Where(s => s.ClassId == classid).Where(x => x.SubjectId == sb).Select(c => c.SubjectName).FirstOrDefault();
                    var studentsubjects = new StudentSubjectGrade()
                    {
                        RegisteredUserId = studid,
                        ClassId = classid,
                        ClassRef = classref,
                        OrgId = i6,
                        SubjectId = sb,
                        SubjectName = subjectname,
                        FirstTerm_ExamGrade = 00.0m,
                        SecondTerm_ExamGrade = 00.0m,
                        ThirdTerm_ExamGrade = 00.0m,
                        FirstTerm_TestGrade = 00.0m,
                        SecondTerm_TestGrade = 00.0m,
                        ThirdTerm_TestGrade = 00.0m,
                        Created_date = DateTime.Now,
                        Updater_Id = 0,
                        Last_updated_date = DateTime.Now,
                    };
                    db.StudentSubjectGrades.Add(studentsubjects);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        [HttpPost]
        public ActionResult AddNewSubjectToExistingStudents(int? classid,int? orgid,int subid, int? classref)  
        {
            try
            {
                //Get all students in class to list
                var studs = db.RegisteredUsers
                    .Where(x => x.ClassId == classid)
                    .Select(x => x.RegisteredUserId).ToList();

                //Create List
                var studentlist = new List<int>(studs);

                // Loop through students
                foreach (var student in studentlist)
                {
                    var subjectname = db.Subjects.Where(s => s.ClassId == classid).Where(x => x.SubjectId == subid).Select(c => c.SubjectName).FirstOrDefault();
                    var studentsubjects = new StudentSubjectGrade()
                    {
                        RegisteredUserId = student,
                        SubjectId = subid,
                        ClassId = classid,
                        ClassRef = classref,
                        OrgId = orgid,
                        FirstTerm_ExamGrade = 00.0m,
                        SecondTerm_ExamGrade = 00.0m,
                        ThirdTerm_ExamGrade = 00.0m,
                        FirstTerm_TestGrade = 00.0m,
                        SecondTerm_TestGrade = 00.0m,
                        ThirdTerm_TestGrade = 00.0m,
                        Last_updated_date = DateTime.Now,
                        Created_date = DateTime.Now,
                        SubjectName = subjectname,
                        Updater_Id = 0,
                    };
                    db.StudentSubjectGrades.Add(studentsubjects);
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index", "StudentSubjects");

        }




  
        public ActionResult MyClass(int? id)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                var OrgId = (int)Session["OrgId"];

                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
                var myclasses = db.Classes.Where(x => x.ClassTeacherId == RegisteredUserId && x.ClassId == id).Select(x => x.ClassId).FirstOrDefault();
                var mystudents = db.StudentSubjectGrades
                    .Where(x => x.ClassId == id && x.ClassId == myclasses)
                    .Include(s => s.Subject)
                    .Include(r => r.RegisteredUser)
                    .ToList();

                return View(mystudents);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        public ActionResult ClassProfile(int id)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var classprofile = db.Classes
                    .Where(x => x.ClassId == id)
                    .Include(x => x.Title)
                    .ToList();

                return PartialView("_ClassProfile", classprofile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        //Update students Grades
        public ActionResult DisplayStudentGrades(int? id) 
        {
            try
            {
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);

                // Get all the subjects from the database
                var ssg = db.StudentSubjectGrades
                    .Where(x => x.RegisteredUserId == id)
                    .Include(x => x.Subject)
                    .ToList();

                var registereduser = db.RegisteredUsers.Find(id);

                // Get students class id

                var classid = db.RegisteredUsers
                    .Where(x => x.RegisteredUserId == id)
                    .Select(x => x.ClassId)
                    .FirstOrDefault();

                // Get all the subjects linked to student
                var subjects = db.Subjects
                    .Where(x => x.ClassId == classid)
                    .Where(x => x.SubjectOrgId == i)
                    .ToList();

                // Initialize the view model
                var displayssgviewmodel = new DisplayStudentGradesViewModel
                {
                    RegisteredUser = registereduser,

                    StudentSubjectGrades = ssg.Select(x => new StudentSubjectGrade()
                    {
                        StudentSubjectGradeId = x.StudentSubjectGradeId,
                        OrgId = x.OrgId,
                        SubjectId = x.SubjectId,
                        SubjectName = x.SubjectName,
                        FirstTerm_ExamGrade = x.FirstTerm_ExamGrade,
                        SecondTerm_ExamGrade = x.SecondTerm_ExamGrade,
                        ThirdTerm_ExamGrade = x.ThirdTerm_ExamGrade,
                        FirstTerm_TestGrade = x.FirstTerm_TestGrade,
                        SecondTerm_TestGrade = x.SecondTerm_TestGrade,
                        ThirdTerm_TestGrade = x.ThirdTerm_TestGrade,
                        RegisteredUserId = x.RegisteredUserId,
                        Created_date = x.Created_date,
                        Last_updated_date = x.Last_updated_date,
                        ClassRef = x.ClassRef,
                        ClassId = x.ClassId,
                        Updater_Id = x.Updater_Id,

                    }).ToList(),

                    Subject = subjects.Select(x => new Subject()
                    {
                        SubjectId = x.SubjectId,
                        SubjectName = x.SubjectName,
                        ClassId = x.ClassId,
                        ClassTeacherId = x.ClassTeacherId,
                        TaughtBy = x.TaughtBy,
                        SubjectOrgId = x.SubjectOrgId,
                        First_Term_Test_MaxGrade = x.First_Term_Test_MaxGrade,
                        Second_Term_Test_MaxGrade = x.Second_Term_Test_MaxGrade,
                        Third_Term_Test_MaxGrade = x.Third_Term_Test_MaxGrade,
                        First_Term_Exam_MaxGrade = x.First_Term_Exam_MaxGrade,
                        Second_Term_Exam_MaxGrade = x.Second_Term_Exam_MaxGrade,
                        Third_Term_Exam_MaxGrade = x.Third_Term_Exam_MaxGrade,
                        Created_date = x.Created_date,
                        Creator_Id = x.Creator_Id
                    }).ToList(),
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_DisplayStudentGrades.cshtml", displayssgviewmodel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index", "StudentSubjects");
        }



        //Update students Grades
        public ActionResult UpdateStudentGrade(int id)
        {
            try
            {
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);


                //var sub = db.StudentSubjectGrades
                //    .Where(x => x.RegisteredUserId == id)
                //    .Include(x => x.Subject)
                //    .ToList();


                // Get all the subjects from the database
                var ssg = db.StudentSubjectGrades
                    .Where(x => x.RegisteredUserId == id)
                    .Include(x => x.Subject)
                    .ToList();
                var registereduser = db.RegisteredUsers.Find(id);

                var subject = new Subject();

                // Initialize the view model
                var updatessgviewmodel = new UpdateStudentGradesViewModel
                {
                    StudentSubjectGrades = ssg.Select(x => new StudentSubjectGrade()
                    {
                        StudentSubjectGradeId = x.StudentSubjectGradeId,
                        OrgId = x.OrgId,
                        SubjectId = x.SubjectId,
                        SubjectName = x.SubjectName,
                        FirstTerm_ExamGrade = x.FirstTerm_ExamGrade,
                        SecondTerm_ExamGrade = x.SecondTerm_ExamGrade,
                        ThirdTerm_ExamGrade = x.ThirdTerm_ExamGrade,
                        FirstTerm_TestGrade = x.FirstTerm_TestGrade,
                        SecondTerm_TestGrade = x.SecondTerm_TestGrade,
                        ThirdTerm_TestGrade = x.ThirdTerm_TestGrade,
                        RegisteredUserId = x.RegisteredUserId,
                        Created_date = x.Created_date,
                        Last_updated_date = x.Last_updated_date,
                        ClassRef = x.ClassRef,
                        ClassId = x.ClassId,
                        Updater_Id = x.Updater_Id,

                    }).ToList(),

                    RegisteredUser = registereduser,
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_UpdateStudentGrades.cshtml", updatessgviewmodel);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index", "StudentSubjects");

        }


        // Update Subject Name on StudentSubjectGrade records for all students
        public ActionResult UpdateStudentSubjectData(int subid, int classid, int? orgid) 
        {
            try
            {
                //Get all students in class to list
                var studs = db.StudentSubjectGrades
                    .Where(x => x.ClassId == classid)
                    .Where(x => x.SubjectId == subid)
                    .Select(x => x.StudentSubjectGradeId).ToList();

                //Create List
                var studentlist = new List<int>(studs);

                //Get Subject Name
                var subjectname = db.Subjects.Where(s => s.ClassId == classid).Where(x => x.SubjectId == subid).Select(c => c.SubjectName).FirstOrDefault();

                foreach (var stud in studs) 
                {
                    // locate student 
                    var student = db.StudentSubjectGrades.AsNoTracking().Where(x => x.StudentSubjectGradeId == stud && x.SubjectId == subid).FirstOrDefault();
                    // class ref
                    var classref = db.Classes.Where(x => x.ClassId == classid).Select(x => x.ClassRefNumb).FirstOrDefault();

                    var updateStudent = new StudentSubjectGrade
                    {
                        StudentSubjectGradeId = student.StudentSubjectGradeId,
                        RegisteredUserId = student.RegisteredUserId,
                        SubjectId = subid,
                        ClassId = classid,
                        ClassRef = classref,
                        OrgId = orgid,
                        FirstTerm_ExamGrade = student.FirstTerm_ExamGrade,
                        SecondTerm_ExamGrade = student.SecondTerm_ExamGrade,
                        ThirdTerm_ExamGrade = student.ThirdTerm_ExamGrade,
                        FirstTerm_TestGrade = student.FirstTerm_TestGrade,
                        SecondTerm_TestGrade = student.SecondTerm_TestGrade,
                        ThirdTerm_TestGrade = student.ThirdTerm_TestGrade,
                        Last_updated_date = student.Last_updated_date,
                        Created_date = student.Created_date,
                        SubjectName = subjectname,
                        Updater_Id = student.Updater_Id,
                    };
                    student = updateStudent;
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new HttpStatusCodeResult(204);
        }









        // POST: StudentSubjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentSubjectGrade studentSubject)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.StudentSubjectGrades.Add(studentSubject);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", studentSubject.RegisteredUserId);
                ViewBag.SelectedSubjects = new MultiSelectList(db.Subjects, "SubjectId", "SubjectName");

                ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", studentSubject.SubjectId);
                return View(studentSubject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }



        // POST: StudentSubjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateStudentGradesViewModel viewmodel)
        {
            try
            {
                foreach (var grade in viewmodel.StudentSubjectGrades) 
                {
                    // Locate record 
                    var grade_id = db.StudentSubjectGrades.AsNoTracking()
                        .Where(x => x.StudentSubjectGradeId == grade.StudentSubjectGradeId).FirstOrDefault();

                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);

                    var updategrade = new StudentSubjectGrade
                    {
                        StudentSubjectGradeId = grade.StudentSubjectGradeId,
                        RegisteredUserId = grade.RegisteredUserId,
                        SubjectId = grade.SubjectId,
                        ClassId = grade.ClassId,
                        ClassRef = grade.ClassRef,
                        OrgId = grade.OrgId,
                        FirstTerm_ExamGrade = grade.FirstTerm_ExamGrade,
                        SecondTerm_ExamGrade = grade.SecondTerm_ExamGrade,
                        ThirdTerm_ExamGrade = grade.ThirdTerm_ExamGrade,
                        FirstTerm_TestGrade = grade.FirstTerm_TestGrade,
                        SecondTerm_TestGrade = grade.SecondTerm_TestGrade,
                        ThirdTerm_TestGrade = grade.ThirdTerm_TestGrade,
                        Last_updated_date = DateTime.Now,
                        Created_date = grade.Created_date,
                        SubjectName = grade.SubjectName,
                        Updater_Id = i
                    };

                    grade_id = updategrade;
                    db.Entry(grade_id).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Grades");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // Method not in use
        //// GET: StudentSubjectGrade/MySubjects
        //public ActionResult MySubjects(int? id, int? ij, string searchname, string searchid)
        //{
        //    if (Session["OrgId"] == null)
        //    {
        //        return RedirectToAction("Signin", "Access");
        //    }
        //    var rr = Session["OrgId"].ToString();
        //    int i = Convert.ToInt32(rr);

        //    var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);


        //    string subjectid = id.ToString();

        //    // returns students of org if class is selected
        //    if (string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid) && (!string.IsNullOrWhiteSpace(subjectid)))
        //    {
        //        return View(db.StudentSubjectGrades.Where(p => p.SubjectId == id && p.OrgId == i).Include(s => s.Subject).Include(r => r.RegisteredUser).ToList());
        //    }

        //    // returns students of org if fullname is provided
        //    if (!string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid))
        //    {

        //    }

        //    // returns students of org if studentid is provided
        //    if (string.IsNullOrWhiteSpace(searchname) && !string.IsNullOrWhiteSpace(searchid))
        //    {
        //        int reguserid = Convert.ToInt32(searchid);
        //        return View(db.StudentSubjectGrades.Where(n => n.RegisteredUserId == reguserid).Include(s => s.Subject).Include(r => r.RegisteredUser).ToList());

        //    }


        //    var studentSubject = db.StudentSubjectGrades.Where(p => p.SubjectId == id).Include(s => s.RegisteredUser).Include(s => s.Subject);
        //    return View(studentSubject.ToList());
        //}

        // POST: StudentSubjects/Delete/5
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    StudentSubjectGrade studentSubject = db.StudentSubjectGrades.Find(id);
        //    db.StudentSubjectGrades.Remove(studentSubject);
        //    db.SaveChanges();
        //    return RedirectToAction("Grades");
        //}
    }
}
