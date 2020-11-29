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

            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }

            if ((int)Session["OrgId"] != 23)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);


                var subjects = db.Subjects
                    .Include(s => s.Class);

                    

                return View(subjects.ToList());

            }

            else

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        // GET: Subjects/Details/5
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
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }


        [ChildActionOnly]
        public ActionResult AddSubject()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            ViewBag.ClassTeacherId = new SelectList(db.RegisteredUsers.Where(x => x.SelectedOrg == i).Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4)), "RegisteredUserId", "FullName");

            ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.OrgId == i).OrderBy(w => w.ClassRefNumb).ToList(), "ClassId", "ClassName");

            return PartialView("~/Views/Shared/PartialViewsForms/_AddSubject.cshtml");

        }





        // GET: Subjects/Create
        public ActionResult Create()
        {

            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }


            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            ViewBag.ClassTeacherId = new SelectList(db.RegisteredUsers.Where(x => x.SelectedOrg == i).Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4)), "RegisteredUserId", "FullName");
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subject subject)
        {

            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }


            if (ModelState.IsValid)
            {
                var taughtby = db.RegisteredUsers.Where(x => x.RegisteredUserId == subject.ClassTeacherId).Select(x => x.FullName).FirstOrDefault();

                subject.TaughtBy = taughtby;


                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            ViewBag.ClassTeacherId = new SelectList(db.RegisteredUsers.Where(x => x.SelectedOrg == i).Where(j => (j.SecondarySchoolUserRoleId == 3) || (j.PrimarySchoolUserRoleId == 4)), "RegisteredUserId", "FullName");



            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", subject.ClassId);
            return View(subject);
        }

        // GET: Subjects/Edit/5
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
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }


         
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", subject.ClassId);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectId,SubjectName,ClassId")] Subject subject)
        {

            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }


            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", subject.ClassId);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {

            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }



            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
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
