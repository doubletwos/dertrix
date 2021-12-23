using System;
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

        // GET: StudentSubjects/Grades
        public ActionResult Grades()
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


            var studentSubject = db.StudentSubjectGrades.Where(p => p.OrgId == i).Include(s => s.RegisteredUser).Include(s => s.Subject);
            return View(studentSubject.ToList());
        }


        public ActionResult YourChild(int id)
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



        // GET: StudentSubjects/MySubjects
        public ActionResult MySubjects(int? id, int? ij, string searchname, string searchid)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);


            string subjectid = id.ToString();

            // returns students of org if class is selected
            if (string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid) && (!string.IsNullOrWhiteSpace(subjectid)))
            {
                return View(db.StudentSubjectGrades.Where(p => p.SubjectId == id && p.OrgId == i).Include(s => s.Subject).Include(r => r.RegisteredUser).ToList());
            }

            // returns students of org if fullname is provided
            if (!string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid))
            {

            }

            // returns students of org if studentid is provided
            if (string.IsNullOrWhiteSpace(searchname) && !string.IsNullOrWhiteSpace(searchid))
            {
                int reguserid = Convert.ToInt32(searchid);
                return View(db.StudentSubjectGrades.Where(n => n.RegisteredUserId == reguserid).Include(s => s.Subject).Include(r => r.RegisteredUser).ToList());

            }


            var studentSubject = db.StudentSubjectGrades.Where(p => p.SubjectId == id).Include(s => s.RegisteredUser).Include(s => s.Subject);
            return View(studentSubject.ToList());
        }

        // GET: StudentSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubjectGrade studentSubject = db.StudentSubjectGrades.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            return View(studentSubject);
        }

        // GET: StudentSubjects/Create
        public ActionResult Create()
        {

            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);




            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName");
            ViewBag.SelectedSubjects = new MultiSelectList(db.Subjects, "SubjectId", "SubjectName");


            return View();
        }


        [ChildActionOnly]
        public ActionResult StudentUpdateSubject()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            return PartialView("~/Views/Shared/PartialViewsForms/_StudentUpdateSubject.cshtml");
        }



         
        [HttpPost]
        public ActionResult CreateStudentModules(int? classid,int studid,int? classref,int i6) 
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
                    };
                    db.StudentSubjectGrades.Add(studentsubjects);
                    db.SaveChanges();
             }

            return RedirectToAction("Index", "StudentSubjects");

        }

        [HttpPost]
        public ActionResult AddNewSubjectToExistingStudents(int? classid, int studid, int? classref, int i6, int subid) 
        {
            //Get all students in class to list
            var studs = db.RegisteredUsers
                .Where(x => x.ClassId == classid)
                .Where(x => x.ClassRef == classref)
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
                    OrgId = i6,
                    FirstTerm_ExamGrade = 00.0m,
                    SecondTerm_ExamGrade = 00.0m,
                    ThirdTerm_ExamGrade = 00.0m,
                    FirstTerm_TestGrade = 00.0m,
                    SecondTerm_TestGrade = 00.0m,
                    ThirdTerm_TestGrade = 00.0m,
                    Last_updated_date = null,
                    Created_date = DateTime.Now,
                    SubjectName = subjectname
                };
                db.StudentSubjectGrades.Add(studentsubjects);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "StudentSubjects");

        }




        //classref list
        //var classreflist = db.Classes.Where(x => x.ClassRefNumb == classid).Where(o => o.OrgId == orgid).Select(p => p.ClassId).ToList();
        //var listofclasses = new List<int>(classreflist);



        //var classref = db.Classes.Where(x => x.ClassRefNumb == classid).Where(o => o.OrgId == orgid).Select(p => p.ClassRefNumb).FirstOrDefault();
        //foreach (var cr in classreflist)
        //{
        //    //subject ids
        //    var subjectid = db.Subjects.Where(s => s.ClassId == cr).Select(c => c.SubjectId).FirstOrDefault();
        //    //student list
        //    var students = db.RegisteredUsers.Where(x => x.ClassId == cr).Where(p => p.SelectedOrg == orgid).Where(s => s.StudentRegFormId != null).Select(k => k.RegisteredUserId).ToList();
        //    var studentid = new List<int>(students);
        //    //subject list
        //    var subjects = db.Subjects.Where(x => x.ClassId == cr).Select(c => c.SubjectId).ToList();
        //    var subject = new List<int>(subjects);
        //    foreach (var stu in students)
        //    {
        //        foreach (var sb in subjects)
        //        {
        //            var subjectexistcheck = db.StudentSubjectGrades.Where(x => x.RegisteredUserId == stu && x.SubjectId == sb).FirstOrDefault();
        //            if (subjectexistcheck == null)
        //            {
        //                var subjectname = db.Subjects.Where(s => s.ClassId == cr).Where(x => x.SubjectId == sb).Select(c => c.SubjectName).FirstOrDefault();
        //                var fullname = db.RegisteredUsers.Where(s => s.RegisteredUserId == stu).Select(f => f.FullName).FirstOrDefault();
        //                var studentsubjects = new StudentSubjectGrade()
        //                {
        //                    RegisteredUserId = stu,
        //                    ClassId = cr,
        //                    ClassRef = classref,
        //                    OrgId = orgid,
        //                    SubjectId = sb,
        //                    FirstTerm_ExamGrade = 00.0m,
        //                    SecondTerm_ExamGrade = 00.0m,
        //                    ThirdTerm_ExamGrade = 00.0m,
        //                    FirstTerm_TestGrade = 00.0m,
        //                    SecondTerm_TestGrade= 00.0m,
        //                    ThirdTerm_TestGrade = 00.0m
        //                };
        //                db.StudentSubjectGrades.Add(studentsubjects);
        //                db.SaveChanges();
        //            }
        //        }
        //    }
        //}



        public ActionResult MyClass(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            var OrgId = (int)Session["OrgId"];


            var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
            var myclasses = db.Classes.Where(x => x.ClassTeacherId == RegisteredUserId && x.ClassId == id).Select(x => x.ClassId).FirstOrDefault();
            var mystudents = db.StudentSubjectGrades.Where(x => x.ClassId == id && x.ClassId == myclasses)
                .Include(s => s.Subject).Include(r => r.RegisteredUser).ToList();

            return View(mystudents);

        }


        public ActionResult ClassProfile(int id)
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var classprofile = db.Classes
                .Where(x => x.ClassId == id)
                .Include(x => x.Title)
                .ToList();

            return PartialView("_ClassProfile", classprofile);
        }



        //[ChildActionOnly]
        public ActionResult UpdateStudentGrade(int id) 
        {
            var sess = Session["OrgId"].ToString();
            int i = Convert.ToInt32(sess);

            var orgschcalendar = new OrgSchCalendar();

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


                }).ToList(),

                RegisteredUser = registereduser,

             
                

            };
            return PartialView("~/Views/Shared/PartialViewsForms/_UpdateStudentGrades.cshtml", updatessgviewmodel);
        }






        public ActionResult UpdateStudentGrade2(int Id)
        {
            if (Id != 0)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var stud1 = db.StudentSubjectGrades
                    .Include(s => s.Subject)
                    .Include(r => r.RegisteredUser)
                    .Where(x => x.RegisteredUserId == Id)
                    .FirstOrDefault();

                var stud = new StudentSubjectGrade
                {
                    StudentSubjectGradeId = stud1.StudentSubjectGradeId,
                    RegisteredUserId = stud1.RegisteredUserId,
                    SubjectId = stud1.SubjectId,
                    ClassId = stud1.ClassId,
                    FirstTerm_ExamGrade = stud1.FirstTerm_ExamGrade,
                    SecondTerm_ExamGrade = stud1.SecondTerm_ExamGrade,
                    ThirdTerm_ExamGrade = stud1.ThirdTerm_ExamGrade,
                    FirstTerm_TestGrade = stud1.FirstTerm_TestGrade,
                    SecondTerm_TestGrade = stud1.SecondTerm_TestGrade,
                    ThirdTerm_TestGrade = stud1.ThirdTerm_TestGrade,
                    OrgId = stud1.OrgId,
                    ClassRef = stud1.ClassRef


                };

                return PartialView("~/Views/Shared/PartialViewsForms/_UpdateStudentGrade.cshtml", stud);
            }
            return PartialView("_UpdateStudentGrade");
        }










        // POST: StudentSubjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentSubjectGrade studentSubject)
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

        // GET: StudentSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubjectGrade studentSubject = db.StudentSubjectGrades.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", studentSubject.RegisteredUserId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", studentSubject.SubjectId);
            return View(studentSubject);
        }

        // POST: StudentSubjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateStudentGradesViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viewmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Grades");
            }
            return View(viewmodel);
        }

        // GET: StudentSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubjectGrade studentSubject = db.StudentSubjectGrades.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            return View(studentSubject);
        }

        // POST: StudentSubjects/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            StudentSubjectGrade studentSubject = db.StudentSubjectGrades.Find(id);
            db.StudentSubjectGrades.Remove(studentSubject);
            db.SaveChanges();
            return RedirectToAction("Grades");
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
