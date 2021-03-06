using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Dertrix.Models;
using System.Net.Mail;
using System.Text;

namespace Dertrix.Controllers
{
    [RoutePrefix("")]
    public class AccessController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult LogOut(string id)
        {
            try
            {
                if (id == null)
                {
                    Session.Abandon();
                    return RedirectToRoute(new { controller = "Access", action = "Signin", });
                }
                else
                {
                    var logouttime = DateTime.Now;
                    var getsession = db.RegUsersAccessLogs.AsNoTracking().Where(x => x.SessionId == id).FirstOrDefault();
                    var getsession1 = new RegUsersAccessLog
                    {
                        RegUsersAccessLogId = getsession.RegUsersAccessLogId,
                        OrgId = getsession.OrgId,
                        SessionId = getsession.SessionId,
                        RegUserId = getsession.RegUserId,
                        UserFullName = getsession.UserFullName,
                        LogInTime = getsession.LogInTime,
                        LogOutTime = logouttime
                    };
                    getsession = getsession1;
                    db.Entry(getsession).State = EntityState.Modified;
                    db.SaveChanges();
                    Session.Abandon();
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                    return RedirectToRoute(new { controller = "Access", action = "Signin", });

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }



        // GET: Access/Logs
        [Route("Logs")]
        public ActionResult Logs()
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToRoute(new { controller = "Access", action = "Signin", });
                }
                if ((int)Session["OrgId"] != 23)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return View(db.RegUsersAccessLogs.ToList());
        }

