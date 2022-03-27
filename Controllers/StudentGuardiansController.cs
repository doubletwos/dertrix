using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;

using System.Net;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;
using System.Text;

namespace Dertrix.Controllers
{
    public class StudentGuardiansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentGuardians
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        public ActionResult MyGuardians(int id)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }




        [ChildActionOnly]
        public ActionResult MyChildCount()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }




        public ActionResult MyChildList()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }


        public ActionResult EditGuardian(int Id)
        {
            try
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
                        Telephone = guardian.Telephone,
                        IsRegistered = guardian.IsRegistered,
                        RegisteredDate = guardian.RegisteredDate,
                        LastLogOn = guardian.LastLogOn,
                        InviteSentDate = guardian.InviteSentDate,
                        CountOfInvite = guardian.CountOfInvite
                    };
                    ViewBag.RelationshipId = new SelectList(db.Relationships, "RelationshipId", "RelationshipName", guardian.RelationshipId);
                    ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", guardian.TitleId);
                    return PartialView("~/Views/Shared/PartialViewsForms/_EditGuardian.cshtml", studentguardian);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditGuardian.cshtml");
        }






        // POST: StudentGuardians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentGuardian studentGuardian)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.StudentGuardians.Add(studentGuardian);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return View(studentGuardian);
        }


        // POST: StudentGuardians/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentGuardian studentGuardian)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    studentGuardian.GuardianFullName = studentGuardian.GuardianFirstName + studentGuardian.GuardianLastName;
                    db.Entry(studentGuardian).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return View(studentGuardian);
        }


        //[HttpPost]
        public JsonResult SendEmailInvitation(int id)
        {
            if (Session["IsTester"] != null)
            {
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                // GET TESTER'S EMAIL ADDRESS
                var useremail = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.Email).FirstOrDefault();

                string Body = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/InvitationEmail.html"));
                Body = Body.Replace("#OrganisationName#", Session["OrgName"].ToString());
                Body = Body.Replace("var(--white)", Session["regOrgBrandButtonColour"].ToString());
                var orgName = Session["OrgName"].ToString();
                var subject = "Invitation from" + " " + orgName;
                bool result = false;
                result = SendEmail(useremail, subject, Body);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                // GET USER'S EMAIL ADDRESS
                var user = db.StudentGuardians.AsNoTracking().Where(x => x.StudentGuardianId == id).FirstOrDefault();

                var invitecount = user.CountOfInvite;
                if (invitecount == null)
                {
                    var zero = 0;
                    invitecount = zero;
                }

                var guardian = new StudentGuardian
                {
                    StudentGuardianId = user.StudentGuardianId,
                    RegisteredUserId = user.RegisteredUserId,
                    GuardianFirstName = user.GuardianFirstName,
                    GuardianLastName = user.GuardianLastName,
                    GuardianFullName = user.GuardianFullName,
                    GuardianEmailAddress = user.GuardianEmailAddress,
                    DateAdded = user.DateAdded,
                    StudentId = user.StudentId,
                    StudentFullName = user.StudentFullName,
                    OrgId = user.OrgId,
                    TitleId = user.TitleId,
                    RelationshipId = user.RelationshipId,
                    Telephone = user.Telephone,
                    Stu_class_Org_Grp_id = user.Stu_class_Org_Grp_id,
                    IsRegistered = user.IsRegistered,
                    RegisteredDate = user.RegisteredDate,
                    LastLogOn = user.LastLogOn,
                    InviteSentDate = DateTime.Now,
                    CountOfInvite = invitecount + 1
                };
                user = guardian;
                db.Entry(guardian).State = EntityState.Modified;
                db.SaveChanges();


                string Body = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/InvitationEmail.html"));
                Body = Body.Replace("#OrganisationName#", Session["OrgName"].ToString());
                Body = Body.Replace("var(--white)", Session["regOrgBrandButtonColour"].ToString());
                //Body = Body.Replace("#Body#", postcontent);
                var orgName = Session["OrgName"].ToString();
                var subject = "Invitation from" + " " + orgName;
                bool result = false;
                result = SendEmail(user.GuardianEmailAddress, subject, Body);
                return Json(result, JsonRequestBehavior.AllowGet);

            }





        }


        public bool SendEmail(string toEmail, string subject, string Body)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.ionos.co.uk", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, Body);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }



        // POST: StudentGuardians/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                // GET GUARDIANS REGUSERID
                var gd_id = db.StudentGuardians.Where(x => x.StudentGuardianId == id).FirstOrDefault();

                // CHECK IF GUARDIAN IS LINKED TO ANY OTHER STUDENT IN THIS ORG - IF NO - DELETE FROM SG /REGUSERORG/ REGUSER - TABLE AND LOG EVENT. CHECK USER IS DELETED FROM GRP LINKED TO CLASS.


                // COUNT HOW MANY STUDENT GUARDIAN IS LINKED TO IN THE DATABASE
                var linked_stud = db.StudentGuardians.Where(x => x.RegisteredUserId == gd_id.RegisteredUserId).Count();
                // IF COUNT OF LINKED STUDENT IS 1 - MEANS GUARDIAN IS ONLY LINKED TO THE STUDENT - WE GO IN THIS CONDITON AND FULLY DELETE GUARDIAN FROM THE SYSTEM.
                if (linked_stud == 1)
                {
                    // LOG EVENT 
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_SubjectId = gd_id.RegisteredUserId.ToString(),
                        Org_Event_SubjectName = gd_id.GuardianFullName,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString(),
                        Org_Events_Types = Org_Events_Types.Deregistered_Guardian

                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();

                    // UPDATE STUD'S GUARDIAN COUNT.

                    // GET STUD ID
                    var studid = db.StudentGuardians.Where(x => x.StudentGuardianId == id).Select(x => x.StudentId).FirstOrDefault();
                    var locatestud = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == studid).FirstOrDefault();
                    var currentcount = db.RegisteredUsers.Where(x => x.RegisteredUserId == studid).Select(x => x.PgCount).FirstOrDefault();

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
                        PgCount = currentcount - 1

                    };
                    locatestud = studgaurd;
                    db.Entry(studgaurd).State = EntityState.Modified;
                    db.SaveChanges();

                    // SOFT DELETE USER
                    // GET USER'S DATA
                    var userdataRu = db.RegisteredUsers.Where(x => x.RegisteredUserId == gd_id.RegisteredUserId).FirstOrDefault();
                    var userdataRug = db.RegisteredUserOrganisations
                        .Where(x => x.RegisteredUserId == gd_id.RegisteredUserId)
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
                        RelationshipId = gd_id.RelationshipId,
                        IsTester = (bool)userdataRu.IsTester.GetValueOrDefault(),
                        DateOfBirth = userdataRu.DateOfBirth,
                        LastLogOn = userdataRug.LastLogOn,
                        EnrolmentDate = userdataRug.EnrolmentDate.GetValueOrDefault(),
                        EnrolledBy = Convert.ToInt32(userdataRug.CreatedBy)
                    };
                    db.RemovedRegisteredUsers.Add(remvuser);
                    db.SaveChanges();


                    // REMV FROM PG FROM SG
                    RegisteredUser remv_g = db.RegisteredUsers.Find(gd_id.RegisteredUserId);
                    db.RegisteredUsers.Remove(remv_g);
                    db.SaveChanges();

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
                else
                {
                    // GET THE STUD PG IS BEING UNLINKED FROM
                    var stud_id = db.StudentGuardians.Where(x => x.StudentGuardianId == id).FirstOrDefault();

                    // CHECK HOW MANY STU IN ORG PG IS LINKED TO - IF 1 - REMV PG FROM SG / REGUORG - ONLY
                    var mylinkedstudsinorg = db.StudentGuardians
                        .Where(x => x.RegisteredUserId == gd_id.RegisteredUserId)
                        .Where(x => x.OrgId == i)
                        .Select(x => x.RegisteredUserId)
                        .Count();


                    // COUNT OF mylinkedstudsinorg IS 1 - MEANS PG IS LINKED TO ONLY 1 STUD IN THIS ORG. - REMV PG FROM REGUSERORG / SG / ORGGROUP /TABLE
                    if (mylinkedstudsinorg == 1)
                    {
                        var getpginreguserorg = db.RegisteredUserOrganisations
                            .Where(x => x.RegisteredUserId == gd_id.RegisteredUserId)
                            .Select(x => x.RegisteredUserOrganisationId)
                            .FirstOrDefault();

                        // SOFT DELETE USER
                        // GET USER'S DATA
                        var userdataRu = db.RegisteredUsers.Where(x => x.RegisteredUserId == gd_id.RegisteredUserId).FirstOrDefault();

                        var userdataRug = db.RegisteredUserOrganisations
                            .Where(x => x.RegisteredUserId == gd_id.RegisteredUserId)
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
                            RelationshipId = stud_id.RelationshipId.GetValueOrDefault(),
                            IsTester = (bool)userdataRu.IsTester.GetValueOrDefault(),
                            DateOfBirth = userdataRu.DateOfBirth,
                            LastLogOn = userdataRug.LastLogOn,
                            EnrolmentDate = userdataRug.EnrolmentDate.GetValueOrDefault(),
                            EnrolledBy = Convert.ToInt32(userdataRug.CreatedBy)
                        };
                        db.RemovedRegisteredUsers.Add(remvuser);
                        db.SaveChanges();

                        // REMV FROM REGUSERORG
                        RegisteredUserOrganisation reguserord = db.RegisteredUserOrganisations.Find(getpginreguserorg);
                        db.RegisteredUserOrganisations.Remove(reguserord);
                        db.SaveChanges();


                        // REMV PG FROM ORGGRP - 
                        var pginorggrp = db.RegisteredUsersGroups
                            .Where(x => x.RegisteredUserId == gd_id.RegisteredUserId)
                            .Where(x => x.RegUserOrgId == i)
                            .Select(x => x.RegisteredUsersGroupsId)
                            .ToList();

                        var pgingrp = new List<int>(pginorggrp);

                        foreach (var pg in pginorggrp)
                        {
                            RegisteredUsersGroups regusrg = db.RegisteredUsersGroups.Find(pg);
                            db.RegisteredUsersGroups.Remove(regusrg);
                            db.SaveChanges();

                        }
                        // LOG EVENT 
                        var orgeventlog = new Org_Events_Log()
                        {
                            Org_Event_SubjectId = gd_id.RegisteredUserId.ToString(),
                            Org_Event_SubjectName = stud_id.GuardianFullName,
                            Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                            Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                            Org_Event_Time = DateTime.Now,
                            OrgId = Session["OrgId"].ToString(),
                            Org_Events_Types = Org_Events_Types.Deregistered_Guardian
                        };
                        db.Org_Events_Logs.Add(orgeventlog);
                        db.SaveChanges();


                        // UPDATE STUD'S GUARDIAN COUNT.
                        // GET STUD ID
                        var studid = db.StudentGuardians.Where(x => x.StudentGuardianId == id).Select(x => x.StudentId).FirstOrDefault();
                        var locatestud = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == studid).FirstOrDefault();
                        var currentcount = db.RegisteredUsers.Where(x => x.RegisteredUserId == studid).Select(x => x.PgCount).FirstOrDefault();

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
                            PgCount = currentcount - 1

                        };
                        locatestud = studgaurd;
                        db.Entry(studgaurd).State = EntityState.Modified;
                        db.SaveChanges();



                        // REMV FROM PG FROM SG
                        StudentGuardian studgd = db.StudentGuardians.Find(id);
                        db.StudentGuardians.Remove(studgd);
                        db.SaveChanges();

                    }
                    else
                    {
                        // GET STUD CLASS GRP ID
                        var studclassGrpid = db.StudentGuardians
                            .Where(x => x.RegisteredUserId == gd_id.RegisteredUserId)
                            .Where(x => x.OrgId == i)
                            .Where(x => x.StudentId == stud_id.StudentId)
                            .Select(x => x.Stu_class_Org_Grp_id)
                            .FirstOrDefault();

                        // COUNT HOW MANY STUDENTS GD IS LINKED TO IN CLASS
                        var alllinkedstuds = db.StudentGuardians
                            .Where(x => x.RegisteredUserId == gd_id.RegisteredUserId)
                            .Where(x => x.OrgId == i)
                            .Where(x => x.Stu_class_Org_Grp_id == studclassGrpid)
                            .Select(x => x.Stu_class_Org_Grp_id == studclassGrpid)
                            .Count();


                        // LOOP THRU THE LIST OF STUDENTS PG IS LINKED TO 
                        var mylinkedstuds = db.StudentGuardians.Where(x => x.RegisteredUserId == gd_id.RegisteredUserId).Select(x => x.StudentGuardianId).ToList();
                        var linkstudents = new List<int>(mylinkedstuds);

                        // MORE THAN 1 - LOOP THRU LIST OF STUD AND REMV FROM SG
                        foreach (var std in mylinkedstuds)
                        {
                            if (std == id)
                            {

                                // PG IS LINKED TO ANOTHER STUD IN CLASS - SO WE ONLY REMV RECRD OF LINKED STU N PG IN REGUSRORGGRP
                                if (alllinkedstuds > 1)
                                {
                                    // UPDATE STUD'S GUARDIAN COUNT.
                                    // GET STUD ID
                                    var studid = db.StudentGuardians.Where(x => x.StudentGuardianId == id).Select(x => x.StudentId).FirstOrDefault();
                                    var locatestud = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == studid).FirstOrDefault();
                                    var currentcount = db.RegisteredUsers.Where(x => x.RegisteredUserId == studid).Select(x => x.PgCount).FirstOrDefault();

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
                                        PgCount = currentcount - 1

                                    };
                                    locatestud = studgaurd;
                                    db.Entry(studgaurd).State = EntityState.Modified;
                                    db.SaveChanges();

                                    // REMV FROM PG FROM SG
                                    StudentGuardian studgd = db.StudentGuardians.Find(id);
                                    db.StudentGuardians.Remove(studgd);
                                    db.SaveChanges();

                                    // REMV PG FROM ORGGRP - 
                                    var pginorggrp = db.RegisteredUsersGroups
                                        .Where(x => x.RegisteredUserId == gd_id.RegisteredUserId)
                                        .Where(x => x.RegUserOrgId == i)
                                        .Where(X => X.OrgGroupId == studclassGrpid)
                                        .Where(x => x.LinkedStudentId == studid)
                                        .Select(x => x.RegisteredUsersGroupsId).ToList();
                                    var pgingrp = new List<int>(pginorggrp);

                                    foreach (var pg in pginorggrp)
                                    {
                                        RegisteredUsersGroups regusrg = db.RegisteredUsersGroups.Find(pg);
                                        db.RegisteredUsersGroups.Remove(regusrg);
                                        db.SaveChanges();


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
                                    //EXIT
                                    return RedirectToAction("Index");

                                }
                                // PG IS LINKED TO JUST THIS STUD IN CLASS - SO WE REMV FROM CLASSGRP AND SG TABLE

                                if (alllinkedstuds == 1)
                                {

                                    // REMV PG FROM ORGGRP - 
                                    var pginorggrp = db.RegisteredUsersGroups
                                        .Where(x => x.RegisteredUserId == gd_id.RegisteredUserId)
                                        .Where(x => x.RegUserOrgId == i)
                                        .Where(X => X.OrgGroupId == studclassGrpid)
                                        .Select(x => x.RegisteredUsersGroupsId).ToList();

                                    var pgingrp = new List<int>(pginorggrp);

                                    foreach (var pg in pginorggrp)
                                    {
                                        RegisteredUsersGroups regusrg = db.RegisteredUsersGroups.Find(pg);
                                        db.RegisteredUsersGroups.Remove(regusrg);
                                        db.SaveChanges();

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

                                    // UPDATE STUD'S GUARDIAN COUNT.
                                    // GET STUD ID
                                    var studid = db.StudentGuardians.Where(x => x.StudentGuardianId == id).Select(x => x.StudentId).FirstOrDefault();
                                    var locatestud = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == studid).FirstOrDefault();
                                    var currentcount = db.RegisteredUsers.Where(x => x.RegisteredUserId == studid).Select(x => x.PgCount).FirstOrDefault();

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
                                        PgCount = currentcount - 1

                                    };
                                    locatestud = studgaurd;
                                    db.Entry(studgaurd).State = EntityState.Modified;
                                    db.SaveChanges();

                                    // REMV FROM PG FROM SG
                                    StudentGuardian studgd = db.StudentGuardians.Find(id);
                                    db.StudentGuardians.Remove(studgd);
                                    db.SaveChanges();

                                }

                            }
                        }


                    };
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
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
