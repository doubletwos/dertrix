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
    public class StudentSubjectsController : Controller
    {
      

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentSubjects/Grades
        public ActionResult Grades()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);


            var studentSubject = db.StudentSubject.Where(p => p.OrgId == i).Include(s => s.RegisteredUser).Include(s => s.Subject);
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

     
            var yourchild = db.StudentSubject.Where(p => p.OrgId == i).Where(x => x.RegisteredUserId == id);
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
            //var j = db.Classes.Where(s => s.OrgId == id && ij == s.ClassRefNumb).Select(g => g.ClassId).FirstOrDefault();


            string subjectid = id.ToString(); 

            // returns students of org if class is selected
            if (string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid) && (!string.IsNullOrWhiteSpace(subjectid)))
            {
                return View(db.StudentSubject.Where(p => p.SubjectId == id && p.OrgId == i).Include(s => s.Subject).Include(r => r.RegisteredUser).ToList());
            }

            // returns students of org if fullname is provided
            if (!string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid))
            {
                return View(db.StudentSubject.Where(n => n.StudentFullName == searchname).Include(s => s.Subject).Include(r => r.RegisteredUser).ToList());

            }

            // returns students of org if studentid is provided
            if (string.IsNullOrWhiteSpace(searchname) && !string.IsNullOrWhiteSpace(searchid))
            {
                int reguserid = Convert.ToInt32(searchid);
                return View(db.StudentSubject.Where(n => n.RegisteredUserId == reguserid).Include(s => s.Subject).Include(r => r.RegisteredUser).ToList());

            }

            //var mysbujects = db.Subjects.Where(p => p.ClassTeacherId == RegisteredUserId && p.SubjectId == id).Select()

            var studentSubject = db.StudentSubject.Where(p => p.SubjectId == id).Include(s => s.RegisteredUser).Include(s => s.Subject);
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
            StudentSubject studentSubject = db.StudentSubject.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            return View(studentSubject);
        }

        // GET: StudentSubjects/Create
        public ActionResult Create() {

            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

    


            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName");
            //ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
            //ViewBag.Subjects = new MultiSelectList(db.Subjects, "SubjectId", "SubjectName");
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
        public ActionResult StudentUpdateSubject(string searchname, int classid)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            var orgid = Convert.ToInt32(Session["OrgId"]);



            //classref list
            var classreflist = db.Classes.Where(x => x.ClassRefNumb == classid).Where(o => o.OrgId == orgid).Select(p => p.ClassId).ToList();
            var listofclasses = new List<int>(classreflist);
            var classref = db.Classes.Where(x => x.ClassRefNumb == classid).Where(o => o.OrgId == orgid).Select(p => p.ClassRefNumb).FirstOrDefault();
        



            foreach (var cr in classreflist)
            {

                //subject ids
                var subjectid = db.Subjects.Where(s => s.ClassId == cr).Select(c => c.SubjectId).FirstOrDefault();
                

                //student list
                var students = db.RegisteredUsers.Where(x => x.ClassId == cr).Where(p => p.SelectedOrg == orgid).Where(s => s.StudentRegFormId != null).Select(k => k.RegisteredUserId).ToList();
                var studentid = new List<int>(students);


                //subject list
                var subjects = db.Subjects.Where(x => x.ClassId == cr).Select(c => c.SubjectId).ToList();
                var subject = new List<int>(subjects);


        

                foreach (var stu in students)
                {

                    foreach (var sb in subjects)
                    {
                        var subjectexistcheck = db.StudentSubject.Where(x => x.RegisteredUserId == stu && x.SubjectId == sb).FirstOrDefault();

                        if (subjectexistcheck == null)
                        {
                            var subjectname = db.Subjects.Where(s => s.ClassId == cr).Where(x => x.SubjectId == sb).Select(c => c.SubjectName).FirstOrDefault();
                            var fullname = db.RegisteredUsers.Where(s => s.RegisteredUserId == stu).Select(f => f.FullName).FirstOrDefault();
                        

                            var studentsubjects = new StudentSubject()
                            {
                                RegisteredUserId = stu,
                                ClassId = cr,
                                ClassRef = classref,
                                OrgId = orgid,
                                SubjectId = sb,
                                SubjectName = subjectname,
                                StudentFullName = fullname,
                                FirstTermStudentGrade = 00.0m,
                                SecondTermStudentGrade = 00.0m,
                                ThirdTermStudentGrade = 00.0m
                            };
                            db.StudentSubject.Add(studentsubjects);
                            db.SaveChanges();

                        }


                    }

                }

            }


            return RedirectToAction("Index", "StudentSubjects");
        }


        public ActionResult MyClass(int? id, string searchname, string searchid)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            var OrgId = (int)Session["OrgId"];


            // returns students of org if fullname is provided
            if (!string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid))
            {
                return View(db.StudentSubject.Where(n => n.StudentFullName == searchname).ToList());

            }
            // returns students of org if studentid is provided
            if (string.IsNullOrWhiteSpace(searchname) && !string.IsNullOrWhiteSpace(searchid))
            {
                int reguserid = Convert.ToInt32(searchid);
                return View(db.StudentSubject.Where(n => n.RegisteredUserId == reguserid).ToList());

            }


            var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
            var myclasses = db.Classes.Where(x => x.ClassTeacherId == RegisteredUserId).Select(x => x.ClassId).FirstOrDefault();
            var mystudents = db.StudentSubject.Where(x => x.ClassId == id && x.ClassId == myclasses)
                .Include(s => s.Subject).Include(r => r.RegisteredUser).ToList();

            return View(mystudents);

        }


        public JsonResult AutoCompleteStudentFullname(string prefix, int? id )
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);



            var studentsfullname = (from stu in db.StudentSubject.Where(j => j.OrgId == i)
                                    where stu.StudentFullName.StartsWith(prefix)
                                    select new
                                    {
                                        label = stu.StudentFullName,
                                        Val = stu.RegisteredUserId
                                    }).ToList();
            return Json(studentsfullname);
        }



        public ActionResult UpdateStudentGrade(int Id)
        {
            if (Id != 0)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var stud1 = db.StudentSubject
                    .Include(s => s.Subject)
                    .Include(r => r.RegisteredUser)
                    .Where(x => x.StudentSubjectId == Id)
                    .FirstOrDefault();

                var stud = new StudentSubject
                {
                    StudentSubjectId = stud1.StudentSubjectId,
                    RegisteredUserId = stud1.RegisteredUserId,
                    SubjectId = stud1.SubjectId,
                    SubjectName = stud1.SubjectName,
                    ClassId = stud1.ClassId,
                    StudentFullName = stud1.StudentFullName,
                    FirstTermStudentGrade = stud1.FirstTermStudentGrade,
                    SecondTermStudentGrade = stud1.SecondTermStudentGrade,
                    ThirdTermStudentGrade = stud1.ThirdTermStudentGrade
                                   
              };
                return PartialView("~/Views/Shared/PartialViewsForms/_UpdateStudentGrade.cshtml", stud);
            }
            return PartialView("_UpdateStudentGrade");
        }








        // POST: StudentSubjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {

                db.StudentSubject.Add(studentSubject);
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
            StudentSubject studentSubject = db.StudentSubject.Find(id);
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
        public ActionResult Edit(StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Grades");
            }
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName", studentSubject.RegisteredUserId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", studentSubject.SubjectId);
            return View(studentSubject);
        }

        // GET: StudentSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubject studentSubject = db.StudentSubject.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            return View(studentSubject);
        }

        // POST: StudentSubjects/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentSubject studentSubject = db.StudentSubject.Find(id);
            db.StudentSubject.Remove(studentSubject);
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
