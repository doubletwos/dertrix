using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zeus.Models;

namespace Zeus.Controllers
{
    public class StudentSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentSubjects
        public ActionResult Index()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }
            var studentSubject = db.StudentSubject.Include(s => s.RegisteredUser).Include(s => s.Subject);
            return View(studentSubject.ToList());
        }

        // GET: StudentSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
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
                return RedirectToAction("Index", "Access");
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


            return PartialView("_StudentUpdateSubject");
        }




        [HttpPost]
        public ActionResult StudentUpdateSubject(string searchname, int classid)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }
            var orgid = Convert.ToInt32(Session["OrgId"]);
            var classref = db.Classes.Where(x => x.ClassRefNumb == classid).Where(o => o.OrgId == orgid).Select(p => p.ClassId).FirstOrDefault();
            var subjectid = db.Subjects.Where(s => s.ClassId == classref).Select(c => c.SubjectId).FirstOrDefault();
            //var subjectname = db.Subjects.Where(s => s.ClassId == classref).Where(x => x.SubjectId == subjectid).Select(c => c.SubjectName).FirstOrDefault();




            //student list
            var students = db.RegisteredUsers.Where(x => x.ClassId == classref).Where(p => p.SelectedOrg == orgid).Where(s => s.StudentRegFormId != null).Select(k => k.RegisteredUserId).ToList();
            var studentid = new List<int>(students);


            //subject list
            var subjects = db.Subjects.Where(x => x.ClassId == classref).Select(c => c.SubjectId).ToList();
            var subject = new List<int>(subjects);


            foreach (var stu in students)
            {

                foreach (var sb in subjects)
                {
                    var subjectexistcheck = db.StudentSubject.Where(x => x.RegisteredUserId == stu && x.SubjectId == sb).FirstOrDefault();

                    if(subjectexistcheck == null)
                    {
                        var subjectname = db.Subjects.Where(s => s.ClassId == classref).Where(x => x.SubjectId == sb).Select(c => c.SubjectName).FirstOrDefault();


                        var studentsubjects = new StudentSubject()
                        {
                            RegisteredUserId = stu,
                            ClassId = classref,
                            SubjectId = sb,
                            SubjectName = subjectname
                        };
                        db.StudentSubject.Add(studentsubjects);
                        db.SaveChanges();

                    }
                    

                }
                
            }


            return RedirectToAction("Index", "StudentSubjects");
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentSubjectId,RegisteredUserId,SubjectId")] StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
