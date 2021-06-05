using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Dertrix.Models;
namespace Dertrix.Controllers
{
    public class RegisteredUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegisteredUsers/Index
        public ActionResult Index(int? id)
        {
        

            /* Redirect back to Log in Page if session == null*/
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            /* Populate user list for non superusers if session is not null*/
            if (Session["OrgId"] != null)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                id = i;
            }
            if ((int)Session["OrgId"] == 23)
            {
                return View(db.RegisteredUsers.Where(j => j.SelectedOrg == id).Include(t => t.RegisteredUserType).ToList());
            }
            else
            {
                /*Changes will have to be made to the code below of Parents are given roles*/
                return View(db.RegisteredUsers.Where(j => j.SelectedOrg == id).Where(f => f.PrimarySchoolUserRoleId != null || f.SecondarySchoolUserRoleId != null).Include(t => t.RegisteredUserType).ToList());
            }
        }

        [ChildActionOnly]
        public ActionResult Regs()
        {
            return PartialView("_Secure");
        }

        [ChildActionOnly]
        public ActionResult RegisterUser()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            ViewBag.SelectedOrgList = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles, "SecondarySchoolUserRoleId", "RoleName");
            ViewBag.ClassId = new SelectList(db.Classes.Where(o => o.OrgId == i), "ClassId", "ClassName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            return PartialView("~/Views/Shared/PartialViewsForms/_RegisterUser.cshtml");
        }

        [ChildActionOnly]
        public ActionResult Nav()
        {
            return PartialView("_Nav");
        }

        [ChildActionOnly]
        public ActionResult AddStudent()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.OrgId == i).OrderBy(w => w.ClassRefNumb).ToList(), "ClassId", "ClassName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles, "SecondarySchoolUserRoleId", "RoleName");
            ViewBag.ReligionId = new SelectList(db.Religions, "ReligionId", "ReligionName");
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName");
            ViewBag.StudentRegFormId = new SelectList(db.StudentRegForm, "StudentRegFormId", "Name");
            ViewBag.TribeId = new SelectList(db.Tribes, "TribeId", "TribeName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
            return PartialView("~/Views/Shared/PartialViewsForms/_AddStudent.cshtml");
        }

        [ChildActionOnly]
        public ActionResult AddStaff()
        {
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName");
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles.Where(x => x.PrimarySchoolUserRoleID != 5), "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles.Where(x => x.SecondarySchoolUserRoleId != 5), "SecondarySchoolUserRoleId", "RoleName");

            return PartialView("~/Views/Shared/PartialViewsForms/_AddStaff.cshtml");
        }

        [ChildActionOnly]
        public ActionResult AddGuardian()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles, "SecondarySchoolUserRoleId", "RoleName");
            return PartialView("~/Views/Shared/PartialViewsForms/_AddGuardian.cshtml");
        }

        [ChildActionOnly]
        public ActionResult AddSysAdmin()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            ViewBag.SelectedOrgList = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            return PartialView("~/Views/Shared/PartialViewsForms/_AddSysAdmin.cshtml");
        }

        [ChildActionOnly]
        public ActionResult StudentCount()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var studentCount = db.RegisteredUsers
                .Where(x => x.SelectedOrg == i)
                .Where(j => j.StudentRegFormId != null)
                .Where(c => c.ClassId != null)
                .ToList();
            return PartialView("_StudentCount", studentCount);
        }

        public ActionResult StudentDetails(int Id)
        {
            var stud = db.RegisteredUsers
                .Include(r => r.Religion)
                .Include(c => c.Class)
                .Include(g => g.Gender)
                .Include(t => t.Tribe)
                .Where(x => x.RegisteredUserId == Id);
            ViewBag.RegisteredUser = stud;
            return PartialView("_StudentDetails");
        }


        public ActionResult MyRegProfile(int id)
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var myprofile = db.RegisteredUsers
                .Where(x => x.RegisteredUserId == id)
                .Include(x => x.Religion)
                .Include(x => x.Tribe)
                .Include(x => x.Class)
                .FirstOrDefault();

            return PartialView("_MyRegProfile", myprofile);
        }




        public ActionResult StaffDetails(int? Id)
        {

            var stud = db.RegisteredUsers
            .Where(x => x.RegisteredUserId == Id)
            .Include(x => x.Title)
            .Include(x => x.PrimarySchoolUserRole)
            .Include(x => x.SecondarySchoolUserRole)


            ;
            ViewBag.RegisteredUser = stud;
            return PartialView("_StaffDetails");
        }

        public ActionResult EditStudent(int Id)
        {
            if (Id != 0)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var stud1 = db.RegisteredUsers
                    .Include(r => r.Religion)
                    .Include(c => c.Class)
                    .Include(g => g.Gender)
                    .Include(t => t.Tribe)
                    .Where(x => x.RegisteredUserId == Id)
                    .FirstOrDefault();
                ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.OrgId == i).OrderBy(w => w.ClassRefNumb).ToList(), "ClassId", "ClassName", stud1.ClassId);
                ViewBag.ReligionId = new SelectList(db.Religions, "ReligionId", "ReligionName", stud1.ReligionId);
                ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName", stud1.GenderId);
                ViewBag.TribeId = new SelectList(db.Tribes, "TribeId", "TribeName", stud1.TribeId);
                var stud = new RegisteredUser
                {
                    RegisteredUserId = stud1.RegisteredUserId,
                    RegisteredUserTypeId = stud1.RegisteredUserTypeId,
                    FirstName = stud1.FirstName,
                    LastName = stud1.LastName,
                    ClassId = stud1.ClassId,
                    GenderId = stud1.Gender.GenderId,
                    ReligionId = stud1.Religion.ReligionId,
                    StudentRegFormId = stud1.StudentRegFormId,
                    TribeId = stud1.TribeId,
                    EnrolmentDate = stud1.EnrolmentDate,
                    DateOfBirth = stud1.DateOfBirth,
                    FullName = stud1.FullName,
                    CreatedBy = stud1.CreatedBy,
                    RegUserOrgBrand = stud1.RegUserOrgBrand,
                    ClassRef = stud1.ClassRef
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditStudent.cshtml", stud);
            }
            return PartialView("_EditStudent");
        }



        public ActionResult LinkGuardianStudent(int Id)
        {
            if (Id != 0)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var stud1 = db.RegisteredUsers
                    .Include(c => c.Class)
                    .Where(x => x.RegisteredUserId == Id)
                    .FirstOrDefault();
                ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
                ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles, "SecondarySchoolUserRoleId", "RoleName");
                ViewBag.RelationshipId = new SelectList(db.Relationships, "RelationshipId", "RelationshipName");
                ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName");


                var stud = new RegisteredUser
                {
                    RegisteredUserId = stud1.RegisteredUserId,
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_LinkGuardianStudent.cshtml", stud);
            }
            return PartialView("_LinkGuardianStudent");
        }

        public ActionResult EditStaff(int Id)
        {
            if (Id != 0)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var stud1 = db.RegisteredUsers
                    .Where(x => x.RegisteredUserId == Id)
                    .FirstOrDefault();
                ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", stud1.TitleId);
                ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", stud1.ClassId);
                var stud = new RegisteredUser
                {
                    RegisteredUserId = stud1.RegisteredUserId,
                    RegisteredUserTypeId = stud1.RegisteredUserTypeId,
                    TitleId = stud1.TitleId,
                    FirstName = stud1.FirstName,
                    LastName = stud1.LastName,
                    Email = stud1.Email,
                    Password = stud1.Password,
                    ConfirmPassword = stud1.ConfirmPassword,
                    Telephone = stud1.Telephone,
                    SelectedOrg = stud1.SelectedOrg,
                    ClassId = stud1.ClassId,
                    EnrolmentDate = stud1.EnrolmentDate,
                    CreatedBy = stud1.CreatedBy,
                    PrimarySchoolUserRoleId = stud1.PrimarySchoolUserRoleId,
                    SecondarySchoolUserRoleId = stud1.SecondarySchoolUserRoleId,
                    FullName = stud1.FullName,
                    RegUserOrgBrand = stud1.RegUserOrgBrand
                    
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditStaff.cshtml", stud);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditStaff.cshtml");
        }

        // GET: RegisteredUsers/AllStudents/
        public ActionResult AllStudents(int? id, int? ij, string searchname, string searchid)
        {
            if (Request.Browser.IsMobileDevice == true)
            {
                return RedirectToAction("WrongDevice", "Orgs");

            }
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            var orgid  = (int)Session["OrgId"];
            // returns students of org if fullname is provided
            if (!string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid))
            {
                return View(db.RegisteredUsers.Where(n => n.FullName == searchname).Include(s => s.Class).ToList());

            }
            // returns students of org if studentid is provided
            if (string.IsNullOrWhiteSpace(searchname) && !string.IsNullOrWhiteSpace(searchid))
            {
                int reguserid = Convert.ToInt32(searchid);
                return View(db.RegisteredUsers.Where(n => n.RegisteredUserId == reguserid).Include(s => s.Class).ToList());

            }
            var students = db.RegisteredUsers
           .Where(x => x.StudentRegFormId != null && x.SelectedOrg == orgid)
           .ToList();
            return View(students);
        }


        // GET: RegisteredUsers/Staffs/
        public ActionResult Staffs(int? id)
        {
            if (Request.Browser.IsMobileDevice == true)
            {
                return RedirectToAction("WrongDevice", "Orgs");

            }
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if (Session["OrgId"] != null)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                id = i;
            }
            var staffs = db.RegisteredUserOrganisations
                .Where(j => j.OrgId == id)
                .Where(p => p.PrimarySchoolUserRoleId != null || p.SecondarySchoolUserRoleId != null)
                .Where(p => p.PrimarySchoolUserRoleId != 5)
                .Where(p => p.SecondarySchoolUserRoleId != 5)
                .Include(p => p.Title)
                .Include(p => p.PrimarySchoolUserRole)
                .Include(p => p.SecondarySchoolUserRole)
                .ToList();

            return View(staffs);
        }




        // GET: RegisteredUsers/SysAdmins/
        public ActionResult SysAdmins(int? id) 
        {
            if (Request.Browser.IsMobileDevice == true)
            {
                return RedirectToAction("WrongDevice", "Orgs");
            }
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if (Session["OrgId"] != null)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                id = i;
            }
            var SysAdmins = db.RegisteredUserOrganisations
                .Where(j => j.OrgId == 23)            
                .ToList();

            return View(SysAdmins);
        }



        // GET: RegisteredUsers/ClassStudents/
        public ActionResult ClassStudents(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            var orgid = (int)Session["OrgId"];

            var classref = db.Classes.Where(x => x.ClassId == id).Select(x => x.ClassRefNumb).FirstOrDefault();

            var students = db.RegisteredUsers
                .Where(x => x.StudentRegFormId != null && x.SelectedOrg == orgid && x.ClassRef == classref && x.ClassId == id)
                .ToList();
            return View(students);
        }






        // GET: RegisteredUsers/Staffs/
        public ActionResult Guardians(int? id, string searchname, string searchid)
        {
            /* Redirect back to Log in Page if session == null*/
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if (Session["OrgId"] != null)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                id = i;
            }
           
            if (Session["OrgId"] != null)
            {
                return View(db.RegisteredUsers.Where(j => j.SecondarySchoolUserRoleId == 5 && j.PrimarySchoolUserRoleId == 5).Where(x => x.SelectedOrg == id).Include(g => g.SecondarySchoolUserRole).Include(o => o.PrimarySchoolUserRole).ToList());
                //.Include(t => t.RegisteredUserType).Include(s => s.SecondarySchoolUserRole).Include(s => s.PrimarySchoolUserRole)
                //.ToList());
            }
            return View(db.RegisteredUsers.Where(s => s.RegisteredUserTypeId == 2).Where(p => p.ClassId == id).Include(c => c.Class).ToList());
        }




        // POST: RegisteredUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisteredUser registeredUser)
        {
            /*Accepting all state of model*/
            if (!(ModelState.IsValid) || ModelState.IsValid)
            {
                /************************************************************THIS IS THE BEGINNNG OF CHECKING IF USER BEING ADDED IS AN EXISTING USER ON THE SYSTEM.**********************************************************/


                // CHECKING IF USER USER BEING ADDED ALREADY EXIST ON THE SYSTEM. 
                var checkemail = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.Email).FirstOrDefault();
                // IF EMAIL ADDRESS ALREADY EXISTS IN THE DATABASE - THEN WE GO INTO THE CONDITION BELOW TO PICK THE EXISTING USERS DATA.
                if (checkemail != null)
                {
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);
                    registeredUser.RegisteredUserTypeId = 2;
                    registeredUser.FirstName = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(c => c.FirstName).FirstOrDefault();
                    registeredUser.LastName = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(c => c.LastName).FirstOrDefault();
                    registeredUser.Email = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(c => c.Email).FirstOrDefault();
                    registeredUser.Password = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(c => c.Password).FirstOrDefault();
                    registeredUser.ConfirmPassword = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(c => c.ConfirmPassword).FirstOrDefault();
                    registeredUser.Telephone = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(c => c.Telephone).FirstOrDefault();
                    registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                    registeredUser.FullName = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(c => c.FullName).FirstOrDefault();
                    registeredUser.SelectedOrg = i;
                    var regUserOrgBrand = db.Orgs.Where(x => x.OrgId == i).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j = Convert.ToInt32(regUserOrgBrand);
                    registeredUser.RegUserOrgBrand = j;

 //EXISTING SCHOOL STAFFS    // CHECKING IF USER IS A GUARDIAN - IF THE USER IS NOT A GUARDIAN THEN WE GO INTO THIS CONDITION - (ONLY SCHOOL STAFFS SHOULD GO INTO THIS CONDITION).
                    if (registeredUser.SecondarySchoolUserRoleId != 5 && registeredUser.PrimarySchoolUserRoleId != 5)
                    {
                        registeredUser.RegisteredUserId = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(d => d.RegisteredUserId).FirstOrDefault();
                        // CHECK TO MAKE SURE THE USER DOES NOT ALREADY HAVE AN ACCOUNT AT THIS ORG - IF NO - THEN WE ADD THE USER TO THE REGUSERORG. 
                        var reguserinorg = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Where(x => x.OrgId == i).FirstOrDefault();
                        if (reguserinorg == null)
                        {
                            var regusrid = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.RegisteredUserId).FirstOrDefault();
                            var onetomany = new RegisteredUserOrganisation()
                            {
                                RegisteredUserId = regusrid,
                                OrgId = i,
                                Email = registeredUser.Email,
                                FirstName = registeredUser.FirstName,
                                LastName = registeredUser.LastName,
                                OrgName = db.Orgs.Where(x => x.OrgId == i).Select(x => x.OrgName).FirstOrDefault(),
                                RegUserOrgBrand = registeredUser.RegUserOrgBrand,
                                RegisteredUserTypeId = registeredUser.RegisteredUserTypeId,
                                PrimarySchoolUserRoleId = registeredUser.PrimarySchoolUserRoleId,
                                SecondarySchoolUserRoleId = registeredUser.SecondarySchoolUserRoleId,
                                EnrolmentDate = DateTime.Now,
                                CreatedBy = Session["RegisteredUserId"].ToString(),
                                FullName = registeredUser.FullName,
                                TitleId = registeredUser.TitleId,
                                LastLogOn = null
                            };
                            db.RegisteredUserOrganisations.Add(onetomany);
                            db.SaveChanges();
                            // UPON ADDING A STAFF - LOG THE EVENT - ADDING STAFF IS EVENTTYPEID = 3.
                            var orgeventlog = new Org_Events_Log()
                            {
                                Org_Event_TypeId = 3,
                                Org_Event_Name = "Registered staff",
                                Org_Event_SubjectId = registeredUser.RegisteredUserId.ToString(),
                                Org_Event_SubjectName = registeredUser.FullName,
                                Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                Org_Event_Time = DateTime.Now,
                                OrgId = Session["OrgId"].ToString()
                            };
                            db.Org_Events_Logs.Add(orgeventlog);
                            db.SaveChanges();
                            // THEN EXIT.
                            return RedirectToAction("Staffs", "RegisteredUsers");
                        }
                    }

 //EXISTING GUARDIANS     // CHECKING TO SEE IF THE USER BEING ADDED IS A GUARDIAN - (ONLY GUARDIANS SHOULD GO INTO THIS CONDITION).
                    if (registeredUser.PrimarySchoolUserRoleId != 5 || registeredUser.SecondarySchoolUserRoleId != 5)
                    {
                        // CHECKING TO MAKE SURE THE USER DOES NOT ALREADY HAVE AN ACCOUNT AT THIS ORG. IF NO, THEN ADD THE  USER TO THE REGUSERORG.
                        var reguserinorg1 = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Where(x => x.OrgId == i).FirstOrDefault();
                        var regusrid = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.RegisteredUserId).FirstOrDefault();
                        //  IF USER IS LINKED TO ANOTHER STUDENT AT THIS ORG, THEN WE DONT NEED TO ADD THE USER TO THE REGUSERORG - WE GO INTO THIS CONDITION.
                        if (reguserinorg1 != null)
                        {
                            registeredUser.TempIntHolder = registeredUser.RegisteredUserId;
                            var studentfullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.FullName).FirstOrDefault();
                            string clear = null;
                            registeredUser.RegisteredUserId = Convert.ToInt32(clear);
                            var reguserid = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.RegisteredUserId).FirstOrDefault();
                            // ADD GUARDIAN INTO THE STUDENT GUARDIAN TABLE. 
                            var studentguardian = new StudentGuardian()
                            {
                                RegisteredUserId = reguserid,
                                GuardianFirstName = registeredUser.FirstName,
                                GuardianLastName = registeredUser.LastName,
                                GuardianFullName = registeredUser.FullName,
                                GuardianEmailAddress = registeredUser.Email,
                                StudentId = (int)registeredUser.TempIntHolder,
                                StudentFullName = studentfullname,
                                TitleId = registeredUser.TitleId,
                                RelationshipId = registeredUser.RelationshipId,
                                Telephone = registeredUser.Telephone,
                                DateAdded = DateTime.Now,
                                OrgId = i
                            };
                            db.StudentGuardians.Add(studentguardian);
                            db.SaveChanges();
                            // ADD GUARDIAN TO CLASS GROUP.
                            var rrr = Session["OrgId"].ToString();
                            int w = Convert.ToInt32(rrr);
                            var studentclassref = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.ClassRef).FirstOrDefault();
                            var orggrpref = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.OrgGroupId).FirstOrDefault();
                            var orggrptypeid = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.GroupTypeId).FirstOrDefault();
                            // CHECK IF GUARDIAN ALREADY EXISTS IN THE CLASS GROUP.
                            var chkifguardingrp = db.RegisteredUsersGroups.Where(x => x.RegisteredUserId == reguserid).Where(x => x.OrgGroupId == orggrpref).Select(x => x.OrgGroupId).Count();
                            // IF GUARDIAN DOES NOT ALREADY EXIST IN THE GROUP THEN ADD - WE GO INTO THIS CONDITION.
                            if (chkifguardingrp == 0)
                            {
                                // ADD GUARDIAN TO CLASS GROUP.
                                var regusergrp = new RegisteredUsersGroups
                                {
                                    RegisteredUserId = reguserid,
                                    OrgGroupId = orggrpref,
                                    Email = registeredUser.Email,
                                    RegUserOrgId = i,
                                    GroupTypeId = orggrptypeid
                                };
                                db.RegisteredUsersGroups.Add(regusergrp);
                                db.SaveChanges();
                            }
                            // UPON ADDING GUARDIAN - LOG EVENT - ADDING GUARDIAN IS EVENTTYPEID = 2.                         
                            var orgeventlog = new Org_Events_Log()
                            {
                                Org_Event_TypeId = 2,
                                Org_Event_Name = "Registered guardian",
                                Org_Event_SubjectId = reguserid.ToString(),
                                Org_Event_SubjectName = registeredUser.FullName,
                                Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                Org_Event_Time = DateTime.Now,
                                OrgId = Session["OrgId"].ToString()
                            };
                            db.Org_Events_Logs.Add(orgeventlog);
                            db.SaveChanges();
                            // THEN EXIT.
                            return RedirectToAction("Students", "RegisteredUsers");
                        }
                        //  IF USER IS NOT LINKED TO ANOTHER STUDENT AT THIS ORG, THAT MEANS USER HAS NO ACC IN THE REGUSERORG TABLE. ADD USER - WE GO INTO THIS CONDITION.
                        else
                        {
                            var onetomany = new RegisteredUserOrganisation()
                            {
                                RegisteredUserId = regusrid,
                                OrgId = i,
                                Email = registeredUser.Email,
                                FirstName = registeredUser.FirstName,
                                LastName = registeredUser.LastName,
                                OrgName = db.Orgs.Where(x => x.OrgId == i).Select(x => x.OrgName).FirstOrDefault(),
                                RegUserOrgBrand = registeredUser.RegUserOrgBrand,
                                RegisteredUserTypeId = registeredUser.RegisteredUserTypeId,
                                PrimarySchoolUserRoleId = registeredUser.PrimarySchoolUserRoleId,
                                SecondarySchoolUserRoleId = registeredUser.SecondarySchoolUserRoleId,
                                EnrolmentDate = DateTime.Now,
                                CreatedBy = Session["RegisteredUserId"].ToString(),
                                FullName = registeredUser.FullName,
                                TitleId = registeredUser.TitleId,
                                LastLogOn = null
                            };
                            db.RegisteredUserOrganisations.Add(onetomany);
                            db.SaveChanges();
                            // LOCATE STUDENT AND GUARDIAN DATA - SO IT CAN BE ADDED TO THE STUDENT GUARDIAN TABLE.
                            registeredUser.TempIntHolder = registeredUser.RegisteredUserId;
                            var studentfullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.FullName).FirstOrDefault();
                            string clear = null;
                            registeredUser.RegisteredUserId = Convert.ToInt32(clear);
                            var reguserid = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.RegisteredUserId).FirstOrDefault();
                            // ADD GUARDIAN INTO THE STUDENT GUARDIAN TABLE.
                            var studentguardian = new StudentGuardian()
                            {
                                RegisteredUserId = reguserid,
                                GuardianFirstName = registeredUser.FirstName,
                                GuardianLastName = registeredUser.LastName,
                                GuardianFullName = registeredUser.FullName,
                                GuardianEmailAddress = registeredUser.Email,
                                StudentId = (int)registeredUser.TempIntHolder,
                                StudentFullName = studentfullname,
                                DateAdded = DateTime.Now,
                                TitleId = registeredUser.TitleId,
                                RelationshipId = registeredUser.RelationshipId,
                                Telephone = registeredUser.Telephone,
                                OrgId = i
                            };
                            db.StudentGuardians.Add(studentguardian);
                            db.SaveChanges();
                            // LOCATE CLASS GROUP DATA.
                            var rrr = Session["OrgId"].ToString();
                            int w = Convert.ToInt32(rrr);
                            var studentclassref = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.ClassRef).FirstOrDefault();
                            var orggrpref = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.OrgGroupId).FirstOrDefault();
                            var orggrptypeid = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.GroupTypeId).FirstOrDefault();
                            // ADD GUARDIAN INTO THE CLASSS GROUP.
                            var regusergrp = new RegisteredUsersGroups
                            {
                                RegisteredUserId = reguserid,
                                OrgGroupId = orggrpref,
                                Email = registeredUser.Email,
                                RegUserOrgId = i,
                                GroupTypeId = orggrptypeid
                            };
                            db.RegisteredUsersGroups.Add(regusergrp);
                            db.SaveChanges();
                            // UPON ADDING GUARDIAN - LOG EVENT - LOGGING GUARDIAN IS EVENTTYPEID = 2.                             
                            var orgeventlog = new Org_Events_Log()
                            {
                                Org_Event_TypeId = 2,
                                Org_Event_Name = "Registered guardian",
                                Org_Event_SubjectId = reguserid.ToString(),
                                Org_Event_SubjectName = registeredUser.FullName,
                                Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                Org_Event_Time = DateTime.Now,
                                OrgId = Session["OrgId"].ToString()
                            };
                            db.Org_Events_Logs.Add(orgeventlog);
                            db.SaveChanges();
                            // THEN EXIT.
                            return RedirectToAction("Students", "RegisteredUsers");
                        }
                    }
                }
                /************************************************************THIS IS THE END OF CHECKING IF USER BEING ADDED IS AN EXISTING USER ON THE SYSTEM.**********************************************************/


                /************************************************************THIS IS THE BEGINNING OF ADDING A NEW USER ON THE SYSTEM.***********************************************************************************/

 // NEW DERTRIX USER             // ADDING DERTRIX USER - USER IS A DERTRIX USER - THEN THIS CONDITION IS TRUE - WE GO IN.//
                if (registeredUser.SelectedOrgList != null)
                {
                    var Dertrixuser = registeredUser.SelectedOrgList.FirstOrDefault().ToString();
                    int ii = Convert.ToInt32(Dertrixuser);
                    registeredUser.SelectedOrg = ii;
                    var pwd = "iamanewuser";
                    registeredUser.Password = pwd;
                    registeredUser.ConfirmPassword = pwd;
                    registeredUser.FullName = registeredUser.ContactFullName;
                    var regUserOrgBrand1 = db.Orgs.Where(x => x.OrgId == ii).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j1 = Convert.ToInt32(regUserOrgBrand1);
                    registeredUser.RegUserOrgBrand = j1;
                    registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                    registeredUser.EnrolmentDate = DateTime.Now;
                    registeredUser.RegisteredUserTypeId = registeredUser.RegisteredUserTypeId;
                    db.RegisteredUsers.Add(registeredUser);
                    db.SaveChanges();
                    // ADDING DERTRIX USER - INTO REGUSERORG//
                    var objRegisteredUserOrganisations = new RegisteredUserOrganisation()
                    {
                        RegisteredUserId = registeredUser.RegisteredUserId,
                        OrgId = ii,
                        Email = registeredUser.Email,
                        FirstName = registeredUser.FirstName,
                        LastName = registeredUser.LastName,
                        OrgName = db.Orgs.Where(x => x.OrgId == ii).Select(x => x.OrgName).FirstOrDefault(),
                        RegUserOrgBrand = registeredUser.RegUserOrgBrand,
                        IsTester = registeredUser.IsTester,
                        RegisteredUserTypeId = registeredUser.RegisteredUserTypeId,
                        PrimarySchoolUserRoleId = registeredUser.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = registeredUser.SecondarySchoolUserRoleId,
                        EnrolmentDate = DateTime.Now,
                        CreatedBy = Session["RegisteredUserId"].ToString(),
                        FullName = registeredUser.FullName,
                        LastLogOn = null
                    };
                    db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                    db.SaveChanges();
                    // THEN EXIT.
                    return RedirectToAction("SysAdminSetUp", "Home");
                }


 //NEW SCHOOL STAFF        // ADDING SCHOOL STAFFS - USER IS A SCHOOL STAFF - THEN THIS CONDITION IS TRUE - WE GO IN.//
                var chkifusrexist0 = db.RegisteredUsers.Where(x => x.RegisteredUserId == registeredUser.RegisteredUserId).Select(x => x.RegisteredUserId).FirstOrDefault();
                if (registeredUser.StudentRegFormId == null && registeredUser.SelectedOrg != 23 && chkifusrexist0 == 0)
                {
                    var rr1 = Session["OrgId"].ToString();
                    int w1 = Convert.ToInt32(rr1);
                    registeredUser.Email = registeredUser.Email;
                    var pwd = "iamanewuser";
                    registeredUser.Password = pwd;
                    registeredUser.ConfirmPassword = pwd;
                    registeredUser.TitleId = registeredUser.TitleId;
                    registeredUser.FullName = registeredUser.ContactFullName;
                    registeredUser.RegisteredUserTypeId = 2;
                    registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                    registeredUser.EnrolmentDate = DateTime.Now;
                    var regUserOrgBrand2 = db.Orgs.Where(x => x.OrgId == w1).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j2 = Convert.ToInt32(regUserOrgBrand2);
                    registeredUser.RegUserOrgBrand = j2;
                    db.RegisteredUsers.Add(registeredUser);
                    db.SaveChanges();
                    // ADDING SCHOOL STAFF - INTO REGUSERORG//
                    var objRegisteredUserOrganisations = new RegisteredUserOrganisation()
                    {
                        RegisteredUserId = registeredUser.RegisteredUserId,
                        OrgId = w1,
                        Email = registeredUser.Email,
                        FirstName = registeredUser.FirstName,
                        LastName = registeredUser.LastName,
                        OrgName = db.Orgs.Where(x => x.OrgId == w1).Select(x => x.OrgName).FirstOrDefault(),
                        RegUserOrgBrand = registeredUser.RegUserOrgBrand,
                        IsTester = registeredUser.IsTester,
                        RegisteredUserTypeId = registeredUser.RegisteredUserTypeId,
                        PrimarySchoolUserRoleId = registeredUser.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = registeredUser.SecondarySchoolUserRoleId,
                        EnrolmentDate = DateTime.Now,
                        CreatedBy = Session["RegisteredUserId"].ToString(),
                        FullName = registeredUser.FullName,
                        TitleId = registeredUser.TitleId,
                        LastLogOn = null
                    };
                    db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                    db.SaveChanges();
                    // UPON ADDING STAFF - LOG EVENT - LOGGING GUARDIAN IS EVENTTYPEID = 3.                             
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_TypeId = 3,
                        Org_Event_Name = "Registered staff",
                        Org_Event_SubjectId = registeredUser.RegisteredUserId.ToString(),
                        Org_Event_SubjectName = registeredUser.FullName,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString()
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();
                    // THEN EXIT.
                    return RedirectToAction("Staffs", "RegisteredUsers");
                }
                var rr2 = Session["OrgId"].ToString();
                int w2 = Convert.ToInt32(rr2);
                var chkifguardianexist1 = db.StudentGuardians.Where(x => x.GuardianEmailAddress == registeredUser.Email).Select(x => x.GuardianEmailAddress).FirstOrDefault();

 // NEW GUARDIAN // ADDING NEW GUARDIAN - USER IS A GUARDIAN  - THEN THIS CONDITION IS TRUE - WE GO IN.//
                if (chkifguardianexist1 == null && registeredUser.StudentRegFormId == null)
                {
                    var rr3 = Session["OrgId"].ToString();
                    int w = Convert.ToInt32(rr3);
                    registeredUser.TitleId = registeredUser.TitleId;
                    registeredUser.Email = registeredUser.Email;
                    var pwd = "iamanewuser";
                    registeredUser.Password = pwd;
                    registeredUser.ConfirmPassword = pwd;
                    registeredUser.FullName = registeredUser.ContactFullName;
                    registeredUser.Telephone = registeredUser.Telephone;
                    registeredUser.RegisteredUserTypeId = 2;
                    registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                    registeredUser.EnrolmentDate = DateTime.Now;
                    var regUserOrgBrand3 = db.Orgs.Where(x => x.OrgId == w).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j4 = Convert.ToInt32(regUserOrgBrand3);
                    registeredUser.RegUserOrgBrand = j4;
                    registeredUser.TempIntHolder = registeredUser.RegisteredUserId;
                    string clear = null;
                    registeredUser.RegisteredUserId = Convert.ToInt32(clear);
                    db.RegisteredUsers.Add(registeredUser);
                    db.SaveChanges();
                    // ADDING GUARDIAN  - INTO REGUSERORG//
                    var objRegisteredUserOrganisations = new RegisteredUserOrganisation()
                    {
                        TitleId = registeredUser.TitleId,
                        RegisteredUserId = registeredUser.RegisteredUserId,
                        OrgId = w,
                        Email = registeredUser.Email,
                        FirstName = registeredUser.FirstName,
                        LastName = registeredUser.LastName,
                        OrgName = db.Orgs.Where(x => x.OrgId == w).Select(x => x.OrgName).FirstOrDefault(),
                        RegUserOrgBrand = registeredUser.RegUserOrgBrand,
                        IsTester = registeredUser.IsTester,
                        RegisteredUserTypeId = registeredUser.RegisteredUserTypeId,
                        PrimarySchoolUserRoleId = registeredUser.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = registeredUser.SecondarySchoolUserRoleId,
                        EnrolmentDate = DateTime.Now,
                        CreatedBy = Session["RegisteredUserId"].ToString(),
                        FullName = registeredUser.FullName,
                        LastLogOn = null
                    };
                    db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                    db.SaveChanges();
                    // ADDING GUARDIAN  - INTO STUDENTGUARDIAN TABLE//
                    var studentfullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.FullName).FirstOrDefault();
                    var studentguardian = new StudentGuardian()
                    {
                        RegisteredUserId = registeredUser.RegisteredUserId,
                        TitleId = registeredUser.TitleId,
                        RelationshipId = registeredUser.RelationshipId,
                        GuardianFirstName = registeredUser.FirstName,
                        GuardianLastName = registeredUser.LastName,
                        GuardianFullName = registeredUser.FullName,
                        Telephone = registeredUser.Telephone,
                        GuardianEmailAddress = registeredUser.Email,
                        StudentId = (int)registeredUser.TempIntHolder,
                        StudentFullName = studentfullname,
                        DateAdded = DateTime.Now,
                        OrgId = w
                    };
                    db.StudentGuardians.Add(studentguardian);
                    db.SaveChanges();
                    // UPON ADDING GUARDIAN - LOG EVENT - LOGGING GUARDIAN IS EVENTTYPEID = 2.                             
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_TypeId = 2,
                        Org_Event_Name = "Registered guardian",
                        Org_Event_SubjectId = registeredUser.RegisteredUserId.ToString(),
                        Org_Event_SubjectName = registeredUser.FullName,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString()
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();
                    var studentclassref = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.ClassRef).FirstOrDefault();
                    var orggrpref = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.OrgGroupId).FirstOrDefault();
                    var orggrptypeid = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.GroupTypeId).FirstOrDefault();
                    //ADD GUARDIAN - INTO CLASS GROUP.                             
                    var regusergrp = new RegisteredUsersGroups
                    {
                        RegisteredUserId = registeredUser.RegisteredUserId,
                        OrgGroupId = orggrpref,
                        Email = registeredUser.Email,
                        RegUserOrgId = w,
                        GroupTypeId = orggrptypeid
                    };
                    db.RegisteredUsersGroups.Add(regusergrp);
                    db.SaveChanges();
                    // THEN EXIT
                    return RedirectToAction("Students", "RegisteredUsers");
                }

