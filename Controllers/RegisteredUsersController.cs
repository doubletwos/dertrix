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
                .Include(x => x.Title)
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
                ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", stud1.ClassId);
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









        public JsonResult AutoCompleteStudentFullname(string prefix, int? id)
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            id = i;
            var studentsfullname = (from stu in db.RegisteredUsers.Where(p => p.StudentRegFormId != null).Where(j => j.SelectedOrg == id)
                                    where stu.FullName.StartsWith(prefix)
                                    select new
                                    {
                                        label = stu.FullName,
                                        Val = stu.RegisteredUserId
                                    }).ToList();
            return Json(studentsfullname);
        }

        // GET: RegisteredUsers/Staffs/
        public ActionResult Staffs(int? id)
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


        public JsonResult AutoCompleteStaffFullname(string prefix, int? id)
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            id = i;
            var stafffullname = (from staf in db.RegisteredUsers.Where(p => p.StudentRegFormId == null).Where(j => j.SelectedOrg == id)
                                 where staf.FullName.StartsWith(prefix)
                                 select new
                                 {
                                     label = staf.FullName,
                                     Val = staf.RegisteredUserId
                                 }).ToList();
            return Json(stafffullname);
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
                // 1- Checking if user already exist on the system 
                var checkemail = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.Email).FirstOrDefault();
                var firstname = db.RegisteredUsers.Where(x => x.FirstName == registeredUser.FirstName).Select(x => x.FirstName).FirstOrDefault();
                var lastname = db.RegisteredUsers.Where(x => x.LastName == registeredUser.LastName).Select(x => x.LastName).FirstOrDefault();

                //sTEP 1 - Checking if email address is already in DB - If in DB assign the existing values to object
                if (checkemail != null)
                {
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);
                    if (registeredUser.SecondarySchoolUserRoleId != 5 && registeredUser.PrimarySchoolUserRoleId != 5)
                    {
                        registeredUser.RegisteredUserId = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(d => d.RegisteredUserId).FirstOrDefault();
                    }
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

                    /*STEP 2 - Adding already existing users to the RegUserOrg(Many to Many)*/
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
                            TitleId = registeredUser.TitleId
                        };
                        db.RegisteredUserOrganisations.Add(onetomany);
                        db.SaveChanges();

                        if (registeredUser.PrimarySchoolUserRoleId != 5 || registeredUser.SecondarySchoolUserRoleId != 5)
                        {
                            registeredUser.TempIntHolder = registeredUser.RegisteredUserId;
                            var studentfullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.FullName).FirstOrDefault();
                            string clear = null;
                            registeredUser.RegisteredUserId = Convert.ToInt32(clear);
                            var reguserid = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.RegisteredUserId).FirstOrDefault();
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
                                OrgId = i
                            };
                            if (registeredUser.PrimarySchoolUserRoleId == 5 || registeredUser.SecondarySchoolUserRoleId == 5)
                            {
                                db.StudentGuardians.Add(studentguardian);
                                db.SaveChanges();
                                return RedirectToAction("Students", "RegisteredUsers");
                            }
                            var reguserinorg1 = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Where(x => x.OrgId == i).FirstOrDefault();
                            if (reguserinorg1 != null)
                            {
                                return RedirectToAction("Staffs", "RegisteredUsers");
                            }

                            else
                            {
                                db.RegisteredUserOrganisations.Add(onetomany);
                                db.SaveChanges();
                                return RedirectToAction("Staffs", "RegisteredUsers");
                            }
                        }
                        else
                        {
                            return RedirectToAction("Staffs", "RegisteredUsers");
                        }
                    }
                }

                /* STEP 3- When Dertrix users are added at Dertrix Level*/
                if (registeredUser.SelectedOrgList != null)
                {
                    var Dertrixuser = registeredUser.SelectedOrgList.FirstOrDefault().ToString();
                    int i = Convert.ToInt32(Dertrixuser);
                    registeredUser.SelectedOrg = i;
                    var pwd = "iamanewuser";
                    registeredUser.Password = pwd;
                    registeredUser.ConfirmPassword = pwd;
                    registeredUser.FullName = registeredUser.ContactFullName;
                    var regUserOrgBrand = db.Orgs.Where(x => x.OrgId == i).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j = Convert.ToInt32(regUserOrgBrand);
                    registeredUser.RegUserOrgBrand = j;
                    registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                    registeredUser.EnrolmentDate = DateTime.Now;
                    registeredUser.RegisteredUserTypeId = registeredUser.RegisteredUserTypeId;
                    db.RegisteredUsers.Add(registeredUser);
                    db.SaveChanges();
                    var objRegisteredUserOrganisations = new RegisteredUserOrganisation()
                    {
                        RegisteredUserId = registeredUser.RegisteredUserId,
                        OrgId = i,
                        Email = registeredUser.Email,
                        FirstName = registeredUser.FirstName,
                        LastName = registeredUser.LastName,
                        OrgName = db.Orgs.Where(x => x.OrgId == i).Select(x => x.OrgName).FirstOrDefault(),
                        RegUserOrgBrand = registeredUser.RegUserOrgBrand,
                        IsTester = registeredUser.IsTester,
                        RegisteredUserTypeId = registeredUser.RegisteredUserTypeId,
                        PrimarySchoolUserRoleId = registeredUser.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = registeredUser.SecondarySchoolUserRoleId,
                        EnrolmentDate = DateTime.Now,
                        CreatedBy = Session["RegisteredUserId"].ToString(),
                        FullName = registeredUser.FullName
                    };
                    db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                    db.SaveChanges();
                    return RedirectToAction("SysAdminSetUp", "Home");
                }

                /*STEP 4- When NEW staffs are added at school level*/
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
                    var regUserOrgBrand = db.Orgs.Where(x => x.OrgId == w1).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j = Convert.ToInt32(regUserOrgBrand);
                    registeredUser.RegUserOrgBrand = j;
                    db.RegisteredUsers.Add(registeredUser);
                    db.SaveChanges();

                    /*Adding users to the RegUserOrg(Many to Many)*/
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
                        TitleId = registeredUser.TitleId

                    };
                    db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                    db.SaveChanges();
                    return RedirectToAction("Staffs", "RegisteredUsers");
                }

                /*STEP 5- When new  Guardians are added at school level*/
                var rr2 = Session["OrgId"].ToString();
                int w2 = Convert.ToInt32(rr2);
                var chkifguardianexist1 = db.StudentGuardians.Where(x => x.GuardianEmailAddress == registeredUser.Email).Where(x => x.OrgId == w2).Select(x => x.GuardianEmailAddress).FirstOrDefault();
                if (chkifguardianexist1 == null && registeredUser.StudentRegFormId == null)
                {
                    var rr = Session["OrgId"].ToString();
                    int w = Convert.ToInt32(rr);
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
                    var regUserOrgBrand = db.Orgs.Where(x => x.OrgId == w).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j = Convert.ToInt32(regUserOrgBrand);
                    registeredUser.RegUserOrgBrand = j;
                    registeredUser.TempIntHolder = registeredUser.RegisteredUserId;
                    string clear = null;
                    registeredUser.RegisteredUserId = Convert.ToInt32(clear);
                    db.RegisteredUsers.Add(registeredUser);
                    db.SaveChanges();
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
                        FullName = registeredUser.FullName
                    };
                    db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                    db.SaveChanges();
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
                    return RedirectToAction("Students", "RegisteredUsers");
                }

                //*STEP 6- When Existing Guardians are added at school level*/
                var rr3 = Session["OrgId"].ToString();
                int w3 = Convert.ToInt32(rr3);
                var chkifguardianexist0 = db.StudentGuardians.Where(x => x.GuardianEmailAddress == registeredUser.Email).Where(x => x.OrgId == w3).Select(x => x.GuardianEmailAddress).FirstOrDefault();
                if (chkifguardianexist0 != null && registeredUser.StudentRegFormId == null)
                {
                    var oi = Session["OrgId"].ToString();
                    int p = Convert.ToInt32(oi);
                    registeredUser.Email = registeredUser.Email;
                    var pwd = "iamanewuser";
                    registeredUser.Password = pwd;
                    registeredUser.ConfirmPassword = pwd;
                    registeredUser.FullName = registeredUser.ContactFullName;
                    registeredUser.RegisteredUserTypeId = 2;
                    registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                    registeredUser.EnrolmentDate = DateTime.Now;
                    var regUserOrgBrand = db.Orgs.Where(x => x.OrgId == p).Select(x => x.OrgBrandId).FirstOrDefault();
                    int w = Convert.ToInt32(regUserOrgBrand);
                    registeredUser.RegUserOrgBrand = w;
                    registeredUser.TempIntHolder = registeredUser.RegisteredUserId;
                    string clear = null;
                    registeredUser.RegisteredUserId = Convert.ToInt32(clear);
                    var studentfullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.FullName).FirstOrDefault();
                    var reguserid = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.RegisteredUserId).FirstOrDefault();
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
                        OrgId = w3,
                        Telephone = registeredUser.Telephone
                    };
                    db.StudentGuardians.Add(studentguardian);
                    db.SaveChanges();
                    return RedirectToAction("Students", "RegisteredUsers");
                }

                /*STEP 7- When students are added at school level*/
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
                    var regUserOrgBrand = db.Orgs.Where(x => x.OrgId == registeredUser.SelectedOrg).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j = Convert.ToInt32(regUserOrgBrand);
                    registeredUser.ClassRef = db.Classes.Where(x => x.ClassId == registeredUser.ClassId).Select(x => x.ClassRefNumb).FirstOrDefault();
                    registeredUser.RegUserOrgBrand = j;
                    db.RegisteredUsers.Add(registeredUser);
                    db.SaveChanges();
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
                        FullName = registeredUser.FullName
                    };
                    db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                    db.SaveChanges();
                    return RedirectToAction("Students", "RegisteredUsers");
                }
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.SelectedOrgList = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName", registeredUser.RegisteredUserTypeId);
            return View(registeredUser);
        }

        // POST: RegisteredUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisteredUser registeredUser)
        {
            if (registeredUser.StudentRegFormId == 1)
            {
                registeredUser.Email = "iamanewuser@thisorg.com";
            }
            if (!(ModelState.IsValid) || ModelState.IsValid)
            {
                registeredUser.SelectedOrg = (int)Session["OrgId"];
                db.Entry(registeredUser).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            //registered user id count
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
                    FullName = registeredUser.FullName,
                    TitleId = registeredUser.TitleId
                };

                getid = reguser;

                db.Entry(getid).State = EntityState.Modified;
                db.SaveChanges();
            }


            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName", registeredUser.RegisteredUserTypeId);
            return RedirectToAction("Index");

            //return View(registeredUser);
        }


        // POST: RegisteredUsers/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            db.RegisteredUsers.Remove(registeredUser);
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