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
using System.IO;
using OfficeOpenXml;
namespace Dertrix.Controllers
{
    [RoutePrefix("")]
    public class RegisteredUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        //     GET: RegisteredUsers/AllStudents/
        [Route("Students")]
        public ActionResult AllStudents(string searchname, string searchid)
        {
            if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
            {
                return RedirectToAction("WrongDevice", "Orgs");
            }
            if (Session["OrgId"] == null)
            {
                return RedirectToRoute(new { controller = "Access", action = "Signin", });
            }
            var orgid = (int)Session["OrgId"];
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





        [ChildActionOnly]
        public ActionResult Regs()
        {
            return PartialView("_Secure");
        }

        [ChildActionOnly]
        public ActionResult RegisterUser()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        [ChildActionOnly]
        public ActionResult Nav()
        {
            return PartialView("_Nav");
        }

        [ChildActionOnly]
        public ActionResult AddStudent()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                //ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.OrgId == i).OrderBy(w => w.ClassRefNumb).ToList(), "ClassId", "ClassName");
                ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
                ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
                ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles, "SecondarySchoolUserRoleId", "RoleName");
                ViewBag.ReligionId = new SelectList(db.Religions, "ReligionId", "ReligionName");
                ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName");
                ViewBag.StudentRegFormId = new SelectList(db.StudentRegForm, "StudentRegFormId", "Name");
                ViewBag.TribeId = new SelectList(db.Tribes, "TribeId", "TribeName");
                ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

                ViewBag.ClassId = new SelectList(
                                from x in db.Classes
                                .Where(x => x.OrgId == i)
                                .OrderBy(w => w.ClassRefNumb)
                                .ToList()
                                select new { x.ClassId, x.ClassName, Name_Id = x.ClassName + " " + "[" + x.ClassId + "]" },
                                "ClassId", "Name_Id");