        // GET: Access/KeyCheck
        [Route("KeyCheck")]
        public ActionResult KeyCheck()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckKey(RegisteredUser registeredUser)
        {
            try
            {
                var locateuser = db.RegisteredUsers.Where(x => x.InviteKey == registeredUser.InviteKey).FirstOrDefault();
                var locateusercount = db.RegisteredUsers.Where(x => x.InviteKey == registeredUser.InviteKey).Count();


                if (registeredUser.InviteKey == null)
                {
                    registeredUser.LoginErrorMsg = "Please enter invitation key or a valid invitation key";
                    return View("KeyCheck", registeredUser);
                }
                if (registeredUser.InviteKey.Length < 6)
                {
                    registeredUser.LoginErrorMsg = "Invitation key must be at least 6 characters long.";
                    return View("KeyCheck", registeredUser);
                }
                if (locateusercount == 0)
                {
                    registeredUser.LoginErrorMsg = "Please enter invitation key or a valid invitation key";
                    return View("KeyCheck", registeredUser);
                }
                if (registeredUser.InviteKey == locateuser.InviteKey)
                {
                    return RedirectToRoute(new { controller = "Access", action = "InitialSettings", id = locateuser.RegisteredUserId });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(registeredUser);
            }
            return View();
        }


        // GET: Access/InitialSettings
        [Route("InitialSettings")]
        public ActionResult InitialSettings(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var newuser = db.RegisteredUsers.Where(x => x.RegisteredUserId == Id).FirstOrDefault();

                    var usr = new RegisteredUser
                    {
                        RegisteredUserId = newuser.RegisteredUserId,
                        RegisteredUserType = newuser.RegisteredUserType,
                        FirstName = newuser.FirstName,
                        LastName = newuser.LastName,
                        Email = newuser.Email,
                        Password = newuser.Password,
                        ConfirmPassword = newuser.ConfirmPassword,
                        Telephone = newuser.Telephone,
                        SelectedOrg = newuser.SelectedOrg,
                        ClassId = newuser.ClassId,
                        GenderId = newuser.GenderId,
                        TribeId = newuser.TribeId,
                        DateOfBirth = newuser.DateOfBirth,
                        EnrolmentDate = newuser.EnrolmentDate,
                        ReligionId = newuser.ReligionId,
                        PrimarySchoolUserRoleId = newuser.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = newuser.SecondarySchoolUserRoleId,
                        StudentRegFormId = newuser.StudentRegFormId,
                        CreatedBy = newuser.CreatedBy,
                        RegUserOrgBrand = newuser.RegUserOrgBrand,
                        FullName = newuser.FullName,
                        IsTester = newuser.IsTester,
                        ClassRef = newuser.ClassRef,
                        TempIntHolder = newuser.TempIntHolder,
                        TitleId = newuser.TitleId,
                        RelationshipId = newuser.RelationshipId,
                        PgCount = newuser.PgCount,
                        NurserySchoolUserRoleId = newuser.NurserySchoolUserRoleId,
                        InviteKey = newuser.InviteKey,
                        InviteSentDate = newuser.InviteSentDate,
                        CountOfInvite = newuser.CountOfInvite,
                        IsRegistered = newuser.IsRegistered,
                        RegisteredDate = newuser.RegisteredDate,
                    };
                    return View(usr);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmInitialSettings(RegisteredUser registeredUser)
        {
            try
            {
                var locateuser = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == registeredUser.RegisteredUserId).FirstOrDefault();

                // Set Usr password & Delete Invite Key
                var usr = new RegisteredUser
                {
                    RegisteredUserId = locateuser.RegisteredUserId,
                    RegisteredUserTypeId = locateuser.RegisteredUserTypeId,
                    FirstName = locateuser.FirstName,
                    LastName = locateuser.LastName,
                    Email = locateuser.Email,
                    Password = registeredUser.Password,
                    ConfirmPassword = registeredUser.ConfirmPassword,
                    Telephone = locateuser.Telephone,
                    SelectedOrg = locateuser.SelectedOrg,
                    ClassId = locateuser.ClassId,
                    GenderId = locateuser.GenderId,
                    TribeId = locateuser.TribeId,
                    DateOfBirth = locateuser.DateOfBirth,
                    EnrolmentDate = locateuser.EnrolmentDate,
                    ReligionId = locateuser.ReligionId,
                    PrimarySchoolUserRoleId = locateuser.PrimarySchoolUserRoleId,
                    SecondarySchoolUserRoleId = locateuser.SecondarySchoolUserRoleId,
                    StudentRegFormId = locateuser.StudentRegFormId,
                    CreatedBy = locateuser.CreatedBy,
                    RegUserOrgBrand = locateuser.RegUserOrgBrand,
                    FullName = locateuser.FullName,
                    IsTester = locateuser.IsTester,
                    ClassRef = locateuser.ClassRef,
                    TempIntHolder = locateuser.TempIntHolder,
                    TitleId = locateuser.TitleId,
                    RelationshipId = locateuser.RelationshipId,
                    PgCount = locateuser.PgCount,
                    NurserySchoolUserRoleId = locateuser.NurserySchoolUserRoleId,
                    InviteKey = "",
                    InviteSentDate = locateuser.InviteSentDate,
                    CountOfInvite = locateuser.CountOfInvite,
                    IsRegistered = true,
                    RegisteredDate = DateTime.Now,
                };
                locateuser = usr;
                db.Entry(locateuser).State = EntityState.Modified;
                db.SaveChanges();


                // Locate SG in the SG table and set IsRegistered to TRUE
                var locateGD = db.StudentGuardians.AsNoTracking()
                    .Where(x => x.RegisteredUserId == locateuser.RegisteredUserId)
                    .FirstOrDefault();

                var updategd = new StudentGuardian
                {
                    StudentGuardianId = locateGD.StudentGuardianId,
                    RegisteredUserId = locateGD.RegisteredUserId,
                    GuardianFirstName = locateGD.GuardianFirstName,
                    GuardianLastName = locateGD.GuardianLastName,
                    GuardianFullName = locateGD.GuardianFullName,
                    GuardianEmailAddress = locateGD.GuardianEmailAddress,
                    DateAdded = locateGD.DateAdded,
                    StudentId = locateGD.StudentId,
                    StudentFullName = locateGD.StudentFullName,
                    OrgId = locateGD.OrgId,
                    TitleId = locateGD.TitleId,
                    RelationshipId = locateGD.RelationshipId,
                    Telephone = locateGD.Telephone,
                    Stu_class_Org_Grp_id = locateGD.Stu_class_Org_Grp_id,
                    IsRegistered = true
                };
                locateGD = updategd;
                db.Entry(locateGD).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToRoute(new { controller = "Access", action = "Signin", });


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Redirect("~/ErrorHandler.html");
            }

        }




        // GET: Access/Signin
        [Route("LogIn")]
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(RegisteredUser registeredUser)
        {
            try
            {
                var reguserdetails = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email && x.Password == registeredUser.Password).FirstOrDefault();
                var orgredirect = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email.ToString()).Select(x => x.OrgId).FirstOrDefault();
                var reguserorg = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Select(x => x.OrgName).FirstOrDefault();
                var regUserOrgBrand = db.RegisteredUserOrganisations.Where(x => x.Email == registeredUser.Email).Select(x => x.RegUserOrgBrand).FirstOrDefault();
                if (reguserdetails == null)
                {
                    registeredUser.LoginErrorMsg = "Invalid Email or Password";
                    return View("Signin", registeredUser);
                }
                else
                {
                    Session["RegisteredUserId"] = reguserdetails.RegisteredUserId.ToString();
                    Session["Email"] = reguserdetails.Email.ToString();
                    Session["FullName"] = reguserdetails.FullName;
                    Session["RegisteredUserTypeId"] = reguserdetails.RegisteredUserTypeId;
                    Session["OrgId"] = orgredirect;
                    Session["OrgName"] = reguserorg;
                    Session["regUserOrgBrand"] = regUserOrgBrand;
                    Session["regOrgBrandButtonColour"] = db.OrgBrands.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.OrgBrandButtonColour).FirstOrDefault();
                    Session["regOrgLogo"] = db.Files.Where(x => x.OrgBrandId == regUserOrgBrand).Select(x => x.Content).FirstOrDefault();
                    Session["IsTester"] = reguserdetails.IsTester;
                    Session["OrgType"] = db.Orgs.Where(x => x.OrgId == orgredirect).Select(x => x.OrgTypeId).FirstOrDefault();
                    Session["IsAdmin"] = db.RegisteredUsersGroups.Where(x => x.RegisteredUserId == reguserdetails.RegisteredUserId).Where(x => x.RegUserOrgId == orgredirect).Where(x => x.GroupTypeId == 1).Select(x => x.GroupTypeId).FirstOrDefault();
                    Session["IsParent/Guardian"] = db.StudentGuardians.Where(x => x.RegisteredUserId == reguserdetails.RegisteredUserId && x.OrgId == orgredirect).Select(x => x.GuardianEmailAddress).FirstOrDefault();
                }
                var reguseraccessLogs = new RegUsersAccessLog
                {
                    OrgId = orgredirect,
                    SessionId = HttpContext.Session.SessionID.ToString(),
                    RegUserId = reguserdetails.RegisteredUserId,
                    UserFullName = reguserdetails.FullName,
                    LogInTime = DateTime.Now,
                    LogOutTime = null
                };
                db.RegUsersAccessLogs.Add(reguseraccessLogs);
                db.SaveChanges();