// NEW STUDENT                // ADDING NEW STUDENT - USER IS A STUDENT  - THEN THIS CONDITION IS TRUE - WE GO IN.//
                if (registeredUser.SelectedOrgList == null && registeredUser.StudentRegFormId != null)
                {
                    var rr4 = Session["OrgId"].ToString();
                    int w4 = Convert.ToInt32(rr4);
                    registeredUser.SelectedOrg = w4;
                    var email = "iamanewuser@thisorg.com";
                    registeredUser.Email = email;
                    var pwd = "iamanewuser";
                    registeredUser.Password = pwd;
                    registeredUser.ConfirmPassword = pwd;
                    registeredUser.FullName = registeredUser.ContactFullName;
                    registeredUser.RegisteredUserTypeId = 2;
                    registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                    registeredUser.EnrolmentDate = DateTime.Now;
                    registeredUser.DateOfBirth = registeredUser.DateOfBirth;
                    var regUserOrgBrand4 = db.Orgs.Where(x => x.OrgId == registeredUser.SelectedOrg).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j5 = Convert.ToInt32(regUserOrgBrand4);
                    registeredUser.ClassRef = db.Classes.Where(x => x.ClassId == registeredUser.ClassId).Select(x => x.ClassRefNumb).FirstOrDefault();
                    registeredUser.RegUserOrgBrand = j5;
                    db.RegisteredUsers.Add(registeredUser);
                    db.SaveChanges();
                    // UPON ADDING STUDENT - ADD STUDENT TO REGUSERORG
                    var objRegisteredUserOrganisations = new RegisteredUserOrganisation()
                    {
                        RegisteredUserId = registeredUser.RegisteredUserId,
                        OrgId = w4,
                        Email = registeredUser.Email,
                        FirstName = registeredUser.FirstName,
                        LastName = registeredUser.LastName,
                        OrgName = db.Orgs.Where(x => x.OrgId == w4).Select(x => x.OrgName).FirstOrDefault(),
                        RegUserOrgBrand = registeredUser.RegUserOrgBrand,
                        IsTester = registeredUser.IsTester,
                        RegisteredUserTypeId = registeredUser.RegisteredUserTypeId,
                        PrimarySchoolUserRoleId = registeredUser.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = registeredUser.SecondarySchoolUserRoleId,
                        EnrolmentDate = DateTime.Now,
                        CreatedBy = Session["RegisteredUserId"].ToString(),
                        FullName = registeredUser.FullName,
                        LastLogOn = null
                    };
                    db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                    db.SaveChanges();
                    // UPON ADDING STUDENT - LOG EVENT - LOGGING STUDENT IS EVENTTYPEID = 1
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_TypeId = 1,
                        Org_Event_Name = "Registered student",
                        Org_Event_SubjectId = registeredUser.RegisteredUserId.ToString(),
                        Org_Event_SubjectName = registeredUser.FullName,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = w4.ToString()
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();
                    // UPON ADDING STUDENT -  UPDATE CLASS DATA.
                    var rr6 = Session["OrgId"].ToString();
                    int i6 = Convert.ToInt32(rr6);
                    var getclassid = db.Classes.AsNoTracking().Where(x => x.ClassId == registeredUser.ClassId).FirstOrDefault();
                    var studentcount = db.RegisteredUsers.Where(x => x.ClassId == registeredUser.ClassId && x.SelectedOrg == i6).Count();
                    var FemStuCount = db.RegisteredUsers.Where(x => x.ClassId == registeredUser.ClassId && x.GenderId == 2 && x.SelectedOrg == i6).Count();
                    var MaleStudCount = db.RegisteredUsers.Where(x => x.ClassId == registeredUser.ClassId && x.GenderId == 1 && x.SelectedOrg == i6).Count();
                    var updateclass = new Class
                    {
                        ClassId = getclassid.ClassId,
                        ClassName = getclassid.ClassName,
                        ClassIsActive = getclassid.ClassIsActive,
                        OrgId = getclassid.OrgId,
                        ClassRefNumb = getclassid.ClassRefNumb,
                        TitleId = getclassid.TitleId,
                        ClassTeacherId = getclassid.ClassTeacherId,
                        ClassTeacherFullName = getclassid.ClassTeacherFullName,
                        Students_Count = studentcount,
                        Female_Students_Count = FemStuCount,
                        Male_Students_Count = MaleStudCount
                    };
                    getclassid = updateclass;
                    db.Entry(getclassid).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Students", "RegisteredUsers");
                    // THEN EXIT
                }
            }
            return View(registeredUser);
        }




        // POST: RegisteredUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisteredUser registeredUser)
        {
            if (registeredUser.StudentRegFormId == 1)
            {
                var classref = db.Classes.Where(x => x.ClassId == registeredUser.ClassId).Select(x => x.ClassRefNumb).FirstOrDefault();
                registeredUser.ClassRef = classref;
                registeredUser.Email = "iamanewuser@thisorg.com";
            }
            if (!(ModelState.IsValid) || ModelState.IsValid)
            {
                registeredUser.SelectedOrg = (int)Session["OrgId"];
                db.Entry(registeredUser).State = EntityState.Modified;
                db.SaveChanges();
            }

            //Updating registered user organisation with changes 
            var reguseridcount = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == registeredUser.RegisteredUserId).Select(p => p.RegisteredUserOrganisationId).ToList();
            var listofreguserid = new List<int>(reguseridcount);
            foreach (var re in reguseridcount)
            {
                var getid = db.RegisteredUserOrganisations.AsNoTracking().Where(x => x.RegisteredUserOrganisationId == re).FirstOrDefault();
                var reguser = new RegisteredUserOrganisation
                {
                    RegisteredUserOrganisationId = getid.RegisteredUserOrganisationId,
                    RegisteredUserId = getid.RegisteredUserId,
                    OrgId = getid.OrgId,
                    OrgName = getid.OrgName,
                    RegUserOrgBrand = getid.RegUserOrgBrand,
                    IsTester = getid.IsTester,
                    RegisteredUserTypeId = getid.RegisteredUserTypeId,
                    PrimarySchoolUserRoleId = getid.PrimarySchoolUserRoleId,
                    SecondarySchoolUserRoleId = getid.SecondarySchoolUserRoleId,
                    EnrolmentDate = getid.EnrolmentDate,
                    CreatedBy = getid.CreatedBy,
                    Email = registeredUser.Email,
                    FirstName = registeredUser.FirstName,
                    LastName = registeredUser.LastName,
                    FullName = registeredUser.FirstName + " " + registeredUser.LastName,
                    TitleId = registeredUser.TitleId,
                    LastLogOn = getid.LastLogOn
                };
                getid = reguser;
                db.Entry(getid).State = EntityState.Modified;
                db.SaveChanges();
            }
            //If registered user is a teacher, update teacher details in Class Model.
            var teacher = db.Classes.Where(x => x.ClassTeacherId == registeredUser.RegisteredUserId).Select(x => x.ClassId).ToList();
            var listofteacher = new List<int>(teacher);
            if (listofteacher.Count > 0)
            {
                foreach(var te in teacher)
                {
                    var getteacher = db.Classes.AsNoTracking().Where(x => x.ClassId == te).FirstOrDefault();
                    var regteacher = new Class
                    {
                      ClassId = getteacher.ClassId,
                      ClassName = getteacher.ClassName,
                      ClassIsActive = getteacher.ClassIsActive,
                      OrgId = getteacher.OrgId,
                      ClassRefNumb = getteacher.ClassRefNumb,
                      ClassTeacherId = getteacher.ClassTeacherId,
                      ClassTeacherFullName = registeredUser.FirstName + " " + registeredUser.LastName,
                      Students_Count = getteacher.Students_Count,
                      Female_Students_Count = getteacher.Female_Students_Count,
                      Male_Students_Count = getteacher.Male_Students_Count,
                      TitleId = registeredUser.TitleId
                    };
                    getteacher = regteacher;
                    db.Entry(getteacher).State = EntityState.Modified;
                    db.SaveChanges();
                }
            };

            //////If registered user is a student - update class object
            if (registeredUser.StudentRegFormId != null)
            {
                var updateclasses = UpdateClassProfile();
            }

            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName", registeredUser.RegisteredUserTypeId);
            return RedirectToAction("Index");
        }





        //Update Class profile.
        public ActionResult UpdateClassProfile()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            //Number of classes in org.
            var numbofclasses = db.Classes.Where(x => x.OrgId == i).Select(p => p.ClassId).ToList();
            var classestolist = new List<int>(numbofclasses);

            foreach (var cl in numbofclasses)
            {
                var classid = db.Classes.AsNoTracking().Where(x => x.OrgId == i && x.ClassId == cl).FirstOrDefault();
                var studentcount = db.RegisteredUsers.Where(x => x.ClassId == cl && x.SelectedOrg == i).Count();
                var FemStuCount = db.RegisteredUsers.Where(x => x.ClassId == cl && x.GenderId == 2 && x.SelectedOrg == i).Count();
                var MaleStudCount = db.RegisteredUsers.Where(x => x.ClassId == cl && x.GenderId == 1 && x.SelectedOrg == i).Count();

                var updateclass = new Class
                {
                    ClassId = classid.ClassId,
                    ClassName = classid.ClassName,
                    ClassIsActive = classid.ClassIsActive,
                    OrgId = classid.OrgId,
                    ClassRefNumb = classid.ClassRefNumb,
                    ClassTeacherId = classid.ClassTeacherId,
                    ClassTeacherFullName = classid.ClassTeacherFullName,
                    Students_Count = studentcount,
                    Female_Students_Count = FemStuCount,
                    Male_Students_Count = MaleStudCount,
                    TitleId = classid.TitleId
                };
                classid = updateclass;
                db.Entry(classid).State = EntityState.Modified;
                db.SaveChanges();
            };

            return RedirectToAction("Index");
        }




        // POST: RegisteredUsers/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            // CHECK IF USER BEING DELETED IS A STAFF = IF YES - WE GO INTO THIS CONDITION - LOG EVENT. REMOVING  STUDENT IS EVENTTYPEID = 4
            // LOCATE USERS ROLES
            var chkifPsStaff = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).Select(x => x.PrimarySchoolUserRoleId).FirstOrDefault();
            var chkifSsStaff = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).Select(x => x.SecondarySchoolUserRoleId).FirstOrDefault();

            if (chkifPsStaff == 1 || chkifPsStaff == 2 || chkifPsStaff == 3 || chkifPsStaff == 4 || chkifSsStaff == 1 || chkifSsStaff == 2 || chkifSsStaff == 3 || chkifSsStaff == 4)
            {
            
            var staforgcount = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == id).Select(x => x.RegisteredUserId).Count();
            // IF COUNT OF ORG IS 1 - WE GO INTO THIS CONDITION - WE DELETE USER FROM REGUSER TABLE / LOG EVENT AND MOVE ON.
             if (staforgcount == 1)
             {
            // GET STAFF'S FULLNAME
              var stafullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).Select(x => x.FullName).FirstOrDefault();
            // BEFORE REMVING STAFF - LOG EVENT. REMOVING STAFF IS EVENTTYPEID = 6
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_TypeId = 6,
                        Org_Event_Name = "Deregistered staff",
                        Org_Event_SubjectId = id.ToString(),
                        Org_Event_SubjectName = stafullname,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString()
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();

               RegisteredUser removestaff = db.RegisteredUsers.Find(id);
               db.RegisteredUsers.Remove(removestaff);
               db.SaveChanges();
                
               return RedirectToAction("Index");

             }
            // GET A LIST OF ORGS STAFF IS LINKED TO - LOOP THRU - AND DELETE FROM REGUSERORG TABLE AND LOG EVENT ONCE ON ORG THAT IS SAME AS ACTIVE SESSION.
            else
             {
                    var stafsorgs = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == id).Select(x => x.OrgId).ToList();
                    var linkedorgs = new List<int>(stafsorgs);
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);
                    foreach (var ruo in stafsorgs)
                    {
                      if (ruo == i)
                      {
                            var getstaff = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == id).Where(x => x.OrgId == i).Select(x => x.RegisteredUserOrganisationId).FirstOrDefault();
                            RegisteredUserOrganisation removestaff = db.RegisteredUserOrganisations.Find(getstaff);
                            db.RegisteredUserOrganisations.Remove(removestaff);
                            db.SaveChanges();

           // GET STAFF'S FULLNAME
                            var stafullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).Select(x => x.FullName).FirstOrDefault();

           // UPON REMVING STAFF - LOG EVENT. REMOVING STAFF IS EVENTTYPEID = 6
                            var orgeventlog = new Org_Events_Log()
                            {
                                Org_Event_TypeId = 6,
                                Org_Event_Name = "Deregistered staff",
                                Org_Event_SubjectId = id.ToString(),
                                Org_Event_SubjectName = stafullname,
                                Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                Org_Event_Time = DateTime.Now,
                                OrgId = Session["OrgId"].ToString()
                            };
                            db.Org_Events_Logs.Add(orgeventlog);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }

             }


            }



           // CHECK IF USER TO BE DELETED IS A STUDENT - IF YES - WE GO INTO THIS CONDITION.  
           // IF USER BEING DELETED IS A STUDENT - WE NEED TO LOCATE STUD'S GUARDIANS.
                 var chkifstud = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).Select(x => x.StudentRegFormId).FirstOrDefault();
            if (chkifstud != null)
            {
           //LIST NUMBER OF GUARDIANS LINKED TO STUDENT.
                var guardianstolist = db.StudentGuardians.Where(x => x.StudentId == id).Select(x => x.StudentGuardianId).ToList();
                var linkedguardians = new List<int>(guardianstolist);
           // IF LIST OF GUARDIANS IS NOT 0 - WE GO INTO THIS CONDITION. 
                if (guardianstolist.Count > 0)
                {
                    foreach (var gd in guardianstolist)
                    {
           // GET GUARDIAN REGUSERID
                        var gdreguserid = db.StudentGuardians.Where(x => x.StudentGuardianId == gd).Select(x => x.RegisteredUserId).FirstOrDefault();
           // CHECK IF THIS GUARDIAN IS LINKED TO ANY OTHER STUDENT. IF FALSE - WE REMV BOTH GUARDIAN AND STUDENT.
                        var mylinkedstudents = db.StudentGuardians.Where(x => x.RegisteredUserId == gdreguserid).Select(x => x.StudentId).ToList();
                        var linkedstudents = new List<int>(mylinkedstudents);

           // IF COUNT OF LINKED STUD IS ONLY 1 - REMV GUARD FROM REGUSER/REGUSERORG/STUDGUARDIAN TABLE
                        if (mylinkedstudents.Count == 1)
                        {
           //LOCATE GUARD IN REG USER TABLE AND DELETE.
                            var locateguard = db.StudentGuardians.Where(x => x.StudentGuardianId == gd).Select(x => x.RegisteredUserId).FirstOrDefault();
                            var guardfullname = db.StudentGuardians.Where(x => x.StudentGuardianId == gd).Select(x => x.GuardianFullName).FirstOrDefault();
                            RegisteredUser remvguard = db.RegisteredUsers.Find(locateguard);
                            db.RegisteredUsers.Remove(remvguard);
                            db.SaveChanges();

           // UPON REMVING GUARD - LOG EVENT. REMOVING GUARD IS EVENTTYPEID = 5
                            var orgeventlog = new Org_Events_Log()
                            {
                                Org_Event_TypeId = 5,
                                Org_Event_Name = "Deregistered guardian",
                                Org_Event_SubjectId = locateguard.ToString(),
                                Org_Event_SubjectName = guardfullname,
                                Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                Org_Event_Time = DateTime.Now,
                                OrgId = Session["OrgId"].ToString()
                            };
                            db.Org_Events_Logs.Add(orgeventlog);
                            db.SaveChanges();
                        }
                        // GUARDIAN IS LINKED TO MORE THAN 1 STUDENT - WE GO INTO THIS CONDITION. AND LOOP THRU LIST OF ALL STUDENTS TILL WE GET TO THE STUD BEING REMOVED.
                        else
                        {
                            foreach (var std in mylinkedstudents)
                            {
           // IF STD TO BE DELETED - WE GO INTO THIS CONDITION 
                                if (std == id)
                                {
                                    var rr = Session["OrgId"].ToString();
                                    int i = Convert.ToInt32(rr);
           // LOCATE THE GD IN THE STUD GUARD TABLE  
                                    var locateguard = db.StudentGuardians.Where(x => x.StudentGuardianId == gd).Select(x => x.RegisteredUserId).FirstOrDefault();
           // CHECK HOW MANY OTHER STUD GUARD IS LINKED TO IN ACTIVE ORG = IF ONLY 1 THEN WE CAN REMV GUARD FROM REGUSERORG TABLE
                                    var linkedstdinorgcount = db.StudentGuardians.Where(x => x.RegisteredUserId == locateguard).Where(x => x.OrgId == i).Select(x => x.OrgId).Count();
           // IF ONLY 1 - MEANS GUARD IS ONLY LINKED TO BE DELETED WE GO INTO THIS CONDITION - SO WE DELETE GUARD FROM REGUSERORG TABLE AND LOG EVENT.
                                    if (linkedstdinorgcount == 1)
                                    {
           // LOCATE GUARD IN REGUSER ORG TABLE.                                      
                                        var locategd1 = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == gdreguserid).Where(x => x.OrgId == i).Select(x => x.RegisteredUserOrganisationId).FirstOrDefault();
           // DELETE GUARD FROM REGUSER ORG TABLE.
                                        RegisteredUserOrganisation regusrorg = db.RegisteredUserOrganisations.Find(locategd1);
                                        db.RegisteredUserOrganisations.Remove(regusrorg);
                                        db.SaveChanges();
           // GET GUARDIANS FULLNAME 
                                    var guardfullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == gdreguserid).Select(x => x.FullName).FirstOrDefault();
           // WE THEN LOG EVENT - REMOVING GUARD IS EVENTTYPEID = 5
                                        var orgeventlog = new Org_Events_Log()
                                        {
                                            Org_Event_TypeId = 5,
                                            Org_Event_Name = "Deregistered guardian",
                                            Org_Event_SubjectId = gdreguserid.ToString(),
                                            Org_Event_SubjectName = guardfullname,
                                            Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                            Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                            Org_Event_Time = DateTime.Now,
                                            OrgId = Session["OrgId"].ToString()
                                        };
                                        db.Org_Events_Logs.Add(orgeventlog);
                                        db.SaveChanges();
                                    }
                                    // WE THEN DELETE GUARDIAN & LINKED STUDENT RECORD FROM STUDGUARD TABLE. 
                                    StudentGuardian studentguardian = db.StudentGuardians.Find(gd);
                                    db.StudentGuardians.Remove(studentguardian);
                                    db.SaveChanges();
         
                                    

                                }
                            }
                        }
                    }
                }
            }
           // IF USER BEING DELETED IS NOT A STUDENT - WE COME HERE STRAIGHT AND REMOVE USER.
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            db.RegisteredUsers.Remove(registeredUser);
            db.SaveChanges();

           // CHECK IF USER BEING DELETED IS A STUDENT = IF YES - LOG EVENT. REMOVING  STUDENT IS EVENTTYPEID = 4
            var chkifstud1 = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).Select(x => x.StudentRegFormId).FirstOrDefault();
            var studfullname = db.RegisteredUsers.Where(x => x.RelationshipId == id).Select(x => x.FullName).FirstOrDefault();
            if (chkifstud != null)
            {
                var orgeventlog = new Org_Events_Log()
                {
                    Org_Event_TypeId = 4,
                    Org_Event_Name = "Deregistered student",
                    Org_Event_SubjectId = id.ToString(),
                    Org_Event_SubjectName = registeredUser.FullName,
                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                    Org_Event_Time = DateTime.Now,
                    OrgId = Session["OrgId"].ToString()
                };
                db.Org_Events_Logs.Add(orgeventlog);
                db.SaveChanges();
            }



            var updateclasses = UpdateClassProfile();
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