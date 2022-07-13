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
    public class OrgClassPeriodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: OrgClassPeriods/Timetable
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult ClassTimetables(int? classid)  
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var cid = db.OrgClassPeriods
                        .Where(x => x.ClassId == classid)
                        .Where(x => x.OrgId == i).FirstOrDefault();

                return View(cid);                       
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new HttpStatusCodeResult(204);
        }


        public JsonResult AutoCompleteClasses(string prefix)
        {
            var subjectlist = (from sb in db.Subjects
                              where sb.SubjectName.StartsWith(prefix)
                              select new
                              {
                                  label = sb.SubjectName,
                                  Val = sb.SubjectId
                              }).ToList();
            return Json(subjectlist);
        }


        // POST: OrgClassPeriods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrgClassPeriod orgClassPeriod)
        {
            if (ModelState.IsValid)
            {
                db.OrgClassPeriods.Add(orgClassPeriod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrgSchDayId = new SelectList(db.OrgSchDays, "OrgSchDayId", "Day", orgClassPeriod.OrgSchDayId);
            return View(orgClassPeriod);
        }




        // One time use method to add org period to existing classes -
        // Method should be adjusted after release of sprint 07 
        // For creating orgperiods for newly added class / orgs
        [ChildActionOnly]
        public ActionResult PopulateOrgPeriods()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);


                ViewBag.ClassId = new SelectList(db.Classes
                .Where(x => x.OrgId == i)
                .OrderBy(w => w.ClassRefNumb)
                .ToList(), "ClassId", "ClassName");
                return PartialView("~/Views/Shared/PartialViewsForms/_PopulateOrgPeriods.cshtml");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new HttpStatusCodeResult(204);
        }


        // One time use method to add org period to existing classes -
        // Method should be adjusted after release of sprint 07 
        // For creating orgperiods for newly added class / orgs
        public ActionResult ProcessOrgPeriods(int? classid)   
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                // Locate list of OrgSchDays
                var days = db.OrgSchDays.Select(x => x.OrgSchDayId).ToList(); 
                var listdays = new List<int>(days);

                foreach(var day in days)
                {
                    var currentclass = db.Classes.Where(x => x.OrgId == i).Where(x => x.ClassId == classid).FirstOrDefault();

                    var orgclassperiod = new OrgClassPeriod()
                    {
                        ClassId = classid,
                        ClassRef = currentclass.ClassRefNumb, 
                        OrgId = i,
                        Period_1 = null,
                        Period_2 = null,
                        Period_3 = null,
                        Period_4 = null,
                        Period_5 = null,
                        Period_6 = null,
                        Period_7 = null,
                        Period_8 = null,
                        OrgSchDayId = day,
                        Updater_Id = 0,
                        Last_updated_date = DateTime.Now,
                    };
                    db.OrgClassPeriods.Add(orgclassperiod);
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new HttpStatusCodeResult(204);
        }




        //[ChildActionOnly]
        public ActionResult AddClassTimeTableSlot(int id)
        {
            try
            {
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);

                // Get all the subjects from the database
                var ssg = db.OrgClassPeriods
                    .Where(x => x.ClassId == id)
                    .Include(x => x.OrgSchDay)
                    .Include(x => x.Subject)
                    .ToList();

                var cls = db.Classes.Find(id);
                var day = new OrgSchDay();
                var subject = new Subject();

                
                var subjects = db.Subjects
                    .Where(x => x.ClassId == id)
                    .ToList();

                // Initialize the view model
                var updatetimetableviewmodel = new AddClassTimetableSlotViewModel
                {

                    OrgClassPeriod = ssg.Select(x => new OrgClassPeriod
                    {
                        OrgClassPeriodId = x.OrgClassPeriodId,
                        OrgId = x.OrgId,
                        OrgSchDayId = x.OrgSchDayId,
                        OrgSchDay = x.OrgSchDay,
                        ClassRef = x.ClassRef,
                        ClassId = x.ClassId,
                        SubjectId = x.SubjectId,
                        Period_1 = x.Period_1,
                        Period_2 = x.Period_2,
                        Period_3 = x.Period_3,
                        Period_4 = x.Period_4,
                        Period_5 = x.Period_5,
                        Period_6 = x.Period_6,
                        Period_7 = x.Period_7,
                        Period_8 = x.Period_8,
                        Updater_Id = x.Updater_Id,
                        Last_updated_date = x.Last_updated_date,
                    }).ToList(),

                    @Class = cls,
                    Subjects = subjects,
                };
                //ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", subject.SubjectId );
                ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectId", subject.SubjectId);
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
                return PartialView("~/Views/Shared/PartialViewsForms/_AddClassTimetableSlot.cshtml", updatetimetableviewmodel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        // Save TimeTable Slot
        public ActionResult UpdateClassTimeTableSlot(AddClassTimetableSlotViewModel viewmodel)
        {
            try
            {
                foreach (var tt in viewmodel.OrgClassPeriod)
                {
                    // Locate time table record 
                    var periodid = db.OrgClassPeriods.AsNoTracking()
                        .Where(x => x.OrgClassPeriodId == tt.OrgClassPeriodId).FirstOrDefault(); 

                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);

                    var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                    var updatett = new OrgClassPeriod
                    {
                        OrgClassPeriodId = tt.OrgClassPeriodId,
                        ClassId = tt.ClassId,
                        ClassRef = tt.ClassRef,
                        OrgId = tt.OrgId,
                        Period_1 = tt.Period_1,
                        Period_2 = tt.Period_2, 
                        Period_3 = tt.Period_3,
                        Period_4 = tt.Period_4,
                        Period_5 = tt.Period_5,
                        Period_6 = tt.Period_6,
                        Period_7 = tt.Period_7,
                        Period_8 = tt.Period_8,
                        OrgSchDayId = tt.OrgSchDayId,
                        SubjectId = tt.SubjectId,
                        Updater_Id = RegisteredUserId,
                        Last_updated_date = DateTime.Now
                    };

                    periodid = updatett;
                    db.Entry(periodid).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return new HttpStatusCodeResult(204);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        //Display students Grades
        public ActionResult DisplayClassTimeTable(int? id) 
        {
            try
            {
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);

                // Get all the subjects from the database
                var tt = db.OrgClassPeriods
                    .Where(x => x.ClassId == id)
                    .Where(x => x.OrgId == i)
                    .Include(x => x.Subject)
                    .Include(x => x.OrgSchDay)
                    .ToList();

                var classid = db.Classes.Find(id);

                // Get all the subjects linked to class
                var subjects = db.Subjects
                    .Where(x => x.ClassId == id)
                    .Where(x => x.SubjectOrgId == i)
                    .ToList();

                // Initialize the view model
                var displayttviewmodel = new DisplayClassTimetableViewModel
                {
                    Class = classid,

                    OrgClassPeriod = tt.Select(x => new OrgClassPeriod()
                    {
                        OrgClassPeriodId = x.OrgClassPeriodId,
                        ClassId = x.ClassId,
                        ClassRef = x.ClassRef,
                        OrgId = x.OrgId,
                        Period_1 = x.Period_1,
                        Period_2 = x.Period_2,
                        Period_3 = x.Period_3,
                        Period_4 = x.Period_4,
                        Period_5 = x.Period_5,
                        Period_6 = x.Period_6,
                        Period_7 = x.Period_7,
                        Period_8 = x.Period_8,
                        OrgSchDayId = x.OrgSchDayId,
                        Updater_Id = x.Updater_Id,
                        OrgSchDay = x.OrgSchDay,
                        Last_updated_date = x.Last_updated_date,
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
                        Subject_Min_Passmark = x.Subject_Min_Passmark,
                        Created_date = x.Created_date,
                        Creator_Id = x.Creator_Id
                    }).ToList(),

                };
                return PartialView("~/Views/Shared/DisplayViews/_DisplayClassTimeTable.cshtml", displayttviewmodel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        [HttpPost]
        public JsonResult ListOfSubjects(string Prefix) 
        {
            List<Subject> ObjSubject = new List<Subject>();

            //Searching records from list using LINQ query  
            var SubjectName = (from N in ObjSubject
                               where N.SubjectName.StartsWith(Prefix)
                        select new { N.SubjectName });

            return Json(SubjectName, JsonRequestBehavior.AllowGet);

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
