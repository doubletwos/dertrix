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
namespace Dertrix.Controllers
{
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
                    return RedirectToAction("Signin", "Access");
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
                    return RedirectToAction("Signin", "Access");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }



        // GET: Access/Logs
        public ActionResult Logs()
        {
            try
            {
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
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


        // GET: Access/Signin
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(RegisteredUser registeredUser)
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
                    return RedirectToAction("SystemAdminIndex", "Orgs", new { id = orgredirect });
                }

                if (Session["IsParent/Guardian"] != null)
                {
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
                return Redirect("~/ErrorHandler.html");

            }
        }

        // GET: Access/Welcome
        public ActionResult Welcome()
        {
            return View();
        }

        // GET: Access/Welcome
        public ActionResult Register() 
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