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
    [RoutePrefix("")]
    public class SubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subjects
        [Route("AllSubjects")]
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
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
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
                .Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4) || (j.NurserySchoolUserRoleId == 3))
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

                    var edtsubject = db.Subjects
                        .Include(x => x.Class)
                        .Where(x => x.SubjectId == Id)
                        .FirstOrDefault();

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
                        Subject_Min_Passmark = edtsubject.Subject_Min_Passmark,
                        SubjectOrgId = edtsubject.SubjectOrgId,
                        Created_date = edtsubject.Created_date,
                        Creator_Id = edtsubject.Creator_Id,

                    };

                    ViewBag.ClassTeacherId = new SelectList(db.RegisteredUserOrganisations
                        .Where(x => x.OrgId == i)
                        .Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4) || (j.NurserySchoolUserRoleId == 3)), "RegisteredUserId", "FullName", edtsubject1.ClassTeacherId);

                    ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.OrgId == i), "ClassId", "ClassName", edtsubject.ClassId);

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
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                if (ModelState.IsValid)
                {
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
                    .Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4) || (j.NurserySchoolUserRoleId == 3)), "RegisteredUserId", "FullName");
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
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
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




        public ActionResult AssignSubjectTeacher(int Id) 
        {
            try
            {
                if (Id != 0)
                {
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);

                    var edtsubject = db.Subjects
                        .Include(x => x.Class)
                        .Where(x => x.SubjectId == Id)
                        .FirstOrDefault();

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
                        Subject_Min_Passmark = edtsubject.Subject_Min_Passmark,
                        SubjectOrgId = edtsubject.SubjectOrgId,
                        Created_date = edtsubject.Created_date,
                        Creator_Id = edtsubject.Creator_Id,

                    };
                    ViewBag.ClassTeacherId = new SelectList(db.RegisteredUserOrganisations
                        .Where(x => x.OrgId == i)
                        .Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4) || (j.NurserySchoolUserRoleId == 3)), "RegisteredUserId", "FullName", edtsubject1.ClassTeacherId);

                    ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.OrgId == i), "ClassId", "ClassName", edtsubject.ClassId);

                    return PartialView("~/Views/Shared/PartialViewsForms/_AssignSubjectTeacher.cshtml", edtsubject1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_AssignSubjectTeacher.cshtml");
        }




        // POST: Subjects/AssignTeacher/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignSubjectTeacher(Subject subject) 
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                if (ModelState.IsValid)
                {
                    if(subject.ClassTeacherId == null)
                    {
                        db.Entry(subject).State = EntityState.Modified;
                        db.SaveChanges();

                        // Log Event 
                        var orgeventlog = new Org_Events_Log()
                        {
                            Org_Event_SubjectId = subject.ClassTeacherId.ToString(),
                            Org_Event_SubjectName = "From " + subject.SubjectName + " " + "["+subject.SubjectId+"]",
                            Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                            Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                            Org_Event_Time = DateTime.Now,
                            OrgId = Session["OrgId"].ToString(),
                            Org_Events_Types = Org_Events_Types.Unassigned_Teacher
                        };
                        db.Org_Events_Logs.Add(orgeventlog);
                        db.SaveChanges();

                        return RedirectToAction("Index");

                    }
                    else
                    {
                        var taughtby = db.RegisteredUsers.Where(x => x.RegisteredUserId == subject.ClassTeacherId).Select(x => x.FullName).FirstOrDefault();
                        subject.TaughtBy = taughtby;

                        db.Entry(subject).State = EntityState.Modified;
                        db.SaveChanges();

                        var orgid = subject.SubjectOrgId;
                        var subid = subject.SubjectId;
                        var classid = subject.ClassId;

                        // Log Event
                        var orgeventlog = new Org_Events_Log()
                        {
                            Org_Event_SubjectId = subject.ClassTeacherId.ToString(),
                            Org_Event_SubjectName = taughtby + " To " + subject.SubjectName + " " + "["+subject.SubjectId+"]",
                            Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                            Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                            Org_Event_Time = DateTime.Now,
                            OrgId = Session["OrgId"].ToString(),
                            Org_Events_Types = Org_Events_Types.Assigned_Teacher
                        };
                        db.Org_Events_Logs.Add(orgeventlog);
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
        //        return RedirectToRoute(new { controller = "Access",  action = "Signin", });
        //    }

        //    Subject subject = db.Subjects.Find(id);
        //    db.Subjects.Remove(subject);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