                //Upon successful logon, update logon date/time column in ReguserOrg Table
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var reguserid = db.RegisteredUserOrganisations.AsNoTracking().Where(x => x.RegisteredUserId == reguserdetails.RegisteredUserId).Where(x => x.OrgId == i).FirstOrDefault();

                var reguserorgn = new RegisteredUserOrganisation
                {
                    RegisteredUserOrganisationId = reguserid.RegisteredUserOrganisationId,
                    RegisteredUserId = reguserid.RegisteredUserId,
                    OrgId = reguserid.OrgId,
                    Email = reguserid.Email,
                    FirstName = reguserid.FirstName,
                    LastName = reguserid.LastName,
                    OrgName = reguserid.OrgName,
                    RegUserOrgBrand = reguserid.RegUserOrgBrand,
                    IsTester = reguserid.IsTester,
                    RegisteredUserTypeId = reguserid.RegisteredUserTypeId,
                    PrimarySchoolUserRoleId = reguserid.PrimarySchoolUserRoleId,
                    SecondarySchoolUserRoleId = reguserid.SecondarySchoolUserRoleId,
                    NurserySchoolUserRoleId = reguserid.NurserySchoolUserRoleId,
                    EnrolmentDate = reguserid.EnrolmentDate,
                    CreatedBy = reguserid.CreatedBy,
                    FullName = reguserid.FullName,
                    TitleId = reguserid.TitleId,
                    LastLogOn = DateTime.Now,
                };

                reguserid = reguserorgn;
                db.Entry(reguserid).State = EntityState.Modified;
                db.SaveChanges();

                if (orgredirect == 23)
                {
                    return RedirectToAction("SystemAdminIndex", "Orgs");
                }

