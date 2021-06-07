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
    public class StudentGuardiansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentGuardians
        public ActionResult Index()
        {
            if (Request.Browser.IsMobileDevice == true)
            {
                return RedirectToAction("WrongDevice", "Orgs");

            }
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if (Request.Browser.IsMobileDevice == true)
            {
                return RedirectToAction("WrongDevice", "Orgs");

            }


            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var studentGuardians = db.StudentGuardians
                .Where(e => e.OrgId == i) 
                .Include(s => s.RegisteredUser)
                .Include(s => s.Title)
                .Include(s => s.Relationship);               
            return View(studentGuardians.ToList());
        }




        public ActionResult MyGuardians(int id)
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var Myguardians = db.StudentGuardians
                .Where(x => x.StudentId == id && x.OrgId == i)
                .Include(x => x.Title)
                .Include(x => x.Relationship)
                .ToList();

            return PartialView("_MyGuardians", Myguardians);
        }




        [ChildActionOnly]
        public ActionResult MyChildCount()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

            var mychildcount = db.StudentGuardians
                .Where(x => x.OrgId == i)
                .Where(j => j.RegisteredUserId == RegisteredUserId)
                .ToList();
            return PartialView("_MyChildCount", mychildcount);
        }


        public ActionResult MyChildList()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

            var mychildlist = db.StudentGuardians
                .Where(x => x.OrgId == i)
                .Where(j => j.RegisteredUserId == RegisteredUserId)
                .ToList();
            return PartialView("_MyChildList", mychildlist);
        }


        public ActionResult EditGuardian(int Id)
        {
            if (Id != 0)
            {
                var guardian = db.StudentGuardians
                    .Where(x => x.StudentGuardianId == Id)
                    .FirstOrDefault();
                var studentguardian = new StudentGuardian
                {
                    StudentGuardianId = guardian.StudentGuardianId,
                    RegisteredUserId = guardian.RegisteredUserId,
                    GuardianFirstName = guardian.GuardianFirstName,
                    GuardianLastName = guardian.GuardianLastName,
                    GuardianFullName = guardian.GuardianFullName,
                    GuardianEmailAddress = guardian.GuardianEmailAddress,
                    DateAdded = guardian.DateAdded,
                    StudentId = guardian.StudentId,
                    StudentFullName = guardian.StudentFullName,
                    OrgId = guardian.OrgId,
                    TitleId = guardian.TitleId,
                    RelationshipId = guardian.RelationshipId,
                    Telephone = guardian.Telephone          
                };

                ViewBag.RelationshipId = new SelectList(db.Relationships, "RelationshipId", "RelationshipName", guardian.RelationshipId);
                ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", guardian.TitleId);

                return PartialView("~/Views/Shared/PartialViewsForms/_EditGuardian.cshtml", studentguardian);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditGuardian.cshtml");
        }



        // GET: StudentGuardians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGuardian studentGuardian = db.StudentGuardians.Find(id);
            if (studentGuardian == null)
            {
                return HttpNotFound();
            }
            return View(studentGuardian);
        }

        // GET: StudentGuardians/Create
        public ActionResult Create()
        {
            ViewBag.RegisteredUserId = new SelectList(db.RegisteredUsers, "RegisteredUserId", "FirstName");
            return View();
        }

        // POST: StudentGuardians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentGuardian studentGuardian)
        {
            if (ModelState.IsValid)
            {



                db.StudentGuardians.Add(studentGuardian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentGuardian);
        }


        // POST: StudentGuardians/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentGuardian studentGuardian)
        {
            if (ModelState.IsValid)
            {
                studentGuardian.GuardianFullName = studentGuardian.GuardianFirstName + studentGuardian.GuardianLastName;



                db.Entry(studentGuardian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentGuardian);
        }

        // GET: StudentGuardians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGuardian studentGuardian = db.StudentGuardians.Find(id);
            if (studentGuardian == null)
            {
                return HttpNotFound();
            }
            return View(studentGuardian);
        }

        // POST: StudentGuardians/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            // CHECK IF GUARDIAN IS LINKED TO ANY OTHER STUDENT IN THIS ORG - IF NO - DELETE FROM SG /REGUSERORG/ REGUSER - TABLE AND LOG EVENT. CHECK USER IS DELETED FROM GRP LINKED TO CLASS.

            // GET GUARDIANS REGUSERID
            var gd_id = db.StudentGuardians.Where(x => x.StudentGuardianId == id).Select(x => x.RegisteredUserId).FirstOrDefault();
            // COUNT HOW MANY STUDENT GUARDIANS IS LINKED TO IN ORG
            var linked_stud = db.StudentGuardians.Where(x => x.RegisteredUserId == gd_id).Where(x => x.OrgId == i ).Count();
            // IF COUNT OF LINKED STUDENT IS 1 - MEANS GUARDIAN IS ONLY LINKED TO THE STUDENT - WE GO IN THIS CONDITON AND FULLY DELETE GUARDIAN FROM THE SYSTEM.
            if (linked_stud == 1)
            {
            // GET GUARDIANS FULLNAME. 
                string guardfullname1 = db.RegisteredUsers.Where(x => x.RegisteredUserId == gd_id).Select(x => x.FullName).FirstOrDefault();

            // LOG EVENT 
                var orgeventlog = new Org_Events_Log()
                {
                    Org_Event_TypeId = 5,
                    Org_Event_Name = "Deregistered guardian",
                    Org_Event_SubjectId = gd_id.ToString(),
                    Org_Event_SubjectName = guardfullname1,
                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                    Org_Event_Time = DateTime.Now,
                    OrgId = Session["OrgId"].ToString()
                };
                db.Org_Events_Logs.Add(orgeventlog);
                db.SaveChanges();


            // LOG EVENT 

                RegisteredUser remv_g = db.RegisteredUsers.Find(gd_id);
                db.RegisteredUsers.Remove(remv_g);
                db.SaveChanges();

           
            };
       
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