                return PartialView("~/Views/Shared/PartialViewsForms/_AddStudent.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        [ChildActionOnly]
        public ActionResult UploadStudents()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                ViewBag.ClassId = new SelectList(db.Classes
                    .Where(x => x.OrgId == i)
                    .OrderBy(w => w.ClassRefNumb)
                    .ToList(), "ClassId", "ClassName");

                return PartialView("~/Views/Shared/PartialViewsForms/_UploadStudents.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }
        [ChildActionOnly]
        public ActionResult AddStaff()
        {
            try
            {
                ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName");
                ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
                ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
                ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles.Where(x => x.PrimarySchoolUserRoleID != 5), "PrimarySchoolUserRoleId", "RoleName");
                ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles.Where(x => x.SecondarySchoolUserRoleId != 5), "SecondarySchoolUserRoleId", "RoleName");
                ViewBag.NurserySchoolUserRoleId = new SelectList(db.NurserySchoolUserRoles.Where(x => x.NurserySchoolUserRoleId != 5), "NurserySchoolUserRoleId", "RoleName");

                return PartialView("~/Views/Shared/PartialViewsForms/_AddStaff.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }

        [Route("YourInformation")]
        public ActionResult AccountInfo()
        {
            try
            {
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access", action = "Signin", });
                }
                else
                {
                    var user = db.RegisteredUsers.Where(j => j.RegisteredUserId == RegisteredUserId).FirstOrDefault();

                    ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", user.TitleId);
                    ViewBag.Password = user.Password;
                    ViewBag.ConfirmPassword = user.Password;

                    return View(user);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAccountInfo(RegisteredUser registeredUser)
        {
            try
            {
                if (!(ModelState.IsValid) || ModelState.IsValid)
                {
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);

                    var locateuser = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == registeredUser.RegisteredUserId).FirstOrDefault();

                    // Title
                    if (registeredUser.TitleId != locateuser.TitleId)
                    {
                        // LOG USER CHANGE
                        var userchangelog = new User_Change_Events_Log()
                        {
                            RegUserId = registeredUser.RegisteredUserId,
                            ChangedBy = registeredUser.RegisteredUserId,
                            Old_Value = locateuser.TitleId.ToString(),
                            New_Value = registeredUser.TitleId.ToString(),
                            OrgId = rr,
                            User_Change_Event_Time = DateTime.Now,
                            User_Change_Events_Types = User_Change_Events_Types.Title,
                        };
                        db.User_Change_Events_Logs.Add(userchangelog);
                        db.SaveChanges();
                    }

                    // FirstName
                    if (registeredUser.FirstName != locateuser.FirstName)
                    {
                        // LOG USER CHANGE
                        var userchangelog = new User_Change_Events_Log()
                        {
                            RegUserId = registeredUser.RegisteredUserId,
                            ChangedBy = registeredUser.RegisteredUserId,
                            Old_Value = locateuser.FirstName,
                            New_Value = registeredUser.FirstName,
                            OrgId = rr,
                            User_Change_Event_Time = DateTime.Now,
                            User_Change_Events_Types = User_Change_Events_Types.FirstName,
                        };
                        db.User_Change_Events_Logs.Add(userchangelog);
                        db.SaveChanges();
                    }

                    // OtherNames
                    if (registeredUser.OtherNames != locateuser.OtherNames)
                    {
                        // LOG USER CHANGE
                        var userchangelog = new User_Change_Events_Log()
                        {
                            RegUserId = registeredUser.RegisteredUserId,
                            ChangedBy = registeredUser.RegisteredUserId,
                            Old_Value = locateuser.OtherNames,
                            New_Value = registeredUser.OtherNames,
                            OrgId = rr,
                            User_Change_Event_Time = DateTime.Now,
                            User_Change_Events_Types = User_Change_Events_Types.OtherNames,
                        };
                        db.User_Change_Events_Logs.Add(userchangelog);
                        db.SaveChanges();
                    }

                    // LastName
                    if (registeredUser.LastName != locateuser.LastName)
                    {
                        // LOG USER CHANGE
                        var userchangelog = new User_Change_Events_Log()
                        {
                            RegUserId = registeredUser.RegisteredUserId,
                            ChangedBy = registeredUser.RegisteredUserId,
                            Old_Value = locateuser.LastName,
                            New_Value = registeredUser.LastName,
                            OrgId = rr,
                            User_Change_Event_Time = DateTime.Now,
                            User_Change_Events_Types = User_Change_Events_Types.LastName,
                        };
                        db.User_Change_Events_Logs.Add(userchangelog);
                        db.SaveChanges();
                    }

                    // Email Address
                    if (registeredUser.Email != locateuser.Email)
                    {
                        // LOG USER CHANGE
                        var userchangelog = new User_Change_Events_Log()
                        {
                            RegUserId = registeredUser.RegisteredUserId,
                            ChangedBy = registeredUser.RegisteredUserId,
                            Old_Value = locateuser.Email,
                            New_Value = registeredUser.Email,
                            OrgId = rr,
                            User_Change_Event_Time = DateTime.Now,
                            User_Change_Events_Types = User_Change_Events_Types.EmailAddress,
                        };
                        db.User_Change_Events_Logs.Add(userchangelog);
                        db.SaveChanges();
                    }

                    // Password
                    if (registeredUser.Password != locateuser.Password)
                    {
                        // LOG USER CHANGE
                        var userchangelog = new User_Change_Events_Log()
                        {
                            RegUserId = registeredUser.RegisteredUserId,
                            ChangedBy = registeredUser.RegisteredUserId,
                            Old_Value = null,
                            New_Value = null,
                            OrgId = rr,
                            User_Change_Event_Time = DateTime.Now,
                            User_Change_Events_Types = User_Change_Events_Types.Password,
                        };
                        db.User_Change_Events_Logs.Add(userchangelog);
                        db.SaveChanges();
                    }

                    // Telephone
                    if (registeredUser.Telephone != locateuser.Telephone)
                    {
                        // LOG USER CHANGE
                        var userchangelog = new User_Change_Events_Log()
                        {
                            RegUserId = registeredUser.RegisteredUserId,
                            ChangedBy = registeredUser.RegisteredUserId,
                            Old_Value = locateuser.Telephone,
                            New_Value = registeredUser.Telephone,
                            OrgId = rr,
                            User_Change_Event_Time = DateTime.Now,
                            User_Change_Events_Types = User_Change_Events_Types.TelephoneNumber,
                        };
                        db.User_Change_Events_Logs.Add(userchangelog);
                        db.SaveChanges();
                    }

                    var studs = new RegisteredUser
                    {
                        RegisteredUserId = locateuser.RegisteredUserId,
                        RegisteredUserTypeId = locateuser.RegisteredUserTypeId,
                        TitleId = registeredUser.TitleId,
                        FirstName = registeredUser.FirstName,
                        OtherNames = registeredUser.OtherNames,
                        LastName = registeredUser.LastName,
                        Password = registeredUser.Password,
                        ConfirmPassword = registeredUser.ConfirmPassword,
                        Telephone = registeredUser.Telephone,
                        Email = registeredUser.Email,
                        SelectedOrg = locateuser.SelectedOrg,
                        ClassId = locateuser.ClassId,
                        GenderId = registeredUser.GenderId,
                        TribeId = registeredUser.TribeId,
                        DateOfBirth = registeredUser.DateOfBirth,
                        EnrolmentDate = locateuser.EnrolmentDate,
                        ReligionId = registeredUser.ReligionId,
                        StudentRegFormId = locateuser.StudentRegFormId,
                        CreatedBy = locateuser.CreatedBy,
                        RegUserOrgBrand = locateuser.RegUserOrgBrand,
                        FullName = registeredUser.FirstName + " " + registeredUser.OtherNames + " " + registeredUser.LastName,
                        ClassRef = locateuser.ClassRef,
                        PgCount = locateuser.PgCount,
                        InviteKey = locateuser.InviteKey,
                        InviteSentDate = locateuser.InviteSentDate,
                        CountOfInvite = locateuser.CountOfInvite,
                        IsRegistered = locateuser.IsRegistered,
                        RegisteredDate = locateuser.RegisteredDate,
                        IsTester = locateuser.IsTester,
                    };
                    locateuser = studs;
                    db.Entry(locateuser).State = EntityState.Modified;
                    db.SaveChanges();

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
                            EnrolmentDate = getid.EnrolmentDate,
                            CreatedBy = getid.CreatedBy,
                            Email = registeredUser.Email,
                            FirstName = registeredUser.FirstName,
                            OtherNames = registeredUser.OtherNames,
                            LastName = registeredUser.LastName,
                            FullName = registeredUser.FirstName + " " + registeredUser.OtherNames + " " + registeredUser.LastName,
                            TitleId = registeredUser.TitleId,
                            LastLogOn = getid.LastLogOn,
                            PrimarySchoolUserRoleId = getid.PrimarySchoolUserRoleId,
                            SecondarySchoolUserRoleId = getid.SecondarySchoolUserRoleId,
                            NurserySchoolUserRoleId = getid.NurserySchoolUserRoleId,
                            RegistrationFlags = getid.RegistrationFlags
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
                        foreach (var te in teacher)
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


                    //If registered user is a guardian, update guardian details in studentGuardian table
                    var locateGuard = db.StudentGuardians.Where(x => x.RegisteredUserId == registeredUser.RegisteredUserId).Select(x => x.StudentGuardianId).ToList();
                    var listofguardian = new List<int>(locateGuard);
                    if (listofguardian.Count > 0)
                    {
                        foreach (var pg in locateGuard)
                        {
                            var getguardian = db.StudentGuardians.AsNoTracking().Where(x => x.StudentGuardianId == pg).FirstOrDefault();
                            var parentgd = new StudentGuardian
                            {
                                StudentGuardianId = getguardian.StudentGuardianId,
                                RegisteredUserId = getguardian.RegisteredUserId,
                                GuardianFirstName = registeredUser.FirstName,
                                GuardianLastName = registeredUser.LastName,
                                GuardianFullName = registeredUser.FullName,
                                GuardianEmailAddress = registeredUser.Email,
                                DateAdded = getguardian.DateAdded,
                                StudentId = getguardian.StudentId,
                                StudentFullName = getguardian.StudentFullName,
                                OrgId = getguardian.OrgId,
                                TitleId = registeredUser.TitleId,
                                RelationshipId = getguardian.RelationshipId,
                                Telephone = registeredUser.Telephone,
                                Stu_class_Org_Grp_id = getguardian.Stu_class_Org_Grp_id,
                                IsRegistered = getguardian.IsRegistered,
                            };
                            getguardian = parentgd;
                            db.Entry(getguardian).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    };

                }
                TempData["Message"] = "Your details have been updated successfully";
                return RedirectToAction("AccountInfo", "RegisteredUsers", new { id = registeredUser.RegisteredUserId });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        [ChildActionOnly]
        public ActionResult AddGuardian()
        {
            try
            {
                ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
                ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
                ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
                ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles, "SecondarySchoolUserRoleId", "RoleName");
                return PartialView("~/Views/Shared/PartialViewsForms/_AddGuardian.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        [ChildActionOnly]
        public ActionResult AddSysAdmin()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                ViewBag.SelectedOrgList = new SelectList(db.Orgs, "OrgId", "OrgName");
                ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
                return PartialView("~/Views/Shared/PartialViewsForms/_AddSysAdmin.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        [ChildActionOnly]
        public ActionResult StudentCount()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        public ActionResult StudentDetails(int Id)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }
        public ActionResult DownloadStudentTemplateFile()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "Files/Template_Files/";
                string fileName = "test.xlsx";
                return File(path + fileName, "text/plain", "test.xlsx");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        [HttpPost]
        public ActionResult Uploader(HttpPostedFileBase postedFile, int classid)
        {
            try
            {

                var filePath = Server.MapPath("~/Files/UploadedFiles/");
                var FileName = System.IO.Path.GetFileName(postedFile.FileName);
                filePath = filePath + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);

                String path = Server.MapPath("~/Files/UploadedFiles/").Select(f => filePath).FirstOrDefault();
                var package = new ExcelPackage(new System.IO.FileInfo(path));
                int startColumn = 1;
                int startRow = 2;
                int successfulupload = 0;
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1]; // read sheet 1
                object data = null;
                var selectedClass = db.Classes.Where(x => x.ClassId == classid).Select(x => x.ClassId).FirstOrDefault();
                do
                {
                    data = workSheet.Cells[startRow, startColumn].Value; //column No
                    object firstName = workSheet.Cells[startRow, startColumn].Value;
                    if (firstName == null)
                    {
                        var col7 = workSheet.Cells[startRow, startColumn + 7].Value = "First Name is required.";
                        package.Save();
                        startRow++;
                        data = workSheet.Cells[startRow, startColumn].Value; //column No
                        continue;
                    }
                    object lastName = workSheet.Cells[startRow, startColumn + 1].Value;
                    if (lastName == null)
                    {
                        var col7 = workSheet.Cells[startRow, startColumn + 7].Value = "Last Name is required.";
                        package.Save();
                        startRow++;
                        data = workSheet.Cells[startRow, startColumn].Value; //column No
                        continue;
                    }
                    object _class = selectedClass;
                    object gender = workSheet.Cells[startRow, startColumn + 2].Value;
                    if (gender == null)
                    {
                        var col7 = workSheet.Cells[startRow, startColumn + 7].Value = "Gender is required.";
                        package.Save();
                        startRow++;
                        data = workSheet.Cells[startRow, startColumn].Value; //column No
                        continue;
                    }
                    object religion = workSheet.Cells[startRow, startColumn + 3].Value;
                    if (religion == null)
                    {
                        var col7 = workSheet.Cells[startRow, startColumn + 7].Value = "Religion is required.";
                        package.Save();
                        startRow++;
                        data = workSheet.Cells[startRow, startColumn].Value; //column No
                        continue;
                    }
                    object tribe = workSheet.Cells[startRow, startColumn + 4].Value;
                    if (tribe == null)
                    {
                        var col7 = workSheet.Cells[startRow, startColumn + 7].Value = "Tribe is required.";
                        package.Save();
                        startRow++;
                        data = workSheet.Cells[startRow, startColumn].Value; //column No
                        continue;
                    }
                    object dateOfBirth = workSheet.Cells[startRow, startColumn + 5].Value;
                    if (dateOfBirth == null)
                    {
                        var col7 = workSheet.Cells[startRow, startColumn + 7].Value = "Date of birth is required.";
                        package.Save();
                        startRow++;
                        data = workSheet.Cells[startRow, startColumn].Value; //column No
                        continue;
                    }
                    if (!String.IsNullOrEmpty(data.ToString()))
                    {
                        var isSuccess = SaveStudent(firstName.ToString(),
                        lastName.ToString(),
                        Convert.ToInt32(_class),
                        Convert.ToInt32(gender),
                        Convert.ToInt32(religion),
                        Convert.ToInt32(tribe),
                        Convert.ToDateTime(dateOfBirth));
                    }
                    if (data != null)
                    {
                        var col1 = workSheet.Cells[startRow, startColumn].Value = string.Empty;
                        var col2 = workSheet.Cells[startRow, startColumn + 1].Value = string.Empty;
                        var col3 = workSheet.Cells[startRow, startColumn + 2].Value = string.Empty;
                        var col4 = workSheet.Cells[startRow, startColumn + 3].Value = string.Empty;
                        var col5 = workSheet.Cells[startRow, startColumn + 4].Value = string.Empty;
                        var col6 = workSheet.Cells[startRow, startColumn + 5].Value = string.Empty;
                        var col7 = workSheet.Cells[startRow, startColumn + 6].Value = string.Empty;
                        package.Save();
                    }
                    startRow++;
                    successfulupload++;
                }
                while (!String.IsNullOrEmpty(data.ToString()));

                //////If registered user is a student - update class object
                if (String.IsNullOrEmpty(data.ToString()))
                {
                    var cid = classid;
                    var updateclasses = UpdateClassProfile(cid);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return RedirectToAction("AllStudents", "RegisteredUsers");
        }


        public bool SaveStudent(string firstName, string lastName, int _class, int gender, int religion, int tribe, DateTime dateofBirth)
        {
            try
            {
                var result = false;
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);
                var stud = new RegisteredUser();
                stud.RegisteredUserTypeId = 2;
                stud.FirstName = firstName;
                stud.LastName = lastName;
                stud.Email = "iamastudent";
                stud.SelectedOrg = i;
                stud.ClassId = _class;
                stud.GenderId = gender;
                stud.TribeId = tribe;
                stud.DateOfBirth = dateofBirth;
                stud.EnrolmentDate = DateTime.Now;
                stud.ReligionId = religion;
                stud.StudentRegFormId = 1;
                stud.CreatedBy = Session["RegisteredUserId"].ToString();
                stud.FullName = firstName + " " + lastName;
                stud.ClassRef = db.Classes.Where(x => x.ClassId == stud.ClassId).Select(x => x.ClassRefNumb).FirstOrDefault();
                stud.PgCount = 0;
                result = true;
                db.RegisteredUsers.Add(stud);
                db.SaveChanges();

                // UPON ADDING STUDENT - ADD STUDENT TO REGUSERORG
                // Students added via spreadsheet upload logged as 2
                var regstudinruo = new RegisteredUserOrganisation()
                {
                    RegisteredUserId = stud.RegisteredUserId,
                    OrgId = i,
                    FirstName = stud.FirstName,
                    LastName = stud.LastName,
                    OrgName = db.Orgs.Where(x => x.OrgId == i).Select(x => x.OrgName).FirstOrDefault(),
                    RegUserOrgBrand = stud.RegUserOrgBrand,
                    IsTester = stud.IsTester,
                    RegisteredUserTypeId = stud.RegisteredUserTypeId,
                    EnrolmentDate = DateTime.Now,
                    CreatedBy = Session["RegisteredUserId"].ToString(),
                    FullName = stud.FullName,
                    RegistrationFlags = RegistrationFlags.AddedViaSpreadsheet
                };
                db.RegisteredUserOrganisations.Add(regstudinruo);
                db.SaveChanges();
                // UPON ADDING STUDENT - LOG EVENT - 
                var logstudregistrtn = new Org_Events_Log()
                {
                    Org_Event_SubjectId = stud.RegisteredUserId.ToString(),
                    Org_Event_SubjectName = stud.FullName,
                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                    Org_Event_Time = DateTime.Now,
                    OrgId = i.ToString(),
                    Org_Events_Types = Org_Events_Types.Registered_Student
                };
                db.Org_Events_Logs.Add(logstudregistrtn);
                db.SaveChanges();



                // THEN -  CREATE STUDENTS MODULES
                var otherController = DependencyResolver.Current.GetService<StudentSubjectGradeController>();
                var fire = otherController.CreateStudentModules(_class, stud.RegisteredUserId, stud.ClassRef, i);

                // THEN EXIT
                return result;
            }
            catch (Exception e)
            {
                var result = false;
                Console.WriteLine(e);
                return result;
            }


        }



        public ActionResult MyRegProfile(int id)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        public ActionResult StaffDetails(int? Id)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var stud = db.RegisteredUserOrganisations
                .Where(x => x.RegisteredUserId == Id)
                .Where(x => x.OrgId == i)
                .Include(x => x.Title)
                .Include(x => x.PrimarySchoolUserRole)
                .Include(x => x.SecondarySchoolUserRole)
                .Include(x => x.NurserySchoolUserRole);
                ViewBag.RegisteredUserOrganisation = stud;
                return PartialView("_StaffDetails");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        public ActionResult GenerateGuardianInviteKey(int Id)
        {
            try
            {
                // LOCATE USER
                var reguser = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == Id).FirstOrDefault();

                // GENERATE INVITE KEY
                var ru = reguser.RegisteredUserId.ToString();
                var fn = reguser.FirstName;
                var ln = reguser.LastName;
                var en = reguser.EnrolmentDate.ToString();


                if (ru.Length >= 4 || fn.Length >= 4 || ln.Length >= 4 || en.Length >= 4)
                {
                    string newru = ru.Substring(ru.Length - 2);
                    var code1 = newru.ToUpper().ToString();

                    string newfn = fn.Substring(fn.Length - 2);
                    var code2 = newfn.ToUpper().ToString();

                    string newln = ln.Substring(ln.Length - 2);
                    var code3 = newln.ToUpper().ToString();

                    string newen = en.Substring(en.Length - 2);
                    var code4 = newen.ToUpper().ToString();

                    var invitecode = (code1 + code2 + code3 + code4);



                    // SAVE CODE IN USER'S DATA 

                    var usrtoupdt = new RegisteredUser
                    {
                        RegisteredUserId = reguser.RegisteredUserId,
                        RegisteredUserTypeId = reguser.RegisteredUserTypeId,
                        FirstName = reguser.FirstName,
                        LastName = reguser.LastName,
                        Email = reguser.Email,
                        LoginErrorMsg = reguser.LoginErrorMsg,
                        Password = reguser.Password,
                        ConfirmPassword = reguser.ConfirmPassword,
                        Telephone = reguser.Telephone,
                        SelectedOrg = reguser.SelectedOrg,
                        ClassId = reguser.ClassId,
                        GenderId = reguser.GenderId,
                        TribeId = reguser.TribeId,
                        DateOfBirth = reguser.DateOfBirth,
                        EnrolmentDate = reguser.EnrolmentDate,
                        ReligionId = reguser.ReligionId,
                        PrimarySchoolUserRoleId = reguser.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = reguser.SecondarySchoolUserRoleId,
                        StudentRegFormId = reguser.StudentRegFormId,
                        CreatedBy = reguser.CreatedBy,
                        RegUserOrgBrand = reguser.RegUserOrgBrand,
                        FullName = reguser.FullName,
                        IsTester = reguser.IsTester,
                        ClassRef = reguser.ClassRef,
                        TempIntHolder = reguser.TempIntHolder,
                        TitleId = reguser.TitleId,
                        RelationshipId = reguser.RelationshipId,
                        PgCount = reguser.PgCount,
                        NurserySchoolUserRoleId = reguser.NurserySchoolUserRoleId,
                        InviteKey = invitecode,
                        InviteSentDate = reguser.InviteSentDate,
                        CountOfInvite = reguser.CountOfInvite,
                        IsRegistered = reguser.IsRegistered,
                        RegisteredDate = reguser.RegisteredDate,
                    };
                    reguser = usrtoupdt;
                    db.Entry(reguser).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);


        }


        public ActionResult EditStudent(int Id)
        {
            try
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
                        OtherNames = stud1.OtherNames,
                        Email = stud1.Email,
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
                        ClassRef = stud1.ClassRef,
                        PgCount = stud1.PgCount,
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditStudent.cshtml", stud);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }


            return new HttpStatusCodeResult(204);
        }


        public ActionResult ChangeStudentsClass(int Id)
        {
            try
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
                        OtherNames = stud1.OtherNames,
                        LastName = stud1.LastName,
                        Email = stud1.Email,
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
                        ClassRef = stud1.ClassRef,
                        PgCount = stud1.PgCount
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_ChangeStudentsClass.cshtml", stud);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }


        public ActionResult ChangeStaffsRole(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);
                    var staff = db.RegisteredUserOrganisations
                        .Include(t => t.PrimarySchoolUserRole)
                        .Include(t => t.SecondarySchoolUserRole)
                        .Include(t => t.NurserySchoolUserRole)
                        .Where(x => x.RegisteredUserId == Id)
                        .Where(x => x.OrgId == i)
                        .FirstOrDefault();
                    ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles.Where(x => x.PrimarySchoolUserRoleID != 5), "PrimarySchoolUserRoleId", "RoleName", staff.PrimarySchoolUserRoleId);
                    ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles.Where(x => x.SecondarySchoolUserRoleId != 5), "SecondarySchoolUserRoleId", "RoleName", staff.SecondarySchoolUserRoleId);
                    ViewBag.NurserySchoolUserRoleId = new SelectList(db.NurserySchoolUserRoles.Where(x => x.NurserySchoolUserRoleId != 5), "NurserySchoolUserRoleId", "RoleName", staff.NurserySchoolUserRoleId);

                    var staff0 = new RegisteredUserOrganisation
                    {
                        RegisteredUserOrganisationId = staff.RegisteredUserOrganisationId,
                        RegisteredUserId = staff.RegisteredUserId,
                        OrgId = staff.OrgId,
                        Email = staff.Email,
                        FirstName = staff.FirstName,
                        LastName = staff.LastName,
                        OrgName = staff.OrgName,
                        RegUserOrgBrand = staff.RegUserOrgBrand,
                        IsTester = staff.IsTester,
                        RegisteredUserTypeId = staff.RegisteredUserTypeId,
                        PrimarySchoolUserRoleId = staff.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = staff.SecondarySchoolUserRoleId,
                        NurserySchoolUserRoleId = staff.NurserySchoolUserRoleId,
                        EnrolmentDate = staff.EnrolmentDate,
                        CreatedBy = staff.CreatedBy,
                        FullName = staff.FullName,
                        TitleId = staff.TitleId,
                        LastLogOn = staff.LastLogOn,
                        RegistrationFlags = staff.RegistrationFlags
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_ChangeStaffsRole.cshtml", staff0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }


            return PartialView("_ChangeStaffsRole");
        }
        [ChildActionOnly]
        public ActionResult LinkGuardianToStudent(int? Id)
        {
            try
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
                    ViewBag.NurserySchoolUserRoleId = new SelectList(db.NurserySchoolUserRoles, "NurserySchoolUserRoleId", "RoleName");
                    ViewBag.RelationshipId = new SelectList(db.Relationships, "RelationshipId", "RelationshipName");
                    ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName");
                    var stud = new RegisteredUser
                    {
                        RegisteredUserId = stud1.RegisteredUserId,
                        FullName = stud1.FullName
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_LinkGuardianToStudent.cshtml", stud);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }


        public ActionResult EditStaff(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);
                    var stud1 = db.RegisteredUserOrganisations
                        .Where(x => x.RegisteredUserId == Id)
                        .Where(x => x.OrgId == i)
                        .FirstOrDefault();
                    ViewBag.TitleId = new SelectList(
                    from x in db.Titles select new { x.TitleId, x.TitleName, Name_Id = x.TitleName + " " + "[" + x.TitleId + "]" },
                    "TitleId", "Name_Id", stud1.TitleId);
                    var stud = new RegisteredUserOrganisation
                    {
                        RegisteredUserOrganisationId = stud1.RegisteredUserOrganisationId,
                        RegisteredUserId = stud1.RegisteredUserId,
                        OrgId = stud1.OrgId,
                        Email = stud1.Email,
                        FirstName = stud1.FirstName,
                        LastName = stud1.LastName,
                        OrgName = stud1.OrgName,
                        RegUserOrgBrand = stud1.RegUserOrgBrand,
                        IsTester = stud1.IsTester,
                        RegisteredUserTypeId = stud1.RegisteredUserTypeId,
                        PrimarySchoolUserRoleId = stud1.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = stud1.SecondarySchoolUserRoleId,
                        NurserySchoolUserRoleId = stud1.NurserySchoolUserRoleId,
                        EnrolmentDate = stud1.EnrolmentDate,
                        CreatedBy = stud1.CreatedBy,
                        FullName = stud1.FullName,
                        TitleId = stud1.TitleId,
                        LastLogOn = stud1.LastLogOn,
                        RegistrationFlags = stud1.RegistrationFlags
                    };
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditStaff.cshtml", stud);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

        // GET: RegisteredUsers/Staffs/
        [Route("SchStaffs")]
        public ActionResult Staffs(int? id)
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access", action = "Signin", });
                }
                if (Session["OrgId"] != null)
                {
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);
                    id = i;
                }
                var staffs = db.RegisteredUserOrganisations
                    .Where(j => j.OrgId == id)
                    .Where(p => p.PrimarySchoolUserRoleId != null || p.SecondarySchoolUserRoleId != null || p.NurserySchoolUserRoleId != null)
                    .Where(p => p.PrimarySchoolUserRoleId != 5)
                    .Where(p => p.SecondarySchoolUserRoleId != 5)
                    .Where(p => p.NurserySchoolUserRoleId != 5)
                    .Include(p => p.Title)
                    .Include(p => p.PrimarySchoolUserRole)
                    .Include(p => p.SecondarySchoolUserRole)
                    .Include(p => p.NurserySchoolUserRole)
                    .ToList();
                return View(staffs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }


        }
        // GET: RegisteredUsers/SysAdmins/
        [Route("SysAdmins")]
        public ActionResult SysAdmins(int? id)
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access", action = "Signin", });
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        // GET: RegisteredUsers/ClassStudents/
        [Route("ClassStudents")]
        public ActionResult ClassStudents(int? id)
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access", action = "Signin", });
                }
                var orgid = (int)Session["OrgId"];
                var classref = db.Classes.Where(x => x.ClassId == id).Select(x => x.ClassRefNumb).FirstOrDefault();
                var students = db.RegisteredUsers
                    .Where(x => x.StudentRegFormId != null && x.SelectedOrg == orgid && x.ClassRef == classref && x.ClassId == id)
                    .ToList();
                return View(students);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }


        }



        // POST: RegisteredUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisteredUser registeredUser)
        {
            try
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
                        if (registeredUser.SecondarySchoolUserRoleId != 5 && registeredUser.PrimarySchoolUserRoleId != 5 && registeredUser.NurserySchoolUserRoleId != 5)
                        {
                            // SET ROLE TO NON TEACHING STAFF IF ROLE IS NOT SET.
                            if (registeredUser.SecondarySchoolUserRoleId == null && registeredUser.PrimarySchoolUserRoleId == null && registeredUser.NurserySchoolUserRoleId == null)
                            {
                                // ORG IS SECONDARY SCH
                                if ((int)Session["OrgType"] == 2)
                                {
                                    registeredUser.SecondarySchoolUserRoleId = 6;
                                }
                                // ORG IS PRIMARY SCH
                                if ((int)Session["OrgType"] == 3)
                                {
                                    registeredUser.PrimarySchoolUserRoleId = 6;
                                }
                                // ORG IS NURSERY SCH
                                if ((int)Session["OrgType"] == 4)
                                {
                                    registeredUser.NurserySchoolUserRoleId = 6;
                                }
                            }
                            else
                            {
                                registeredUser.SecondarySchoolUserRoleId = registeredUser.SecondarySchoolUserRoleId;
                                registeredUser.PrimarySchoolUserRoleId = registeredUser.PrimarySchoolUserRoleId;
                                registeredUser.NurserySchoolUserRoleId = registeredUser.NurserySchoolUserRoleId;
                            }
                            registeredUser.RegisteredUserId = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(d => d.RegisteredUserId).FirstOrDefault();
                            // CHECK TO MAKE SURE THE USER DOES NOT ALREADY HAVE AN ACCOUNT AT THIS ORG - IF NO - THEN WE ADD THE USER TO THE REGUSERORG. 
                            var reguserinorg = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Where(x => x.OrgId == i).FirstOrDefault();
                            if (reguserinorg == null)
                            {
                                var regusrid = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.RegisteredUserId).FirstOrDefault();
                                var getlastlogon = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == regusrid).Select(x => x.LastLogOn).FirstOrDefault();
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
                                    NurserySchoolUserRoleId = registeredUser.NurserySchoolUserRoleId,
                                    EnrolmentDate = DateTime.Now,
                                    CreatedBy = Session["RegisteredUserId"].ToString(),
                                    FullName = registeredUser.FullName,
                                    TitleId = registeredUser.TitleId,
                                    LastLogOn = getlastlogon,
                                    RegistrationFlags = RegistrationFlags.ManuallyAdded
                                };
                                db.RegisteredUserOrganisations.Add(onetomany);
                                db.SaveChanges();
                                // UPON ADDING A STAFF - LOG THE EVENT
                                var orgeventlog = new Org_Events_Log()
                                {
                                    Org_Event_SubjectId = registeredUser.RegisteredUserId.ToString(),
                                    Org_Event_SubjectName = registeredUser.FullName,
                                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                    Org_Event_Time = DateTime.Now,
                                    OrgId = Session["OrgId"].ToString(),
                                    Org_Events_Types = Org_Events_Types.Registered_Staff
                                };
                                db.Org_Events_Logs.Add(orgeventlog);
                                db.SaveChanges();
                                // THEN EXIT.
                                return RedirectToAction("Staffs", "RegisteredUsers");
                            }
                        }
                        //EXISTING GUARDIANS     // CHECKING TO SEE IF THE USER BEING ADDED IS A GUARDIAN - (ONLY GUARDIANS SHOULD GO INTO THIS CONDITION).
                        if (registeredUser.PrimarySchoolUserRoleId != 5 || registeredUser.SecondarySchoolUserRoleId != 5 || registeredUser.NurserySchoolUserRoleId != 5)
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
                                // LOCATE CLASS GROUP DATA.
                                var r5 = Session["OrgId"].ToString();
                                int w_5 = Convert.ToInt32(r5);
                                var studentclassref_1 = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.ClassRef).FirstOrDefault();
                                var orggrpref_1 = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref_1 && x.OrgId == w_5).Select(x => x.OrgGroupId).FirstOrDefault();
                                var orggrptypeid_1 = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref_1 && x.OrgId == w_5).Select(x => x.GroupTypeId).FirstOrDefault();

                                // ADD GUARDIAN INTO THE CLASSS GROUP.
                                var regusergrp_1 = new RegisteredUsersGroups
                                {
                                    RegisteredUserId = reguserid,
                                    OrgGroupId = orggrpref_1,
                                    Email = registeredUser.Email,
                                    RegUserOrgId = i,
                                    GroupTypeId = orggrptypeid_1,
                                    LinkedStudentId = (int)registeredUser.TempIntHolder
                                };
                                db.RegisteredUsersGroups.Add(regusergrp_1);
                                db.SaveChanges();
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
                                    OrgId = i,
                                    Stu_class_Org_Grp_id = orggrpref_1,
                                    IsRegistered = false
                                };
                                db.StudentGuardians.Add(studentguardian);
                                db.SaveChanges();

                                // ADD GUARDIAN TO CLASS GROUP.
                                var rrr = Session["OrgId"].ToString();
                                int w = Convert.ToInt32(rrr);
                                var studentclassref = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.ClassRef).FirstOrDefault();
                                var orggrpref = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.OrgGroupId).FirstOrDefault();
                                var orggrptypeid = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.GroupTypeId).FirstOrDefault();

                                var otherController = DependencyResolver.Current.GetService<RegisteredUsersGroupsController>();
                                var result = otherController.UpdateGroupMemberCount(orggrpref, w);

                                // UPDATE STUD'S GUARDIAN COUNT.
                                var studid = db.RegisteredUsers.Where(x => x.RegisteredUserId == registeredUser.TempIntHolder).Select(x => x.RegisteredUserId).FirstOrDefault();
                                var locatestud = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == studid).FirstOrDefault();
                                var currentcount = db.RegisteredUsers.Where(x => x.RegisteredUserId == registeredUser.TempIntHolder).Select(x => x.PgCount).FirstOrDefault();
                                // SET PG TO 0 IF NULL
                                if (currentcount == null)
                                {
                                    var zero = 0;
                                    currentcount = zero;
                                }
                                var studgaurd = new RegisteredUser
                                {
                                    RegisteredUserId = locatestud.RegisteredUserId,
                                    RegisteredUserTypeId = locatestud.RegisteredUserTypeId,
                                    FirstName = locatestud.FirstName,
                                    LastName = locatestud.LastName,
                                    Email = locatestud.Email,
                                    LoginErrorMsg = locatestud.LoginErrorMsg,
                                    Password = locatestud.Password,
                                    ConfirmPassword = locatestud.ConfirmPassword,
                                    Telephone = locatestud.Telephone,
                                    SelectedOrg = locatestud.SelectedOrg,
                                    ClassId = locatestud.ClassId,
                                    GenderId = locatestud.GenderId,
                                    TribeId = locatestud.TribeId,
                                    DateOfBirth = locatestud.DateOfBirth,
                                    EnrolmentDate = locatestud.EnrolmentDate,
                                    ReligionId = locatestud.ReligionId,
                                    PrimarySchoolUserRoleId = locatestud.PrimarySchoolUserRoleId,
                                    SecondarySchoolUserRoleId = locatestud.SecondarySchoolUserRoleId,
                                    NurserySchoolUserRoleId = locatestud.NurserySchoolUserRoleId,
                                    StudentRegFormId = locatestud.StudentRegFormId,
                                    CreatedBy = locatestud.CreatedBy,
                                    RegUserOrgBrand = locatestud.RegUserOrgBrand,
                                    FullName = locatestud.FirstName + " " + locatestud.LastName,
                                    IsTester = locatestud.IsTester,
                                    TempIntHolder = locatestud.TempIntHolder,
                                    TitleId = locatestud.TitleId,
                                    RelationshipId = locatestud.RelationshipId,
                                    ClassRef = locatestud.ClassRef,
                                    PgCount = currentcount + 1,
                                    InviteKey = locatestud.InviteKey,
                                    InviteSentDate = locatestud.InviteSentDate,
                                    CountOfInvite = locatestud.CountOfInvite,
                                    IsRegistered = locatestud.IsRegistered,
                                    RegisteredDate = locatestud.RegisteredDate
                                };
                                locatestud = studgaurd;
                                db.Entry(studgaurd).State = EntityState.Modified;
                                db.SaveChanges();
                                // UPON ADDING GUARDIAN - LOG EVENT -                       
                                var orgeventlog = new Org_Events_Log()
                                {
                                    Org_Event_SubjectId = reguserid.ToString(),
                                    Org_Event_SubjectName = registeredUser.FullName,
                                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                    Org_Event_Time = DateTime.Now,
                                    OrgId = Session["OrgId"].ToString(),
                                    Org_Events_Types = Org_Events_Types.Registered_Guardian
                                };
                                db.Org_Events_Logs.Add(orgeventlog);
                                db.SaveChanges();
                                // THEN EXIT.
                                return RedirectToAction("AllStudents", "RegisteredUsers");
                            }
                            //  IF USER IS NOT LINKED TO ANOTHER STUDENT AT THIS ORG, THAT MEANS USER HAS NO ACC IN THE REGUSERORG TABLE. ADD USER - WE GO INTO THIS CONDITION.
                            else
                            {
                                var getlastlogon = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == regusrid).Select(x => x.LastLogOn).FirstOrDefault();
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
                                    NurserySchoolUserRoleId = registeredUser.NurserySchoolUserRoleId,
                                    EnrolmentDate = DateTime.Now,
                                    CreatedBy = Session["RegisteredUserId"].ToString(),
                                    FullName = registeredUser.FullName,
                                    TitleId = registeredUser.TitleId,
                                    LastLogOn = getlastlogon,
                                    RegistrationFlags = RegistrationFlags.ManuallyAdded
                                };
                                db.RegisteredUserOrganisations.Add(onetomany);
                                db.SaveChanges();

                                // LOCATE STUDENT AND GUARDIAN DATA - SO IT CAN BE ADDED TO THE STUDENT GUARDIAN TABLE.
                                registeredUser.TempIntHolder = registeredUser.RegisteredUserId;
                                var studentfullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.FullName).FirstOrDefault();
                                string clear = null;
                                registeredUser.RegisteredUserId = Convert.ToInt32(clear);
                                var reguserid = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.RegisteredUserId).FirstOrDefault();
                                // LOCATE CLASS GROUP DATA.
                                var rrr = Session["OrgId"].ToString();
                                int w = Convert.ToInt32(rrr);
                                var studentclassref = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.ClassRef).FirstOrDefault();
                                var orggrpref = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.OrgGroupId).FirstOrDefault();
                                var orggrptypeid = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.GroupTypeId).FirstOrDefault();

                                // ADD GUARDIAN INTO THE CLASS GROUP.
                                var regusergrp = new RegisteredUsersGroups
                                {
                                    RegisteredUserId = reguserid,
                                    OrgGroupId = orggrpref,
                                    Email = registeredUser.Email,
                                    RegUserOrgId = i,
                                    GroupTypeId = orggrptypeid,
                                    LinkedStudentId = (int)registeredUser.TempIntHolder
                                };
                                db.RegisteredUsersGroups.Add(regusergrp);
                                db.SaveChanges();



                                // UPDATE STUD'S GUARDIAN COUNT.
                                var studid = db.RegisteredUsers.Where(x => x.RegisteredUserId == registeredUser.TempIntHolder).Select(x => x.RegisteredUserId).FirstOrDefault();
                                var locatestud = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == studid).FirstOrDefault();
                                var currentcount = db.RegisteredUsers.Where(x => x.RegisteredUserId == registeredUser.TempIntHolder).Select(x => x.PgCount).FirstOrDefault();
                                // SET PG TO 0 IF NULL
                                if (currentcount == null)
                                {
                                    var zero = 0;
                                    currentcount = zero;
                                }
                                var studgaurd = new RegisteredUser
                                {
                                    RegisteredUserId = locatestud.RegisteredUserId,
                                    RegisteredUserTypeId = locatestud.RegisteredUserTypeId,
                                    FirstName = locatestud.FirstName,
                                    LastName = locatestud.LastName,
                                    Email = locatestud.Email,
                                    LoginErrorMsg = locatestud.LoginErrorMsg,
                                    Password = locatestud.Password,
                                    ConfirmPassword = locatestud.ConfirmPassword,
                                    Telephone = locatestud.Telephone,
                                    SelectedOrg = locatestud.SelectedOrg,
                                    ClassId = locatestud.ClassId,
                                    GenderId = locatestud.GenderId,
                                    TribeId = locatestud.TribeId,
                                    DateOfBirth = locatestud.DateOfBirth,
                                    EnrolmentDate = locatestud.EnrolmentDate,
                                    ReligionId = locatestud.ReligionId,
                                    PrimarySchoolUserRoleId = locatestud.PrimarySchoolUserRoleId,
                                    SecondarySchoolUserRoleId = locatestud.SecondarySchoolUserRoleId,
                                    NurserySchoolUserRoleId = locatestud.NurserySchoolUserRoleId,
                                    StudentRegFormId = locatestud.StudentRegFormId,
                                    CreatedBy = locatestud.CreatedBy,
                                    RegUserOrgBrand = locatestud.RegUserOrgBrand,
                                    FullName = locatestud.FirstName + " " + locatestud.LastName,
                                    IsTester = locatestud.IsTester,
                                    TempIntHolder = locatestud.TempIntHolder,
                                    TitleId = locatestud.TitleId,
                                    RelationshipId = locatestud.RelationshipId,
                                    ClassRef = locatestud.ClassRef,
                                    PgCount = currentcount + 1,
                                    InviteKey = locatestud.InviteKey,
                                    InviteSentDate = locatestud.InviteSentDate,
                                    CountOfInvite = locatestud.CountOfInvite,
                                    IsRegistered = locatestud.IsRegistered,
                                    RegisteredDate = locatestud.RegisteredDate
                                };
                                locatestud = studgaurd;
                                db.Entry(studgaurd).State = EntityState.Modified;
                                db.SaveChanges();
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
                                    OrgId = i,
                                    Stu_class_Org_Grp_id = orggrpref
                                };
                                db.StudentGuardians.Add(studentguardian);
                                db.SaveChanges();
                                // UPON ADDING GUARDIAN - LOG EVENT -                              
                                var orgeventlog = new Org_Events_Log()
                                {
                                    Org_Event_SubjectId = reguserid.ToString(),
                                    Org_Event_SubjectName = registeredUser.FullName,
                                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                    Org_Event_Time = DateTime.Now,
                                    OrgId = Session["OrgId"].ToString(),
                                    Org_Events_Types = Org_Events_Types.Registered_Guardian
                                };
                                db.Org_Events_Logs.Add(orgeventlog);
                                db.SaveChanges();



                                // UPDATE GROUPS COUNT
                                var otherController = DependencyResolver.Current.GetService<RegisteredUsersGroupsController>();
                                var result = otherController.UpdateGroupMemberCount(orggrpref, w);

                                // THEN EXIT.
                                return RedirectToAction("AllStudents", "RegisteredUsers");
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
                            NurserySchoolUserRoleId = registeredUser.NurserySchoolUserRoleId,
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
                        // SET ROLE TO NON TEACHING STAFF IF ROLE IS NOT SET.
                        if (registeredUser.SecondarySchoolUserRoleId == null && registeredUser.PrimarySchoolUserRoleId == null && registeredUser.NurserySchoolUserRoleId == null)
                        {
                            // ORG IS SECONDARY SCH
                            if ((int)Session["OrgType"] == 2)
                            {
                                registeredUser.SecondarySchoolUserRoleId = 6;
                            }
                            // ORG IS PRIMARY SCH
                            if ((int)Session["OrgType"] == 3)
                            {
                                registeredUser.PrimarySchoolUserRoleId = 6;
                            }
                            // ORG IS NURSERY SCH
                            if ((int)Session["OrgType"] == 4)
                            {
                                registeredUser.NurserySchoolUserRoleId = 6;
                            }
                        }
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
                            NurserySchoolUserRoleId = registeredUser.NurserySchoolUserRoleId,
                            EnrolmentDate = DateTime.Now,
                            CreatedBy = Session["RegisteredUserId"].ToString(),
                            FullName = registeredUser.FullName,
                            TitleId = registeredUser.TitleId,
                            LastLogOn = null,
                            RegistrationFlags = RegistrationFlags.ManuallyAdded
                        };
                        db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                        db.SaveChanges();
                        // UPON ADDING STAFF - LOG EVENT -                          
                        var orgeventlog = new Org_Events_Log()
                        {
                            Org_Event_SubjectId = registeredUser.RegisteredUserId.ToString(),
                            Org_Event_SubjectName = registeredUser.FullName,
                            Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                            Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                            Org_Event_Time = DateTime.Now,
                            OrgId = Session["OrgId"].ToString(),
                            Org_Events_Types = Org_Events_Types.Registered_Staff
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
                        registeredUser.CountOfInvite = 0;
                        db.RegisteredUsers.Add(registeredUser);
                        db.SaveChanges();

                        //// CALL GENERATE KEY METHOD
                        //var generatekey = DependencyResolver.Current.GetService<RegisteredUsersController>();
                        //var result = generatekey.GenerateGuardianInviteKey(registeredUser.RegisteredUserId);

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
                            NurserySchoolUserRoleId = registeredUser.NurserySchoolUserRoleId,
                            EnrolmentDate = DateTime.Now,
                            CreatedBy = Session["RegisteredUserId"].ToString(),
                            FullName = registeredUser.FullName,
                            LastLogOn = null,
                            RegistrationFlags = RegistrationFlags.ManuallyAdded
                        };
                        db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                        db.SaveChanges();

                        //ADD GUARDIAN - INTO CLASS GROUP.                             
                        var studentclassref = db.RegisteredUsers.Where(x => x.RegisteredUserId == (int)registeredUser.TempIntHolder).Select(x => x.ClassRef).FirstOrDefault();
                        var orggrpref = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.OrgGroupId).FirstOrDefault();
                        var orggrptypeid = db.OrgGroups.Where(x => x.GroupRefNumb == studentclassref && x.OrgId == w).Select(x => x.GroupTypeId).FirstOrDefault();
                        var regusergrp = new RegisteredUsersGroups
                        {
                            RegisteredUserId = registeredUser.RegisteredUserId,
                            OrgGroupId = orggrpref,
                            Email = registeredUser.Email,
                            RegUserOrgId = w,
                            GroupTypeId = orggrptypeid,
                            LinkedStudentId = registeredUser.TempIntHolder
                        };
                        db.RegisteredUsersGroups.Add(regusergrp);
                        db.SaveChanges();



                        // UPDATE GROUP COUNT
                        var otherController = DependencyResolver.Current.GetService<RegisteredUsersGroupsController>();
                        var result2 = otherController.UpdateGroupMemberCount(orggrpref, w);




                        // UPDATE STUD'S GUARDIAN COUNT.
                        var studid = db.RegisteredUsers.Where(x => x.RegisteredUserId == registeredUser.TempIntHolder).Select(x => x.RegisteredUserId).FirstOrDefault();
                        var locatestud = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == studid).FirstOrDefault();
                        var currentcount = db.RegisteredUsers.Where(x => x.RegisteredUserId == registeredUser.TempIntHolder).Select(x => x.PgCount).FirstOrDefault();

                        // SET PG TO 0 IF NULL
                        if (currentcount == null)
                        {
                            var zero = 0;
                            currentcount = zero;
                        }
                        var studgaurd = new RegisteredUser
                        {
                            RegisteredUserId = locatestud.RegisteredUserId,
                            RegisteredUserTypeId = locatestud.RegisteredUserTypeId,
                            FirstName = locatestud.FirstName,
                            LastName = locatestud.LastName,
                            Email = locatestud.Email,
                            LoginErrorMsg = locatestud.LoginErrorMsg,
                            Password = locatestud.Password,
                            ConfirmPassword = locatestud.ConfirmPassword,
                            Telephone = locatestud.Telephone,
                            SelectedOrg = locatestud.SelectedOrg,
                            ClassId = locatestud.ClassId,
                            GenderId = locatestud.GenderId,
                            TribeId = locatestud.TribeId,
                            DateOfBirth = locatestud.DateOfBirth,
                            EnrolmentDate = locatestud.EnrolmentDate,
                            ReligionId = locatestud.ReligionId,
                            PrimarySchoolUserRoleId = locatestud.PrimarySchoolUserRoleId,
                            SecondarySchoolUserRoleId = locatestud.SecondarySchoolUserRoleId,
                            NurserySchoolUserRoleId = locatestud.NurserySchoolUserRoleId,
                            StudentRegFormId = locatestud.StudentRegFormId,
                            CreatedBy = locatestud.CreatedBy,
                            RegUserOrgBrand = locatestud.RegUserOrgBrand,
                            FullName = locatestud.FirstName + " " + locatestud.LastName,
                            IsTester = locatestud.IsTester,
                            TempIntHolder = locatestud.TempIntHolder,
                            TitleId = locatestud.TitleId,
                            RelationshipId = locatestud.RelationshipId,
                            ClassRef = locatestud.ClassRef,
                            PgCount = currentcount + 1,
                            InviteKey = locatestud.InviteKey,
                            InviteSentDate = locatestud.InviteSentDate,
                            CountOfInvite = locatestud.CountOfInvite,
                            IsRegistered = locatestud.IsRegistered,
                            RegisteredDate = locatestud.RegisteredDate,
                        };
                        locatestud = studgaurd;
                        db.Entry(studgaurd).State = EntityState.Modified;
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
                            OrgId = w,
                            Stu_class_Org_Grp_id = orggrpref,
                            IsRegistered = false
                        };
                        db.StudentGuardians.Add(studentguardian);
                        db.SaveChanges();
                        // UPON ADDING GUARDIAN - LOG EVENT -                             
                        var orgeventlog = new Org_Events_Log()
                        {
                            Org_Event_SubjectId = registeredUser.RegisteredUserId.ToString(),
                            Org_Event_SubjectName = registeredUser.FullName,
                            Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                            Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                            Org_Event_Time = DateTime.Now,
                            OrgId = Session["OrgId"].ToString(),
                            Org_Events_Types = Org_Events_Types.Registered_Guardian
                        };
                        db.Org_Events_Logs.Add(orgeventlog);
                        db.SaveChanges();
                        // THEN EXIT
                        return RedirectToAction("AllStudents", "RegisteredUsers");
                    }
                    // NEW STUDENT                // ADDING NEW STUDENT - USER IS A STUDENT  - THEN THIS CONDITION IS TRUE - WE GO IN.//
                    if (registeredUser.SelectedOrgList == null && registeredUser.StudentRegFormId != null)
                    {
                        var rr4 = Session["OrgId"].ToString();
                        int w4 = Convert.ToInt32(rr4);
                        var email = "student";
                        registeredUser.FirstName = registeredUser.FirstName;
                        registeredUser.OtherNames = registeredUser.OtherNames;
                        registeredUser.LastName = registeredUser.LastName;
                        registeredUser.Email = email;
                        registeredUser.SelectedOrg = w4;
                        registeredUser.FullName = registeredUser.ContactFullName;
                        registeredUser.RegisteredUserTypeId = 2;
                        registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                        registeredUser.EnrolmentDate = DateTime.Now;
                        registeredUser.DateOfBirth = registeredUser.DateOfBirth;
                        var regUserOrgBrand4 = db.Orgs.Where(x => x.OrgId == registeredUser.SelectedOrg).Select(x => x.OrgBrandId).FirstOrDefault();
                        int j5 = Convert.ToInt32(regUserOrgBrand4);
                        registeredUser.ClassRef = db.Classes.Where(x => x.ClassId == registeredUser.ClassId).Select(x => x.ClassRefNumb).FirstOrDefault();
                        registeredUser.RegUserOrgBrand = j5;
                        registeredUser.PgCount = 0;
                        var classid = registeredUser.ClassId;
                        var classref = registeredUser.ClassRef;
                        db.RegisteredUsers.Add(registeredUser);
                        db.SaveChanges();

                        // UPON ADDING STUDENT - ADD STUDENT TO REGUSERORG
                        var objRegisteredUserOrganisations = new RegisteredUserOrganisation()
                        {
                            RegisteredUserId = registeredUser.RegisteredUserId,
                            OrgId = w4,
                            Email = registeredUser.Email,
                            FirstName = registeredUser.FirstName,
                            OtherNames = registeredUser.OtherNames,
                            LastName = registeredUser.LastName,
                            OrgName = db.Orgs.Where(x => x.OrgId == w4).Select(x => x.OrgName).FirstOrDefault(),
                            RegUserOrgBrand = registeredUser.RegUserOrgBrand,
                            IsTester = registeredUser.IsTester,
                            RegisteredUserTypeId = registeredUser.RegisteredUserTypeId,
                            PrimarySchoolUserRoleId = registeredUser.PrimarySchoolUserRoleId,
                            SecondarySchoolUserRoleId = registeredUser.SecondarySchoolUserRoleId,
                            NurserySchoolUserRoleId = registeredUser.NurserySchoolUserRoleId,
                            EnrolmentDate = DateTime.Now,
                            CreatedBy = Session["RegisteredUserId"].ToString(),
                            FullName = registeredUser.FullName,
                            LastLogOn = null,
                            RegistrationFlags = RegistrationFlags.ManuallyAdded
                        };
                        db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                        db.SaveChanges();

                        var studid = registeredUser.RegisteredUserId;

                        // UPON ADDING STUDENT - LOG EVENT - 
                        var orgeventlog = new Org_Events_Log()
                        {
                            Org_Event_SubjectId = registeredUser.RegisteredUserId.ToString(),
                            Org_Event_SubjectName = registeredUser.FullName,
                            Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                            Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                            Org_Event_Time = DateTime.Now,
                            OrgId = w4.ToString(),
                            Org_Events_Types = Org_Events_Types.Registered_Student
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

                        // UPON ADDING STUDENT -  CREATE STUDENTS MODULES
                        var otherController = DependencyResolver.Current.GetService<StudentSubjectGradeController>();
                        var result = otherController.CreateStudentModules(classid, studid, classref, i6);

                        return RedirectToAction("AllStudents", "RegisteredUsers");
                        // THEN EXIT
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(registeredUser);
            }
            return View(registeredUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStaffRole(RegisteredUserOrganisation registeredUserOrganisation)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                if (!(ModelState.IsValid) || ModelState.IsValid)
                {
                    var locatestaff = db.RegisteredUserOrganisations.AsNoTracking()
                        .Where(x => x.RegisteredUserId == registeredUserOrganisation.RegisteredUserId)
                        .Where(x => x.OrgId == i)
                        .FirstOrDefault();
                    // ORG IS SECONDARY SCH
                    if ((int)Session["OrgType"] == 2)
                    {
                        // Update teachers role to non teaching staff if set as empty
                        if (registeredUserOrganisation.SecondarySchoolUserRoleId == null)
                        {
                            // ORG IS SECONDARY SCH
                            if ((int)Session["OrgType"] == 2)
                            {
                                registeredUserOrganisation.SecondarySchoolUserRoleId = 6;
                            }
                        }
                        else
                        {
                            locatestaff.SecondarySchoolUserRoleId = registeredUserOrganisation.SecondarySchoolUserRoleId;
                        }
                        var secstaff = new RegisteredUserOrganisation
                        {
                            RegisteredUserOrganisationId = locatestaff.RegisteredUserOrganisationId,
                            RegisteredUserId = locatestaff.RegisteredUserId,
                            OrgId = locatestaff.OrgId,
                            Email = locatestaff.Email,
                            FirstName = locatestaff.FirstName,
                            LastName = locatestaff.LastName,
                            OrgName = locatestaff.OrgName,
                            RegUserOrgBrand = locatestaff.RegUserOrgBrand,
                            IsTester = locatestaff.IsTester,
                            RegisteredUserTypeId = locatestaff.RegisteredUserTypeId,
                            PrimarySchoolUserRoleId = locatestaff.PrimarySchoolUserRoleId,
                            SecondarySchoolUserRoleId = registeredUserOrganisation.SecondarySchoolUserRoleId,
                            NurserySchoolUserRoleId = locatestaff.NurserySchoolUserRoleId,
                            EnrolmentDate = locatestaff.EnrolmentDate,
                            CreatedBy = locatestaff.CreatedBy,
                            FullName = locatestaff.FullName,
                            TitleId = locatestaff.TitleId,
                            LastLogOn = locatestaff.LastLogOn,
                            RegistrationFlags = locatestaff.RegistrationFlags,
                        };
                        locatestaff = secstaff;
                        db.Entry(locatestaff).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    // ORG IS PRIMARY SCH
                    if ((int)Session["OrgType"] == 3)
                    {
                        // Update teachers role to non teaching staff if set as empty
                        if (registeredUserOrganisation.PrimarySchoolUserRoleId == null)
                        {
                            // ORG IS PRIMARY SCH
                            if ((int)Session["OrgType"] == 3)
                            {
                                registeredUserOrganisation.PrimarySchoolUserRoleId = 6;
                            }
                        }
                        else
                        {
                            locatestaff.PrimarySchoolUserRoleId = registeredUserOrganisation.PrimarySchoolUserRoleId;
                        }
                        var pristaff = new RegisteredUserOrganisation
                        {
                            RegisteredUserOrganisationId = locatestaff.RegisteredUserOrganisationId,
                            RegisteredUserId = locatestaff.RegisteredUserId,
                            OrgId = locatestaff.OrgId,
                            Email = locatestaff.Email,
                            FirstName = locatestaff.FirstName,
                            LastName = locatestaff.LastName,
                            OrgName = locatestaff.OrgName,
                            RegUserOrgBrand = locatestaff.RegUserOrgBrand,
                            IsTester = locatestaff.IsTester,
                            RegisteredUserTypeId = locatestaff.RegisteredUserTypeId,
                            PrimarySchoolUserRoleId = registeredUserOrganisation.PrimarySchoolUserRoleId,
                            SecondarySchoolUserRoleId = locatestaff.SecondarySchoolUserRoleId,
                            NurserySchoolUserRoleId = locatestaff.NurserySchoolUserRoleId,
                            EnrolmentDate = locatestaff.EnrolmentDate,
                            CreatedBy = locatestaff.CreatedBy,
                            FullName = locatestaff.FullName,
                            TitleId = locatestaff.TitleId,
                            LastLogOn = locatestaff.LastLogOn,
                            RegistrationFlags = locatestaff.RegistrationFlags,
                        };
                        locatestaff = pristaff;
                        db.Entry(locatestaff).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    // ORG IS NURSERY SCH
                    if ((int)Session["OrgType"] == 4)
                    {
                        // Update teachers role to non teaching staff if set as empty
                        if (registeredUserOrganisation.NurserySchoolUserRoleId == null)
                        {
                            // ORG IS SECONDARY SCH
                            if ((int)Session["OrgType"] == 4)
                            {
                                registeredUserOrganisation.NurserySchoolUserRoleId = 6;
                            }
                        }
                        else
                        {
                            locatestaff.SecondarySchoolUserRoleId = registeredUserOrganisation.SecondarySchoolUserRoleId;
                        }
                        var pristaff = new RegisteredUserOrganisation
                        {
                            RegisteredUserOrganisationId = locatestaff.RegisteredUserOrganisationId,
                            RegisteredUserId = locatestaff.RegisteredUserId,
                            OrgId = locatestaff.OrgId,
                            Email = locatestaff.Email,
                            FirstName = locatestaff.FirstName,
                            LastName = locatestaff.LastName,
                            OrgName = locatestaff.OrgName,
                            RegUserOrgBrand = locatestaff.RegUserOrgBrand,
                            IsTester = locatestaff.IsTester,
                            RegisteredUserTypeId = locatestaff.RegisteredUserTypeId,
                            PrimarySchoolUserRoleId = locatestaff.PrimarySchoolUserRoleId,
                            SecondarySchoolUserRoleId = locatestaff.SecondarySchoolUserRoleId,
                            NurserySchoolUserRoleId = registeredUserOrganisation.NurserySchoolUserRoleId,
                            EnrolmentDate = locatestaff.EnrolmentDate,
                            CreatedBy = locatestaff.CreatedBy,
                            FullName = locatestaff.FullName,
                            TitleId = locatestaff.TitleId,
                            LastLogOn = locatestaff.LastLogOn,
                            RegistrationFlags = locatestaff.RegistrationFlags,
                        };
                        locatestaff = pristaff;
                        db.Entry(locatestaff).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Staffs", "RegisteredUsers");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }


            return RedirectToAction("Staffs", "RegisteredUsers");
        }
        // POST: RegisteredUsers/ChangeStudentsClass/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStudentsClass(RegisteredUser registeredUser)
        {
            try
            {
                if (!(ModelState.IsValid) || ModelState.IsValid)
                {
                    var orgid = (int)Session["OrgId"];

                    var locatestud = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == registeredUser.RegisteredUserId).FirstOrDefault();
                    var classref = db.Classes.Where(x => x.ClassId == registeredUser.ClassId).Select(x => x.ClassRefNumb).FirstOrDefault();
                    registeredUser.ClassRef = classref;
                    var studs = new RegisteredUser
                    {
                        RegisteredUserId = locatestud.RegisteredUserId,
                        RegisteredUserTypeId = locatestud.RegisteredUserTypeId,
                        FirstName = registeredUser.FirstName,
                        OtherNames = registeredUser.OtherNames,
                        LastName = registeredUser.LastName,
                        Email = locatestud.Email,
                        SelectedOrg = locatestud.SelectedOrg,
                        ClassId = registeredUser.ClassId,
                        GenderId = locatestud.GenderId,
                        TribeId = locatestud.TribeId,
                        DateOfBirth = locatestud.DateOfBirth,
                        EnrolmentDate = locatestud.EnrolmentDate,
                        ReligionId = locatestud.ReligionId,
                        StudentRegFormId = locatestud.StudentRegFormId,
                        CreatedBy = locatestud.CreatedBy,
                        RegUserOrgBrand = locatestud.RegUserOrgBrand,
                        FullName = registeredUser.FirstName + " " + registeredUser.OtherNames + " " + registeredUser.LastName,
                        ClassRef = classref,
                        PgCount = locatestud.PgCount,
                    };
                    locatestud = studs;
                    db.Entry(locatestud).State = EntityState.Modified;
                    db.SaveChanges();


                    //CHECK IF STUD HAS ANY GRADES IN CURRENT CLASS
                    var checkgrades = db.StudentSubjectGrades.Where(x => x.RegisteredUserId == locatestud.RegisteredUserId).Count();

                    // IF GRADES COUNT > 0, LOG THE GRADES IN Students_Grades_LogController
                    if (checkgrades > 0)
                    {
                        // GET LIST OF GRADES
                        var grades = db.StudentSubjectGrades
                                .Where(x => x.RegisteredUserId == locatestud.RegisteredUserId)
                                .Where(x => x.OrgId == orgid)
                                .Select(x => x.StudentSubjectGradeId)
                                .ToList();
                        var listofsubjects = new List<int>(grades);

                        foreach (var grade in grades)
                        {
                            var currentgrade = db.StudentSubjectGrades
                                .Where(x => x.StudentSubjectGradeId == grade)
                                .Where(x => x.OrgId == orgid)
                                .FirstOrDefault();

                            var currentsubject = db.Subjects
                                .Where(x => x.SubjectId == currentgrade.SubjectId)
                                .Where(x => x.SubjectOrgId == orgid)
                                .FirstOrDefault();

                            var gradeslog = new Students_Grades_Log
                            {
                                RegisteredUserId = locatestud.RegisteredUserId,
                                SubjectId = currentgrade.SubjectId,
                                ClassId = (int)currentgrade.ClassId,
                                ClassRef = (int)currentgrade.ClassRef,
                                OrgId = (int)currentgrade.OrgId,
                                FirstTerm_ExamGrade = currentgrade.FirstTerm_ExamGrade,
                                SecondTerm_ExamGrade = currentgrade.SecondTerm_ExamGrade,
                                ThirdTerm_ExamGrade = currentgrade.ThirdTerm_ExamGrade,
                                FirstTerm_TestGrade = currentgrade.FirstTerm_TestGrade,
                                SecondTerm_TestGrade = currentgrade.SecondTerm_TestGrade,
                                ThirdTerm_TestGrade = currentgrade.ThirdTerm_TestGrade,
                                Last_updated_date = currentgrade.Last_updated_date,
                                First_Term_Exam_MaxGrade = currentsubject.First_Term_Exam_MaxGrade,
                                Second_Term_Exam_MaxGrade = currentsubject.Second_Term_Exam_MaxGrade,
                                Third_Term_Exam_MaxGrade = currentsubject.Third_Term_Exam_MaxGrade,
                                First_Term_Test_MaxGrade = currentsubject.First_Term_Test_MaxGrade,
                                Second_Term_Test_MaxGrade = currentsubject.Second_Term_Test_MaxGrade,
                                Third_Term_Test_MaxGrade = currentsubject.Third_Term_Test_MaxGrade,
                                Subject_Min_Passmark = currentsubject.Subject_Min_Passmark,
                                Subject_Max_Passmark = currentsubject.SubjectMaxGrade,
                                Updater_Id = (int)currentgrade.Updater_Id,
                                StudentClassChangeType = StudentClassChangeType.Change_of_class,
                                ClassTeacherId = currentsubject.ClassTeacherId,
                                Created_date = DateTime.Now,

                            };
                            db.Students_Grades_Logs.Add(gradeslog);
                            db.SaveChanges();

                            // REMOVE RECORD FROM STUSUBGRADS TABLE 
                            db.StudentSubjectGrades.Remove(currentgrade);
                            db.SaveChanges();
                        }
                    }

                    // CALL METHOD TO CREATE SUBJECTS FOR NEW CLASS
                    // UPON ADDING STUDENT -  CREATE STUDENTS MODULES
                    var otherController = DependencyResolver.Current.GetService<StudentSubjectGradeController>();
                    var result = otherController.CreateStudentModules(registeredUser.ClassId, locatestud.RegisteredUserId, classref, orgid);

                    //UPDATE CLASS DATA
                    var getclassid = db.Classes.AsNoTracking().Where(x => x.ClassId == registeredUser.ClassId).FirstOrDefault();
                    var studentcount = db.RegisteredUsers.Where(x => x.ClassId == registeredUser.ClassId && x.SelectedOrg == orgid).Count();
                    var FemStuCount = db.RegisteredUsers.Where(x => x.ClassId == registeredUser.ClassId && x.GenderId == 2 && x.SelectedOrg == orgid).Count();
                    var MaleStudCount = db.RegisteredUsers.Where(x => x.ClassId == registeredUser.ClassId && x.GenderId == 1 && x.SelectedOrg == orgid).Count();
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

                    // UPON CHANGING STUDENT'S CLASS - LOG THE EVENT 
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_SubjectId = locatestud.RegisteredUserId.ToString(),
                        Org_Event_SubjectName = registeredUser.FirstName + " " + registeredUser.LastName,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString(),
                        Org_Events_Types = Org_Events_Types.Changed_Class
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();

                }
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                if (registeredUser.StudentRegFormId == 1)
                {
                    var classref = db.Classes.Where(x => x.ClassId == registeredUser.ClassId).Select(x => x.ClassRefNumb).FirstOrDefault();
                    registeredUser.ClassRef = classref;
                    var linkedguardiancount = db.RegisteredUsersGroups
                        .Where(x => x.LinkedStudentId == registeredUser.RegisteredUserId)
                        .Where(x => x.RegUserOrgId == i)
                        .Select(x => x.RegisteredUsersGroupsId).Count();
                    if (linkedguardiancount > 0)
                    {
                        var linkedguardianlist = db.RegisteredUsersGroups
                        .Where(x => x.LinkedStudentId == registeredUser.RegisteredUserId)
                        .Where(x => x.RegUserOrgId == i)
                        .Select(x => x.RegisteredUsersGroupsId).ToList();
                        var linkedguardian = new List<int>(linkedguardianlist);
                        foreach (var gd in linkedguardianlist)
                        {
                            var locatenewgrpid = db.OrgGroups
                                .Where(x => x.OrgId == i)
                                .Where(x => x.GroupRefNumb == classref)
                                .Select(x => x.OrgGroupId).FirstOrDefault();
                            var locatestud = db.RegisteredUsersGroups.AsNoTracking().Where(x => x.RegisteredUsersGroupsId == gd).FirstOrDefault();
                            var updaterecrd = new RegisteredUsersGroups
                            {
                                RegisteredUsersGroupsId = locatestud.RegisteredUsersGroupsId,
                                RegisteredUserId = locatestud.RegisteredUserId,
                                OrgGroupId = locatenewgrpid,
                                Email = locatestud.Email,
                                RegUserOrgId = locatestud.RegUserOrgId,
                                GroupTypeId = locatestud.GroupTypeId,
                                LinkedStudentId = locatestud.LinkedStudentId
                            };
                            locatestud = updaterecrd;
                            db.Entry(updaterecrd).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        // LOOP THROUGH GROUPS IN ORG AND UPDATE COUNT             
                        var orggrp = db.OrgGroups.Where(x => x.OrgId == i).Select(x => x.OrgGroupId).ToList();
                        var grplist = new List<int>(orggrp);
                        foreach (var grp in grplist)
                        {
                            //UPDATE GROUP COUNT 
                            var otherController = DependencyResolver.Current.GetService<RegisteredUsersGroupsController>();
                            var result = otherController.UpdateGroupMemberCount(grp, i);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

            return RedirectToAction("AllStudents");
        }


        // POST: RegisteredUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisteredUser registeredUser)
        {
            try
            {
                if (!(ModelState.IsValid) || ModelState.IsValid)
                {
                    var rr = Session["OrgId"].ToString();
                    int i = Convert.ToInt32(rr);
                    var locatestud = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == registeredUser.RegisteredUserId).FirstOrDefault();
                    var studs = new RegisteredUser
                    {
                        RegisteredUserId = locatestud.RegisteredUserId,
                        RegisteredUserTypeId = locatestud.RegisteredUserTypeId,
                        TitleId = registeredUser.TitleId,
                        FirstName = registeredUser.FirstName,
                        OtherNames = registeredUser.OtherNames,
                        LastName = registeredUser.LastName,
                        Password = locatestud.Password,
                        ConfirmPassword = locatestud.ConfirmPassword,
                        Telephone = locatestud.Telephone,
                        Email = locatestud.Email,
                        SelectedOrg = locatestud.SelectedOrg,
                        ClassId = locatestud.ClassId,
                        GenderId = registeredUser.GenderId,
                        TribeId = registeredUser.TribeId,
                        DateOfBirth = registeredUser.DateOfBirth,
                        EnrolmentDate = locatestud.EnrolmentDate,
                        ReligionId = registeredUser.ReligionId,
                        StudentRegFormId = locatestud.StudentRegFormId,
                        CreatedBy = locatestud.CreatedBy,
                        RegUserOrgBrand = locatestud.RegUserOrgBrand,
                        FullName = registeredUser.FirstName + " " + registeredUser.OtherNames + " " + registeredUser.LastName,
                        ClassRef = locatestud.ClassRef,
                        PgCount = locatestud.PgCount,
                        InviteKey = locatestud.InviteKey,
                        InviteSentDate = locatestud.InviteSentDate,
                        CountOfInvite = locatestud.CountOfInvite,
                        IsRegistered = locatestud.IsRegistered,
                        RegisteredDate = locatestud.RegisteredDate,
                    };
                    locatestud = studs;
                    db.Entry(locatestud).State = EntityState.Modified;
                    db.SaveChanges();

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
                            EnrolmentDate = getid.EnrolmentDate,
                            CreatedBy = getid.CreatedBy,
                            Email = registeredUser.Email,
                            FirstName = registeredUser.FirstName,
                            OtherNames = registeredUser.OtherNames,
                            LastName = registeredUser.LastName,
                            FullName = registeredUser.FirstName + " " + registeredUser.OtherNames + " " + registeredUser.LastName,
                            TitleId = registeredUser.TitleId,
                            LastLogOn = getid.LastLogOn,
                            PrimarySchoolUserRoleId = getid.PrimarySchoolUserRoleId,
                            SecondarySchoolUserRoleId = getid.SecondarySchoolUserRoleId,
                            NurserySchoolUserRoleId = getid.NurserySchoolUserRoleId,
                            RegistrationFlags = getid.RegistrationFlags
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
                        foreach (var te in teacher)
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
                        var cid = locatestud.ClassId;
                        var updateclasses = UpdateClassProfile((int)cid);
                    }
                    return RedirectToAction("AllStudents", "RegisteredUsers");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return RedirectToAction("AllStudents", "RegisteredUsers");
        }


        //Update Class profile.
        public ActionResult UpdateClassProfile(int cid)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var classid = db.Classes.AsNoTracking().Where(x => x.OrgId == i && x.ClassId == cid).FirstOrDefault();
                var studentcount = db.RegisteredUsers.Where(x => x.ClassId == cid && x.SelectedOrg == i).Count();
                var FemStuCount = db.RegisteredUsers.Where(x => x.ClassId == cid && x.GenderId == 2 && x.SelectedOrg == i).Count();
                var MaleStudCount = db.RegisteredUsers.Where(x => x.ClassId == cid && x.GenderId == 1 && x.SelectedOrg == i).Count();
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

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return RedirectToAction("Index");
        }


        // POST: RegisteredUsers/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                // CHECK IF USER BEING DELETED IS A STAFF = IF YES - WE GO INTO THIS CONDITION - 
                // LOCATE USERS ROLES
                var chkifPsStaff = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == id).Select(x => x.PrimarySchoolUserRoleId).FirstOrDefault();
                var chkifSsStaff = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == id).Select(x => x.SecondarySchoolUserRoleId).FirstOrDefault();
                var chkifNsStaff = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == id).Select(x => x.NurserySchoolUserRoleId).FirstOrDefault();

                if (chkifPsStaff == 1 || chkifPsStaff == 2 || chkifPsStaff == 3 || chkifPsStaff == 4 || chkifPsStaff == 6 ||
                    chkifSsStaff == 1 || chkifSsStaff == 2 || chkifSsStaff == 3 || chkifSsStaff == 4 || chkifSsStaff == 6 ||
                    chkifNsStaff == 1 || chkifNsStaff == 2 || chkifNsStaff == 3 || chkifNsStaff == 4 || chkifNsStaff == 6)
                {
                    var staforgcount = db.RegisteredUserOrganisations
                        .Where(x => x.RegisteredUserId == id)
                        .Select(x => x.RegisteredUserId)
                        .Count();

                    // IF COUNT OF ORG IS 1 - WE GO INTO THIS CONDITION - WE DELETE USER FROM REGUSER TABLE / LOG EVENT AND MOVE ON.
                    if (staforgcount == 1)
                    {
                        // GET STAFF'S DATA
                        var staffdataRu = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).FirstOrDefault();
                        var staffdataRug = db.RegisteredUserOrganisations
                            .Where(x => x.RegisteredUserId == id)
                            .Where(x => x.OrgId == i)
                            .FirstOrDefault();

                        // BEFORE REMVING STAFF - LOG EVENT. 
                        var orgeventlog = new Org_Events_Log()
                        {
                            Org_Event_SubjectId = id.ToString(),
                            Org_Event_SubjectName = staffdataRu.FullName,
                            Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                            Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                            Org_Event_Time = DateTime.Now,
                            OrgId = Session["OrgId"].ToString(),
                            Org_Events_Types = Org_Events_Types.Deregistered_Staff
                        };
                        db.Org_Events_Logs.Add(orgeventlog);
                        db.SaveChanges();


                        // CHECK IF TEACHER AND UN-ASSIGN FOR LINKED CLASSES  
                        var assignedclasscount = db.Classes
                            .Where(x => x.ClassTeacherId == id)
                            .Where(x => x.OrgId == i)
                            .Count();

                        // CHECK IF TEACHER AND UN-ASSIGN FROM LINKED SUBJECTS  
                        var assignedsubjectcount = db.Subjects
                            .Where(x => x.ClassTeacherId == id)
                            .Where(x => x.SubjectOrgId == i)
                            .Count();

                        if (assignedclasscount > 0)
                        {
                            var assignedclasses = db.Classes
                                .Where(x => x.ClassTeacherId == id)
                                .Where(x => x.OrgId == i).Select(x => x.ClassId)
                                .ToList();

                            var classlist = new List<int>(assignedclasses);

                            foreach (var cl in classlist)
                            {
                                var currentclass = db.Classes.AsNoTracking().Where(x => x.ClassId == cl && x.OrgId == i).FirstOrDefault();

                                var updaterecrds = new Class
                                {
                                    ClassId = currentclass.ClassId,
                                    ClassName = currentclass.ClassName,
                                    ClassIsActive = currentclass.ClassIsActive,
                                    OrgId = currentclass.OrgId,
                                    ClassRefNumb = currentclass.ClassRefNumb,
                                    ClassTeacherId = null,
                                    ClassTeacherFullName = null,
                                    Students_Count = currentclass.Students_Count,
                                    Female_Students_Count = currentclass.Female_Students_Count,
                                    Male_Students_Count = currentclass.Male_Students_Count,
                                    TitleId = null
                                };
                                currentclass = updaterecrds;
                                db.Entry(currentclass).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }

                        if (assignedsubjectcount > 0)
                        {
                            var assignedsubs = db.Subjects
                                .Where(x => x.ClassTeacherId == id)
                                .Where(x => x.SubjectOrgId == i).Select(x => x.SubjectId)
                                .ToList();

                            var subjlist = new List<int>(assignedsubs);

                            foreach (var sub in subjlist)
                            {
                                var currentsubj = db.Subjects.AsNoTracking().Where(x => x.SubjectId == sub && x.SubjectOrgId == i).FirstOrDefault();

                                var updaterecrds = new Subject
                                {
                                    SubjectId = currentsubj.SubjectId,
                                    SubjectName = currentsubj.SubjectName,
                                    ClassId = currentsubj.ClassId,
                                    ClassTeacherId = null,
                                    TaughtBy = null,
                                    SubjectOrgId = currentsubj.SubjectOrgId,
                                    First_Term_Test_MaxGrade = currentsubj.First_Term_Test_MaxGrade,
                                    Second_Term_Test_MaxGrade = currentsubj.Second_Term_Test_MaxGrade,
                                    Third_Term_Test_MaxGrade = currentsubj.Third_Term_Test_MaxGrade,
                                    First_Term_Exam_MaxGrade = currentsubj.First_Term_Exam_MaxGrade,
                                    Second_Term_Exam_MaxGrade = currentsubj.Second_Term_Exam_MaxGrade,
                                    Third_Term_Exam_MaxGrade = currentsubj.Third_Term_Exam_MaxGrade,
                                    Subject_Min_Passmark = currentsubj.Subject_Min_Passmark,
                                    Created_date = currentsubj.Created_date,
                                    Creator_Id = currentsubj.Creator_Id,
                                };
                                currentsubj = updaterecrds;
                                db.Entry(currentsubj).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }


                        // SOFT DELETE USER
                        var remvdstaff = new RemovedRegisteredUser
                        {
                            RegisteredUserId = staffdataRu.RegisteredUserId,
                            CreationDate = DateTime.Now,
                            FirstName = staffdataRu.FirstName,
                            LastName = staffdataRu.LastName,
                            FullName = staffdataRu.FullName,
                            Email = staffdataRu.Email,
                            Telephone = staffdataRu.Telephone,
                            RegisteredUserType = staffdataRu.RegisteredUserTypeId,
                            PrimarySchoolUserRole = staffdataRug.PrimarySchoolUserRoleId.GetValueOrDefault(),
                            SecondarySchoolUserRole = staffdataRug.SecondarySchoolUserRoleId.GetValueOrDefault(),
                            NurserySchoolUserRole = staffdataRug.NurserySchoolUserRoleId.GetValueOrDefault(),
                            OrgId = staffdataRug.OrgId,
                            ClassId = staffdataRu.ClassId.GetValueOrDefault(),
                            ClassRef = staffdataRu.ClassRef.GetValueOrDefault(),
                            GenderId = staffdataRu.GenderId.GetValueOrDefault(),
                            ReligionId = staffdataRu.ReligionId.GetValueOrDefault(),
                            RelationshipId = staffdataRu.RelationshipId,
                            StudentRegFormId = staffdataRu.StudentRegFormId.GetValueOrDefault(),
                            IsTester = (bool)staffdataRu.IsTester.GetValueOrDefault(),
                            DateOfBirth = staffdataRu.DateOfBirth,
                            EnrolmentDate = staffdataRug.EnrolmentDate.GetValueOrDefault(),
                            LastLogOn = staffdataRug.LastLogOn,
                            EnrolledBy = Convert.ToInt32(staffdataRug.CreatedBy)
                        };
                        db.RemovedRegisteredUsers.Add(remvdstaff);
                        db.SaveChanges();


                        RegisteredUser removestaff = db.RegisteredUsers.Find(id);
                        db.RegisteredUsers.Remove(removestaff);
                        db.SaveChanges();
                        return RedirectToAction("Staffs");
                    }
                    // GET A LIST OF ORGS STAFF IS LINKED TO - LOOP THRU - AND DELETE FROM REGUSERORG TABLE AND LOG EVENT ONCE ON ORG THAT IS SAME AS ACTIVE SESSION.
                    else
                    {
                        var stafsorgs = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == id).Select(x => x.OrgId).ToList();
                        var linkedorgs = new List<int>(stafsorgs);

                        foreach (var ruo in stafsorgs)
                        {
                            if (ruo == i)
                            {
                                var getstaff = db.RegisteredUserOrganisations
                                    .Where(x => x.RegisteredUserId == id)
                                    .Where(x => x.OrgId == i)
                                    .Select(x => x.RegisteredUserOrganisationId)
                                    .FirstOrDefault();

                                // GET STAFF'S DATA
                                var staffdataRu = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).FirstOrDefault();
                                var staffdataRug = db.RegisteredUserOrganisations
                                    .Where(x => x.RegisteredUserId == id)
                                    .Where(x => x.OrgId == i)
                                    .FirstOrDefault();

                                // CHECK IF TEACHER AND UN-ASSIGN FOR LINKED CLASSES  
                                var assignedclasscount = db.Classes
                                    .Where(x => x.ClassTeacherId == id)
                                    .Where(x => x.OrgId == i)
                                    .Count();

                                // CHECK IF TEACHER AND UN-ASSIGN FROM LINKED SUBJECTS  
                                var assignedsubjectcount = db.Subjects
                                    .Where(x => x.ClassTeacherId == id)
                                    .Where(x => x.SubjectOrgId == i)
                                    .Count();

                                if (assignedclasscount > 0)
                                {
                                    var assignedclasses = db.Classes
                                        .Where(x => x.ClassTeacherId == id)
                                        .Where(x => x.OrgId == i).Select(x => x.ClassId)
                                        .ToList();

                                    var classlist = new List<int>(assignedclasses);

                                    foreach (var cl in classlist)
                                    {
                                        var currentclass = db.Classes.AsNoTracking().Where(x => x.ClassId == cl && x.OrgId == i).FirstOrDefault();

                                        var updaterecrds = new Class
                                        {
                                            ClassId = currentclass.ClassId,
                                            ClassName = currentclass.ClassName,
                                            ClassIsActive = currentclass.ClassIsActive,
                                            OrgId = currentclass.OrgId,
                                            ClassRefNumb = currentclass.ClassRefNumb,
                                            ClassTeacherId = null,
                                            ClassTeacherFullName = null,
                                            Students_Count = currentclass.Students_Count,
                                            Female_Students_Count = currentclass.Female_Students_Count,
                                            Male_Students_Count = currentclass.Male_Students_Count,
                                            TitleId = null
                                        };
                                        currentclass = updaterecrds;
                                        db.Entry(currentclass).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                }

                                if (assignedsubjectcount > 0)
                                {
                                    var assignedsubs = db.Subjects
                                        .Where(x => x.ClassTeacherId == id)
                                        .Where(x => x.SubjectOrgId == i).Select(x => x.SubjectId)
                                        .ToList();

                                    var subjlist = new List<int>(assignedsubs);

                                    foreach (var sub in subjlist)
                                    {
                                        var currentsubj = db.Subjects.AsNoTracking().Where(x => x.SubjectId == sub && x.SubjectOrgId == i).FirstOrDefault();

                                        var updaterecrds = new Subject
                                        {
                                            SubjectId = currentsubj.SubjectId,
                                            SubjectName = currentsubj.SubjectName,
                                            ClassId = currentsubj.ClassId,
                                            ClassTeacherId = null,
                                            TaughtBy = null,
                                            SubjectOrgId = currentsubj.SubjectOrgId,
                                            First_Term_Test_MaxGrade = currentsubj.First_Term_Test_MaxGrade,
                                            Second_Term_Test_MaxGrade = currentsubj.Second_Term_Test_MaxGrade,
                                            Third_Term_Test_MaxGrade = currentsubj.Third_Term_Test_MaxGrade,
                                            First_Term_Exam_MaxGrade = currentsubj.First_Term_Exam_MaxGrade,
                                            Second_Term_Exam_MaxGrade = currentsubj.Second_Term_Exam_MaxGrade,
                                            Third_Term_Exam_MaxGrade = currentsubj.Third_Term_Exam_MaxGrade,
                                            Subject_Min_Passmark = currentsubj.Subject_Min_Passmark,
                                            Created_date = currentsubj.Created_date,
                                            Creator_Id = currentsubj.Creator_Id,
                                        };
                                        currentsubj = updaterecrds;
                                        db.Entry(currentsubj).State = EntityState.Modified;
                                        db.SaveChanges();
                                    }
                                }

                                // SOFT DELETE USER
                                var remvdstaff = new RemovedRegisteredUser
                                {
                                    RegisteredUserId = staffdataRu.RegisteredUserId,
                                    CreationDate = DateTime.Now,
                                    FirstName = staffdataRu.FirstName,
                                    LastName = staffdataRu.LastName,
                                    FullName = staffdataRu.FullName,
                                    Email = staffdataRu.Email,
                                    Telephone = staffdataRu.Telephone,
                                    RegisteredUserType = staffdataRu.RegisteredUserTypeId,
                                    PrimarySchoolUserRole = staffdataRug.PrimarySchoolUserRoleId.GetValueOrDefault(),
                                    SecondarySchoolUserRole = staffdataRug.SecondarySchoolUserRoleId.GetValueOrDefault(),
                                    NurserySchoolUserRole = staffdataRug.NurserySchoolUserRoleId.GetValueOrDefault(),
                                    OrgId = staffdataRug.OrgId,
                                    ClassId = staffdataRu.ClassId.GetValueOrDefault(),
                                    ClassRef = staffdataRu.ClassRef.GetValueOrDefault(),
                                    GenderId = staffdataRu.GenderId.GetValueOrDefault(),
                                    ReligionId = staffdataRu.ReligionId.GetValueOrDefault(),
                                    StudentRegFormId = staffdataRu.StudentRegFormId.GetValueOrDefault(),
                                    RelationshipId = staffdataRu.RelationshipId.GetValueOrDefault(),
                                    IsTester = (bool)staffdataRu.IsTester.GetValueOrDefault(),
                                    DateOfBirth = staffdataRu.DateOfBirth,
                                    LastLogOn = staffdataRug.LastLogOn,
                                    EnrolmentDate = staffdataRug.EnrolmentDate.GetValueOrDefault(),
                                    EnrolledBy = Convert.ToInt32(staffdataRug.CreatedBy)
                                };
                                db.RemovedRegisteredUsers.Add(remvdstaff);
                                staffdataRu.DateOfBirth = null;
                                db.SaveChanges();


                                RegisteredUserOrganisation removestaff = db.RegisteredUserOrganisations.Find(getstaff);
                                db.RegisteredUserOrganisations.Remove(removestaff);
                                db.SaveChanges();
                                // GET STAFF'S FULLNAME
                                var stafullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).Select(x => x.FullName).FirstOrDefault();
                                // UPON REMVING STAFF - LOG EVENT. 
                                var orgeventlog = new Org_Events_Log()
                                {
                                    Org_Event_SubjectId = id.ToString(),
                                    Org_Event_SubjectName = stafullname,
                                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                    Org_Event_Time = DateTime.Now,
                                    OrgId = Session["OrgId"].ToString(),
                                    Org_Events_Types = Org_Events_Types.Deregistered_Staff
                                };
                                db.Org_Events_Logs.Add(orgeventlog);
                                db.SaveChanges();
                                return RedirectToAction("Staffs");
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
                                var locateguard = db.StudentGuardians.Where(x => x.StudentGuardianId == gd).FirstOrDefault();
                                //var guardfullname = db.StudentGuardians.Where(x => x.StudentGuardianId == gd).Select(x => x.GuardianFullName).FirstOrDefault();

                                // SOFT DELETE USER
                                // GET USER'S DATA
                                var guarddataRu = db.RegisteredUsers.Where(x => x.RegisteredUserId == locateguard.RegisteredUserId).FirstOrDefault();
                                var guarddataRug = db.RegisteredUserOrganisations
                                    .Where(x => x.RegisteredUserId == locateguard.RegisteredUserId)
                                    .Where(x => x.OrgId == i)
                                    .FirstOrDefault();
                                var remvguard = new RemovedRegisteredUser
                                {
                                    RegisteredUserId = guarddataRu.RegisteredUserId,
                                    CreationDate = DateTime.Now,
                                    FirstName = guarddataRu.FirstName,
                                    LastName = guarddataRu.LastName,
                                    FullName = guarddataRu.FullName,
                                    Email = guarddataRu.Email,
                                    Telephone = guarddataRu.Telephone,
                                    RegisteredUserType = guarddataRu.RegisteredUserTypeId,
                                    PrimarySchoolUserRole = guarddataRug.PrimarySchoolUserRoleId.GetValueOrDefault(),
                                    SecondarySchoolUserRole = guarddataRug.SecondarySchoolUserRoleId.GetValueOrDefault(),
                                    NurserySchoolUserRole = guarddataRug.NurserySchoolUserRoleId.GetValueOrDefault(),
                                    OrgId = guarddataRug.OrgId,
                                    ClassId = guarddataRu.ClassId.GetValueOrDefault(),
                                    ClassRef = guarddataRu.ClassRef.GetValueOrDefault(),
                                    GenderId = guarddataRu.GenderId.GetValueOrDefault(),
                                    ReligionId = guarddataRu.ReligionId.GetValueOrDefault(),
                                    StudentRegFormId = guarddataRu.StudentRegFormId.GetValueOrDefault(),
                                    RelationshipId = locateguard.RelationshipId,
                                    IsTester = (bool)guarddataRu.IsTester.GetValueOrDefault(),
                                    DateOfBirth = guarddataRu.DateOfBirth,
                                    LastLogOn = guarddataRug.LastLogOn,
                                    EnrolmentDate = guarddataRug.EnrolmentDate.GetValueOrDefault(),
                                    EnrolledBy = Convert.ToInt32(guarddataRug.CreatedBy)
                                };
                                db.RemovedRegisteredUsers.Add(remvguard);
                                db.SaveChanges();


                                //NOW DELETE GUARDIAN FROM REG USER TABLE.
                                RegisteredUser remvguar = db.RegisteredUsers.Find(locateguard.RegisteredUserId);
                                db.RegisteredUsers.Remove(remvguar);
                                db.SaveChanges();


                                // LOOP THROUGH GROUPS IN ORG AND UPDATE COUNT
                                var myorgid = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == id).Select(x => x.OrgId).FirstOrDefault();
                                var orggrp = db.OrgGroups.Where(x => x.OrgId == myorgid).Select(x => x.OrgGroupId).ToList();
                                var grplist = new List<int>(orggrp);
                                foreach (var grp in grplist)
                                {
                                    //UPDATE GROUP COUNT 
                                    var otherController = DependencyResolver.Current.GetService<RegisteredUsersGroupsController>();
                                    var result = otherController.UpdateGroupMemberCount(grp, myorgid);
                                }
                                // UPON REMVING GUARD - LOG EVENT.
                                var orgeventlog = new Org_Events_Log()
                                {
                                    Org_Event_SubjectId = locateguard.RegisteredUserId.ToString(),
                                    Org_Event_SubjectName = locateguard.GuardianFullName,
                                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                    Org_Event_Time = DateTime.Now,
                                    OrgId = Session["OrgId"].ToString(),
                                    Org_Events_Types = Org_Events_Types.Deregistered_Guardian
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

                                        // LOCATE THE GD IN THE STUD GUARD TABLE  
                                        var locateguard = db.StudentGuardians.Where(x => x.StudentGuardianId == gd).FirstOrDefault();
                                        // CHECK HOW MANY OTHER STUD GUARD IS LINKED TO IN ACTIVE ORG = IF ONLY 1 THEN WE CAN REMV GUARD FROM REGUSERORG TABLE
                                        var linkedstdinorgcount = db.StudentGuardians.Where(x => x.RegisteredUserId == locateguard.RegisteredUserId)
                                            .Where(x => x.OrgId == i)
                                            .Select(x => x.OrgId)
                                            .Count();

                                        // IF ONLY 1 - MEANS GUARD IS ONLY LINKED TO BE DELETED WE GO INTO THIS CONDITION - SO WE DELETE GUARD FROM REGUSERORG TABLE AND LOG EVENT.
                                        if (linkedstdinorgcount == 1)
                                        {
                                            // LOCATE GUARD IN REGUSER ORG TABLE.                                      
                                            var locategd1 = db.RegisteredUserOrganisations.Where(x => x.RegisteredUserId == gdreguserid).Where(x => x.OrgId == i).Select(x => x.RegisteredUserOrganisationId).FirstOrDefault();

                                            // SOFT DELETE USER
                                            // GET USER'S DATA
                                            var gddataRu = db.RegisteredUsers.Where(x => x.RegisteredUserId == gdreguserid).FirstOrDefault();
                                            var gddataRug = db.RegisteredUserOrganisations
                                                .Where(x => x.RegisteredUserId == gdreguserid)
                                                .Where(x => x.OrgId == i)
                                                .FirstOrDefault();

                                            var remvgd1 = new RemovedRegisteredUser
                                            {
                                                RegisteredUserId = gddataRu.RegisteredUserId,
                                                CreationDate = DateTime.Now,
                                                FirstName = gddataRu.FirstName,
                                                LastName = gddataRu.LastName,
                                                FullName = gddataRu.FullName,
                                                Email = gddataRu.Email,
                                                Telephone = gddataRu.Telephone,
                                                RegisteredUserType = gddataRu.RegisteredUserTypeId,
                                                PrimarySchoolUserRole = gddataRug.PrimarySchoolUserRoleId.GetValueOrDefault(),
                                                SecondarySchoolUserRole = gddataRug.SecondarySchoolUserRoleId.GetValueOrDefault(),
                                                NurserySchoolUserRole = gddataRug.NurserySchoolUserRoleId.GetValueOrDefault(),
                                                OrgId = gddataRug.OrgId,
                                                ClassId = gddataRu.ClassId.GetValueOrDefault(),
                                                ClassRef = gddataRu.ClassRef.GetValueOrDefault(),
                                                GenderId = gddataRu.GenderId.GetValueOrDefault(),
                                                ReligionId = gddataRu.ReligionId.GetValueOrDefault(),
                                                StudentRegFormId = gddataRu.StudentRegFormId.GetValueOrDefault(),
                                                RelationshipId = locateguard.RelationshipId,
                                                IsTester = (bool)gddataRu.IsTester.GetValueOrDefault(),
                                                DateOfBirth = gddataRu.DateOfBirth,
                                                LastLogOn = gddataRug.LastLogOn,
                                                EnrolmentDate = gddataRug.EnrolmentDate.GetValueOrDefault(),
                                                EnrolledBy = Convert.ToInt32(gddataRug.CreatedBy)
                                            };
                                            db.RemovedRegisteredUsers.Add(remvgd1);
                                            db.SaveChanges();


                                            // DELETE GUARD FROM REGUSER ORG TABLE.
                                            RegisteredUserOrganisation regusrorg = db.RegisteredUserOrganisations.Find(locategd1);
                                            db.RegisteredUserOrganisations.Remove(regusrorg);
                                            db.SaveChanges();

                                            // LOOP THRU GROUP AND DELETE GUARD FROM ALL GROUP
                                            var guardingrp = db.RegisteredUsersGroups
                                                    .Where(x => x.RegisteredUserId == locateguard.RegisteredUserId)
                                                    .Where(x => x.RegUserOrgId == i)
                                                    .Select(x => x.RegisteredUsersGroupsId)
                                                    .ToList();
                                            var guardgrps = new List<int>(guardingrp);
                                            foreach (var gd2 in guardingrp)
                                            {
                                                RegisteredUsersGroups remvgdfrmgrp = db.RegisteredUsersGroups.Find(gd2);
                                                db.RegisteredUsersGroups.Remove(remvgdfrmgrp);
                                                db.SaveChanges();
                                            }
                                            // GET GUARDIANS FULLNAME 
                                            var guardfullname = db.RegisteredUsers.Where(x => x.RegisteredUserId == gdreguserid).Select(x => x.FullName).FirstOrDefault();
                                            // WE THEN LOG EVENT 
                                            var orgeventlog = new Org_Events_Log()
                                            {
                                                Org_Event_SubjectId = gdreguserid.ToString(),
                                                Org_Event_SubjectName = guardfullname,
                                                Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                                                Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                                                Org_Event_Time = DateTime.Now,
                                                OrgId = Session["OrgId"].ToString(),
                                                Org_Events_Types = Org_Events_Types.Deregistered_Guardian
                                            };
                                            db.Org_Events_Logs.Add(orgeventlog);
                                            db.SaveChanges();
                                        }
                                        // MEANS GUARD IS  LINKED TO MORE THAN 1 STUDENT - WE GO INTO THIS CONDITION - 
                                        else
                                        {
                                            // LOCATE GUARDIANS GROUP ID IN STUDGUARD TABLE
                                            var locateguardgrpid = db.StudentGuardians
                                                    .Where(x => x.StudentGuardianId == gd)
                                                    .Where(x => x.OrgId == i)
                                                    .Where(x => x.StudentId == id)
                                                    .Select(x => x.Stu_class_Org_Grp_id).FirstOrDefault();
                                            // LOCATE GUARDIANS REGUSERGRPID
                                            var guardingrpcount = db.RegisteredUsersGroups
                                                    .Where(x => x.RegisteredUserId == locateguard.RegisteredUserId)
                                                    .Where(x => x.OrgGroupId == locateguardgrpid)
                                                    .Where(x => x.RegUserOrgId == i)
                                                    .Where(x => x.LinkedStudentId == id)
                                                    .Select(x => x.RegisteredUsersGroupsId).Count();
                                            if (guardingrpcount > 0)
                                            {
                                                var guardingrplist = db.RegisteredUsersGroups
                                               .Where(x => x.RegisteredUserId == locateguard.RegisteredUserId)
                                               .Where(x => x.OrgGroupId == locateguardgrpid)
                                               .Where(x => x.RegUserOrgId == i)
                                               .Where(x => x.LinkedStudentId == id)
                                               .Select(x => x.RegisteredUsersGroupsId).ToList();
                                                var gdingrpid = new List<int>(guardingrplist);
                                                foreach (var g in guardingrplist)
                                                {
                                                    RegisteredUsersGroups remvgdfrmgrp = db.RegisteredUsersGroups.Find(g);
                                                    db.RegisteredUsersGroups.Remove(remvgdfrmgrp);
                                                    db.SaveChanges();
                                                }
                                            }
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

                    //CHECK IF STUD HAS ANY GRADES IN CURRENT CLASS
                    var checkgrades = db.StudentSubjectGrades.Where(x => x.RegisteredUserId == id).Count();

                    // IF GRADES COUNT > 0, LOG THE GRADES IN Students_Grades_LogController
                    if (checkgrades > 0)
                    {
                        // GET LIST OF GRADES
                        var grades = db.StudentSubjectGrades
                                .Where(x => x.RegisteredUserId == id)
                                .Where(x => x.OrgId == i)
                                .Select(x => x.StudentSubjectGradeId)
                                .ToList();
                        var listofsubjects = new List<int>(grades);

                        foreach (var grade in grades)
                        {
                            var currentgrade = db.StudentSubjectGrades
                                .Where(x => x.StudentSubjectGradeId == grade)
                                .Where(x => x.OrgId == i)
                                .FirstOrDefault();

                            var currentsubject = db.Subjects
                                .Where(x => x.SubjectId == currentgrade.SubjectId)
                                .Where(x => x.SubjectOrgId == i)
                                .FirstOrDefault();

                            var gradeslog = new Students_Grades_Log
                            {
                                RegisteredUserId = id,
                                SubjectId = currentgrade.SubjectId,
                                ClassId = (int)currentgrade.ClassId,
                                ClassRef = (int)currentgrade.ClassRef,
                                OrgId = (int)currentgrade.OrgId,
                                FirstTerm_ExamGrade = currentgrade.FirstTerm_ExamGrade,
                                SecondTerm_ExamGrade = currentgrade.SecondTerm_ExamGrade,
                                ThirdTerm_ExamGrade = currentgrade.ThirdTerm_ExamGrade,
                                FirstTerm_TestGrade = currentgrade.FirstTerm_TestGrade,
                                SecondTerm_TestGrade = currentgrade.SecondTerm_TestGrade,
                                ThirdTerm_TestGrade = currentgrade.ThirdTerm_TestGrade,
                                Last_updated_date = currentgrade.Last_updated_date,
                                First_Term_Exam_MaxGrade = currentsubject.First_Term_Exam_MaxGrade,
                                Second_Term_Exam_MaxGrade = currentsubject.Second_Term_Exam_MaxGrade,
                                Third_Term_Exam_MaxGrade = currentsubject.Third_Term_Exam_MaxGrade,
                                First_Term_Test_MaxGrade = currentsubject.First_Term_Test_MaxGrade,
                                Second_Term_Test_MaxGrade = currentsubject.Second_Term_Test_MaxGrade,
                                Third_Term_Test_MaxGrade = currentsubject.Third_Term_Test_MaxGrade,
                                Subject_Min_Passmark = currentsubject.Subject_Min_Passmark,
                                Subject_Max_Passmark = currentsubject.SubjectMaxGrade,
                                Updater_Id = (int)currentgrade.Updater_Id,
                                StudentClassChangeType = StudentClassChangeType.Change_of_class,
                                ClassTeacherId = currentsubject.ClassTeacherId,
                                Created_date = DateTime.Now,

                            };
                            db.Students_Grades_Logs.Add(gradeslog);
                            db.SaveChanges();

                            // REMOVE RECORD FROM STUSUBGRADS TABLE 
                            db.StudentSubjectGrades.Remove(currentgrade);
                            db.SaveChanges();
                        }
                    }


                }

                // SOFT DELETE USER
                // GET USER'S DATA
                var userdataRu = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).FirstOrDefault();
                var userdataRug = db.RegisteredUserOrganisations
                    .Where(x => x.RegisteredUserId == id)
                    .Where(x => x.OrgId == i)
                    .FirstOrDefault();
                var remvuser = new RemovedRegisteredUser
                {
                    RegisteredUserId = userdataRu.RegisteredUserId,
                    CreationDate = DateTime.Now,
                    FirstName = userdataRu.FirstName,
                    LastName = userdataRu.LastName,
                    FullName = userdataRu.FullName,
                    Email = userdataRu.Email,
                    Telephone = userdataRu.Telephone,
                    RegisteredUserType = userdataRu.RegisteredUserTypeId,
                    PrimarySchoolUserRole = userdataRug.PrimarySchoolUserRoleId.GetValueOrDefault(),
                    SecondarySchoolUserRole = userdataRug.SecondarySchoolUserRoleId.GetValueOrDefault(),
                    NurserySchoolUserRole = userdataRug.NurserySchoolUserRoleId.GetValueOrDefault(),
                    OrgId = userdataRug.OrgId,
                    ClassId = userdataRu.ClassId.GetValueOrDefault(),
                    ClassRef = userdataRu.ClassRef.GetValueOrDefault(),
                    GenderId = userdataRu.GenderId.GetValueOrDefault(),
                    ReligionId = userdataRu.ReligionId.GetValueOrDefault(),
                    StudentRegFormId = userdataRu.StudentRegFormId.GetValueOrDefault(),
                    RelationshipId = userdataRu.RelationshipId.GetValueOrDefault(),
                    IsTester = (bool)userdataRu.IsTester.GetValueOrDefault(),
                    DateOfBirth = userdataRu.DateOfBirth,
                    LastLogOn = userdataRug.LastLogOn,
                    EnrolmentDate = userdataRug.EnrolmentDate.GetValueOrDefault(),
                    EnrolledBy = Convert.ToInt32(userdataRug.CreatedBy)
                };
                db.RemovedRegisteredUsers.Add(remvuser);
                db.SaveChanges();

                // CHECK IF USER BEING DELETED IS A STUDENT = IF YES - LOG EVENT AND UPDATE CLASS PROFILE - DELETE STUDENT
                var chkifstud1 = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).Select(x => x.StudentRegFormId).FirstOrDefault();
                if (chkifstud != null)
                {
                    var locateclassid = db.RegisteredUsers.Where(x => x.RegisteredUserId == id).FirstOrDefault();

                    //GET CLASSID OF STUDENT
                    var cid = locateclassid.ClassId;

                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_SubjectId = id.ToString(),
                        Org_Event_SubjectName = userdataRu.FullName,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString(),
                        Org_Events_Types = Org_Events_Types.Deregistered_Student
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();


                    // IF USER BEING DELETED IS A STUDENT - 
                    RegisteredUser regUser = db.RegisteredUsers.Find(id);
                    db.RegisteredUsers.Remove(regUser);
                    db.SaveChanges();

                    var updateclasses = UpdateClassProfile((int)cid);
                    return RedirectToAction("AllStudents");
                }


                // IF USER BEING DELETED IS NOT A STUDENT - WE COME HERE STRAIGHT AND REMOVE USER.
                RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
                db.RegisteredUsers.Remove(registeredUser);
                db.SaveChanges();


                return RedirectToAction("AllStudents");
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

    }
}