                if (Session["IsParent/Guardian"] != null)
                {
                    // check status of Isregistered
                    var registeredstatus = db.StudentGuardians
                        .Where(x => x.RegisteredUserId == reguserid.RegisteredUserId)
                        //.Where(x => x.OrgId == reguserid.OrgId)
                        .Where(x => x.IsRegistered == false || x.IsRegistered == null)
                        .Select(x => x.IsRegistered)
                        .ToList();

                    if (registeredstatus.Count > 0)
                    {
                        // Locate SG in the SG table and set IsRegistered to TRUE
                        var locateGD = db.StudentGuardians.AsNoTracking()
                            .Where(x => x.RegisteredUserId == reguserid.RegisteredUserId)
                            .Where(x => x.IsRegistered == false || x.IsRegistered == null)
                            //.Where(x => x.OrgId == reguserid.OrgId)
                            .FirstOrDefault();

                        var updategd = new StudentGuardian
                        {
                            StudentGuardianId = locateGD.StudentGuardianId,
                            RegisteredUserId = locateGD.RegisteredUserId,
                            GuardianFirstName = locateGD.GuardianFirstName,
                            GuardianLastName = locateGD.GuardianLastName,
                            GuardianFullName = locateGD.GuardianFullName,
                            GuardianEmailAddress = locateGD.GuardianEmailAddress,
                            DateAdded = locateGD.DateAdded,
                            StudentId = locateGD.StudentId,
                            StudentFullName = locateGD.StudentFullName,
                            OrgId = locateGD.OrgId,
                            TitleId = locateGD.TitleId,
                            RelationshipId = locateGD.RelationshipId,
                            Telephone = locateGD.Telephone,
                            Stu_class_Org_Grp_id = locateGD.Stu_class_Org_Grp_id,
                            IsRegistered = true
                        };
                        locateGD = updategd;
                        db.Entry(locateGD).State = EntityState.Modified;
                        db.SaveChanges();

                    }

                    return RedirectToAction("PGSchCentre", "Orgs", new { id = orgredirect });
                }
                else
                {
                    return RedirectToAction("Index", "Orgs", new { id = orgredirect });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(registeredUser);

            }
        }


        // GET: Access/ForgottenPassword
        [Route("ForgottenPassword")]
        [HttpGet]
        public ActionResult ForgottenPassword()
        {
            return View();
        }


        // GET: Access/PasswordReset
        [HttpPost]
        public ActionResult PasswordReset(string email)
        {
            try
            {
                var Chkifemailexist = db.RegisteredUsers.Where(x => x.Email == email).FirstOrDefault();

                // Check if email exist, if no, Log attempt and exit
                if (Chkifemailexist == null)
                {
                    return View(email);
                }

                // Check if email is registered, if no - log and exit
                if (Chkifemailexist.IsRegistered == null)
                {                 
                    return View(email);
                }
                else
                {
                    //Email exists and registered? Log record & Send Email 
                    var triggeremail = SendPasswordResetEmail(Chkifemailexist.RegisteredUserId, Chkifemailexist.Email);
                    return new HttpStatusCodeResult(204);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(email);
            }
        }


        public JsonResult SendPasswordResetEmail(int id, string email)
        {
            try
            {

                //Encrypt ID
                var decrptedid = Encode(Convert.ToInt32(id).ToString());


                string Body = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/PasswordResetInstructions.html"));
                //Body = Body.Replace("#OrganisationName#", Session["OrgName"].ToString());
                //Body = Body.Replace("var(--white)", Session["regOrgBrandButtonColour"].ToString());
                //var orgName = Session["OrgName"].ToString();
                Body = Body.Replace("#DECRYPTEDKEY#", decrptedid.ToString());
                var subject = "Change Password";
                //var subject = "Change Password" + " " + orgName;

                bool result2 = false;
                result2 = SendEmailpw(email, subject, Body);
                return Json(result2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("~/ErrorHandler.html");
            }

        }

        public bool SendEmailpw(string toEmail, string subject, string Body)
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



        public string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }


        public ActionResult ChangePassword(string id)
        {
            try
            {
                //Decrypt ID
                var decrptedid = Decode(id);
                var newid = Convert.ToInt32(decrptedid);

                var Id = (int)newid;

                if (Id != 0)
                {
                    var newuser = db.RegisteredUsers.Where(x => x.RegisteredUserId == Id).FirstOrDefault();

                    var usr = new RegisteredUser
                    {
                        RegisteredUserId = newuser.RegisteredUserId,
                        RegisteredUserType = newuser.RegisteredUserType,
                        FirstName = newuser.FirstName,
                        LastName = newuser.LastName,
                        Email = newuser.Email,
                        Password = newuser.Password,
                        ConfirmPassword = newuser.ConfirmPassword,
                        Telephone = newuser.Telephone,
                        SelectedOrg = newuser.SelectedOrg,
                        ClassId = newuser.ClassId,
                        GenderId = newuser.GenderId,
                        TribeId = newuser.TribeId,
                        DateOfBirth = newuser.DateOfBirth,
                        EnrolmentDate = newuser.EnrolmentDate,
                        ReligionId = newuser.ReligionId,
                        PrimarySchoolUserRoleId = newuser.PrimarySchoolUserRoleId,
                        SecondarySchoolUserRoleId = newuser.SecondarySchoolUserRoleId,
                        StudentRegFormId = newuser.StudentRegFormId,
                        CreatedBy = newuser.CreatedBy,
                        RegUserOrgBrand = newuser.RegUserOrgBrand,
                        FullName = newuser.FullName,
                        IsTester = newuser.IsTester,
                        ClassRef = newuser.ClassRef,
                        TempIntHolder = newuser.TempIntHolder,
                        TitleId = newuser.TitleId,
                        RelationshipId = newuser.RelationshipId,
                        PgCount = newuser.PgCount,
                        NurserySchoolUserRoleId = newuser.NurserySchoolUserRoleId,
                        InviteKey = newuser.InviteKey,
                        InviteSentDate = newuser.InviteSentDate,
                        CountOfInvite = newuser.CountOfInvite,
                        IsRegistered = newuser.IsRegistered,
                        RegisteredDate = newuser.RegisteredDate,
                    };
                    return View(usr);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewPassword(RegisteredUser registeredUser)
        {
            try
            {
                var locateuser = db.RegisteredUsers.AsNoTracking().Where(x => x.RegisteredUserId == registeredUser.RegisteredUserId).FirstOrDefault();

                // Set Usr password 
                var usr = new RegisteredUser
                {
                    RegisteredUserId = locateuser.RegisteredUserId,
                    RegisteredUserTypeId = locateuser.RegisteredUserTypeId,
                    FirstName = locateuser.FirstName,
                    LastName = locateuser.LastName,
                    Email = locateuser.Email,
                    Password = registeredUser.Password,
                    ConfirmPassword = registeredUser.ConfirmPassword,
                    Telephone = locateuser.Telephone,
                    SelectedOrg = locateuser.SelectedOrg,
                    ClassId = locateuser.ClassId,
                    GenderId = locateuser.GenderId,
                    TribeId = locateuser.TribeId,
                    DateOfBirth = locateuser.DateOfBirth,
                    EnrolmentDate = locateuser.EnrolmentDate,
                    ReligionId = locateuser.ReligionId,
                    PrimarySchoolUserRoleId = locateuser.PrimarySchoolUserRoleId,
                    SecondarySchoolUserRoleId = locateuser.SecondarySchoolUserRoleId,
                    StudentRegFormId = locateuser.StudentRegFormId,
                    CreatedBy = locateuser.CreatedBy,
                    RegUserOrgBrand = locateuser.RegUserOrgBrand,
                    FullName = locateuser.FullName,
                    IsTester = locateuser.IsTester,
                    ClassRef = locateuser.ClassRef,
                    TempIntHolder = locateuser.TempIntHolder,
                    TitleId = locateuser.TitleId,
                    RelationshipId = locateuser.RelationshipId,
                    PgCount = locateuser.PgCount,
                    NurserySchoolUserRoleId = locateuser.NurserySchoolUserRoleId,
                    InviteKey = locateuser.InviteKey,
                    InviteSentDate = locateuser.InviteSentDate,
                    CountOfInvite = locateuser.CountOfInvite,
                    IsRegistered = locateuser.IsRegistered,
                    RegisteredDate = locateuser.RegisteredDate
                };
                locateuser = usr;
                db.Entry(locateuser).State = EntityState.Modified;
                db.SaveChanges();

                var userchangelog = new User_Change_Events_Log()
                {
                    RegUserId = registeredUser.RegisteredUserId,
                    ChangedBy = registeredUser.RegisteredUserId,
                    Old_Value = null,
                    New_Value = null,
                    OrgId = null,
                    User_Change_Event_Time = DateTime.Now,
                    User_Change_Events_Types = User_Change_Events_Types.ForgottenPassword,
                };
                db.User_Change_Events_Logs.Add(userchangelog);
                db.SaveChanges();

                return RedirectToRoute(new { controller = "Access", action = "Signin", });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Redirect("~/ErrorHandler.html");
            }
        }





        // GET: Access/Welcome
        public ActionResult Welcome()
        {
            return View();
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