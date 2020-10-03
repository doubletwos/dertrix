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
using Zeus.Models;

namespace Zeus.Controllers
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
                return RedirectToAction("Index", "Access");
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

            return PartialView("_RegisterUser");
        }




        [ChildActionOnly]
        public ActionResult Nav()
        {       
         return PartialView("_Nav");
        }


        [ChildActionOnly]
        public ActionResult AddStaff()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles, "SecondarySchoolUserRoleId", "RoleName");

            return PartialView("_AddStaff");
        }


        [ChildActionOnly]
        public ActionResult AddStudent()  
        {

            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            ViewBag.ClassId = new SelectList(db.Classes.Where(o => o.OrgId == i), "ClassId", "ClassName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles, "SecondarySchoolUserRoleId", "RoleName");
            ViewBag.ReligionId = new SelectList(db.Religions, "ReligionId", "ReligionName");
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderName");
            ViewBag.StudentRegFormId = new SelectList(db.StudentRegForm, "StudentRegFormId", "Name");
            ViewBag.TribeId = new SelectList(db.Tribes, "TribeId", "TribeName");

            return PartialView("_AddStudent");
        }


        [ChildActionOnly]
        public ActionResult AddSysAdmin()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            ViewBag.SelectedOrgList = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");

            return PartialView("_AddSysAdmin");
        }


        [ChildActionOnly]
        public ActionResult StudentCount()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

          
            var studentCount =  db.RegisteredUsers
                .Where(x => x.SelectedOrg == i)
                .Where(j => j.StudentRegFormId != null)
                .Where(c => c.ClassId != null)
                .ToList();

            return PartialView("_StudentCount" , studentCount);
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



        public ActionResult StaffDetails(int Id)
        {
            var stud = db.RegisteredUsers
                .Where(x => x.RegisteredUserId == Id);

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
                    FullName = stud1.FullName
                };
                return PartialView("_EditStudent", stud);
            }
            return PartialView("_EditStudent");
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
                ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName", stud1.ClassId);
                var stud = new RegisteredUser
                {
                    RegisteredUserId = stud1.RegisteredUserId,
                    RegisteredUserTypeId = stud1.RegisteredUserTypeId,
                    FirstName = stud1.FirstName,
                    LastName = stud1.LastName,
                    Email = stud1.Email,
                    Password = stud1.Password,
                    ConfirmPassword = stud1.ConfirmPassword,
                    Telephone =stud1.Telephone,
                    SelectedOrg = stud1.SelectedOrg,
                    ClassId = stud1.ClassId,
                    EnrolmentDate = stud1.EnrolmentDate,
                    CreatedBy = stud1.CreatedBy,
                    PrimarySchoolUserRoleId = stud1.PrimarySchoolUserRoleId,
                    SecondarySchoolUserRoleId = stud1.SecondarySchoolUserRoleId,
                    FullName = stud1.FullName,
                    RegUserOrgBrand = stud1.RegUserOrgBrand
                    

                };
                return PartialView("_EditStaff", stud);
            }
            return PartialView("_EditStaff");
        }











        // GET: RegisteredUsers/Students/
        public ActionResult Students(int? id, int? ij, string searchname, string searchid)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            id = i;

            if (ij == null)
            {
                ij = 1;
            }

            var j = db.Classes.Where(s => s.OrgId == id && ij == s.ClassRefNumb).Select(g => g.ClassId).FirstOrDefault();

            var stds = db.RegisteredUsers.Where(s => s.RegisteredUserTypeId == 2).Where(p => p.ClassId == j).Include(c => c.Class).Include(g => g.Gender).Include(n => n.Class).ToList();

            string classid = ij.ToString();

         

            // returns students of org if class is selected
            if (string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid) && (!string.IsNullOrWhiteSpace(classid)))
            {
                return View(db.RegisteredUsers.Where(p => p.ClassId == j).Where(s => s.RegisteredUserTypeId == 2).Include(c => c.Class).Include(g => g.Gender).ToList());
            }

            // returns students of org if fullname is provided
            if (!string.IsNullOrWhiteSpace(searchname) && string.IsNullOrWhiteSpace(searchid))
            {
                return View(db.RegisteredUsers.Where(n => n.FullName == searchname).Where(s => s.RegisteredUserTypeId == 2).Where(o => o.SelectedOrg == i).Where(p => p.StudentRegFormId != null).ToList());

            }

            // returns students of org if studentid is provided
            if (string.IsNullOrWhiteSpace(searchname) && !string.IsNullOrWhiteSpace(searchid))
            {
                int reguserid = Convert.ToInt32(searchid);
                return View(db.RegisteredUsers.Where(n => n.RegisteredUserId == reguserid).Where(s => s.RegisteredUserTypeId == 2).Where(o => o.SelectedOrg == i).Where(p => p.StudentRegFormId != null).ToList());

            }

            return View(stds);
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
        public ActionResult Staffs(int? id, string searchname, string searchid)
        {
            /* Redirect back to Log in Page if session == null*/
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            /* Populate user list for non superusers if session is not null*/
            if (Session["OrgId"] != null)
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                id = i;
            }





            /*Returns Zeus staff - if & when name is provided*/
            if ((int)Session["OrgId"] == 23 && !string.IsNullOrWhiteSpace(searchname))
            {
                return View(db.RegisteredUsers.Where(j => j.SelectedOrg == id).Where(f => f.FullName == searchname).Include(t => t.RegisteredUserType).ToList());

            }

            /*Returns Zeus staff - if & when id is provided*/
            if ((int)Session["OrgId"] == 23 && !string.IsNullOrWhiteSpace(searchid))
            {
                int reguserid = Convert.ToInt32(searchid);
                return View(db.RegisteredUsers.Where(j => j.SelectedOrg == id).Where(r => r.RegisteredUserId == reguserid).Include(t => t.RegisteredUserType).ToList());

            }


            /*Returns Zeus staff - upon page load*/
            if ((int)Session["OrgId"] == 23 && string.IsNullOrWhiteSpace(searchname))
            {
                return View(db.RegisteredUsers.Where(j => j.SelectedOrg == id).Include(t => t.RegisteredUserType).ToList());

            }




            /*Returns NON Zeus staff - if & when name is provided*/
            if ((int)Session["OrgId"] != 23 && !string.IsNullOrWhiteSpace(searchname))
            {
                return View(db.RegisteredUsers.Where(j => j.SelectedOrg == id).Where(f => f.FullName == searchname).Where(p => p.StudentRegFormId == null).Include(t => t.RegisteredUserType).ToList());

            }


            /*Returns NON Zeus staff - if & when id is provided*/
            if ((int)Session["OrgId"] != 23 && !string.IsNullOrWhiteSpace(searchid))
            {
                int reguserid = Convert.ToInt32(searchid);
                return View(db.RegisteredUsers.Where(j => j.SelectedOrg == id).Where(r => r.RegisteredUserId == reguserid).Where(p => p.StudentRegFormId == null).Include(t => t.RegisteredUserType).ToList());

            }


            /*Returns NON Zeus staff - upon page load*/
            if ((int)Session["OrgId"] != 23 && string.IsNullOrWhiteSpace(searchname))
            {
                return View(db.RegisteredUsers.Where(j => j.SelectedOrg == id).Where(p => p.StudentRegFormId == null).Include(t => t.RegisteredUserType).Include(s => s.SecondarySchoolUserRole).Include(s => s.PrimarySchoolUserRole)
                    .ToList());

            }



            return View(db.RegisteredUsers.Where(s => s.RegisteredUserTypeId == 2).Where(p => p.ClassId == id).Include(c => c.Class).ToList());

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












        // GET: RegisteredUsers/Details/5
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
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            return View(registeredUser);
        }

        // GET: RegisteredUsers/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.ClassId = new SelectList(db.Classes.Where(o => o.ClassId == 17) , "ClassId", "ClassName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName");
            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.SecondarySchoolUserRoleId = new SelectList(db.SecondarySchoolUserRoles, "SecondarySchoolUserRoleId", "RoleName");
            ViewBag.SelectedOrgList = new SelectList(db.Orgs, "OrgId", "OrgName");
            return View();
        }


        // POST: RegisteredUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisteredUser registeredUser)
        {
            /*Accepting all state of model*/
            if (!(ModelState.IsValid) || ModelState.IsValid)
            {
                //Add existing user to another organsation 
                var checkemail = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(x => x.Email).FirstOrDefault();
                var firstname = db.RegisteredUsers.Where(x => x.FirstName == registeredUser.FirstName).Select(x => x.FirstName).FirstOrDefault();
                var lastname = db.RegisteredUsers.Where(x => x.LastName == registeredUser.LastName).Select(x => x.LastName).FirstOrDefault();



                if (checkemail != null && checkemail == registeredUser.Email && firstname == registeredUser.FirstName && lastname == registeredUser.LastName)
                {
                    registeredUser.RegisteredUserId = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(i => i.RegisteredUserId).FirstOrDefault();
                    registeredUser.RegisteredUserTypeId = 2;
                    registeredUser.FirstName = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(i => i.FirstName).FirstOrDefault();
                    registeredUser.LastName = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(i => i.LastName).FirstOrDefault();
                    registeredUser.Email = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(i => i.Email).FirstOrDefault();
                    registeredUser.Password = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(i => i.Password).FirstOrDefault();
                    registeredUser.ConfirmPassword = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(i => i.ConfirmPassword).FirstOrDefault();
                    registeredUser.Telephone = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(i => i.Telephone).FirstOrDefault();
                    var zeususer = registeredUser.SelectedOrgList.FirstOrDefault().ToString();
                    int k = Convert.ToInt32(zeususer);
                    registeredUser.SelectedOrg = k;
                    registeredUser.PrimarySchoolUserRoleId = 3;
                    registeredUser.SecondarySchoolUserRoleId = 4;
                    registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                    registeredUser.FullName = db.RegisteredUsers.Where(x => x.Email == registeredUser.Email).Select(i => i.FullName).FirstOrDefault();
                    registeredUser.RegUserOrgBrand = db.Orgs.Where(x => x.OrgId == k).Select(i => i.OrgBrandId).FirstOrDefault();
                    registeredUser.IsTester = true;


                    /*Adding users to the RegUserOrg(Many to Many)*/
                    var onetomany = new RegisteredUserOrganisation()
                    {
                        RegisteredUserId = registeredUser.RegisteredUserId,
                        OrgId = registeredUser.SelectedOrg,
                        Email = registeredUser.Email,
                        FirstName = registeredUser.FirstName,
                        LastName = registeredUser.LastName,
                        OrgName = db.Orgs.Where(x => x.OrgId == registeredUser.SelectedOrg).Select(x => x.OrgName).FirstOrDefault(),
                        RegUserOrgBrand = registeredUser.RegUserOrgBrand,
                        RegisteredUserTypeId = registeredUser.RegisteredUserTypeId,
                        IsTester = registeredUser.IsTester

                    };
                    db.RegisteredUserOrganisations.Add(onetomany);
                    db.SaveChanges();
                    return RedirectToAction("Students", "RegisteredUsers");



                }

          



                /*When users are added at Zeus Level*/
                if (registeredUser.SelectedOrgList != null)
                {
                    var zeususer = registeredUser.SelectedOrgList.FirstOrDefault().ToString();
                    int i = Convert.ToInt32(zeususer);
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

                   
                }

                /*When users are added at school level*/
                if (registeredUser.SelectedOrgList == null)
                {
                    registeredUser.SelectedOrg = (int)Session["OrgId"];
                    var email = "iamanewuser@thisorg.com";
                    registeredUser.Email = email;
                    var pwd = "iamanewuser";
                    registeredUser.Password = pwd;
                    registeredUser.ConfirmPassword = pwd;
                    registeredUser.FullName = registeredUser.ContactFullName;
                    registeredUser.RegisteredUserTypeId = 2;
                    registeredUser.CreatedBy = Session["RegisteredUserId"].ToString();
                    registeredUser.EnrolmentDate = DateTime.Now;
                    var regUserOrgBrand = db.Orgs.Where(x => x.OrgId == registeredUser.SelectedOrg).Select(x => x.OrgBrandId).FirstOrDefault();
                    int j = Convert.ToInt32(regUserOrgBrand);
                    registeredUser.RegUserOrgBrand = j;




                }

                db.RegisteredUsers.Add(registeredUser);
                db.SaveChanges();






                /*Adding users to the RegUserOrg(Many to Many)*/
                var objRegisteredUserOrganisations = new RegisteredUserOrganisation()
                {
                    RegisteredUserId = registeredUser.RegisteredUserId,
                    OrgId = registeredUser.SelectedOrg,
                    Email = registeredUser.Email,
                    FirstName = registeredUser.FirstName,
                    LastName = registeredUser.LastName,
                    OrgName = db.Orgs.Where(x => x.OrgId == registeredUser.SelectedOrg).Select(x => x.OrgName).FirstOrDefault(),
                    RegUserOrgBrand = registeredUser.RegUserOrgBrand,

                };
                db.RegisteredUserOrganisations.Add(objRegisteredUserOrganisations);
                db.SaveChanges();
                return RedirectToAction("Students", "RegisteredUsers");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "ClassId", "ClassName");
            ViewBag.SelectedOrgList = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName", registeredUser.RegisteredUserTypeId);
            return View(registeredUser);
        }



        // GET: RegisteredUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            registeredUser.SelectedOrg = (int)Session["OrgId"];

            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName", registeredUser.RegisteredUserTypeId);
            return View(registeredUser);
        }

        // POST: RegisteredUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( RegisteredUser registeredUser)
        {

            if(registeredUser.StudentRegFormId == 1)
            {
                registeredUser.Email  = "iamanewuser@thisorg.com";

            }



            if (!(ModelState.IsValid) || ModelState.IsValid)
            {
                
                registeredUser.SelectedOrg = (int)Session["OrgId"];
                db.Entry(registeredUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrimarySchoolUserRoleId = new SelectList(db.PrimarySchoolUserRoles, "PrimarySchoolUserRoleId", "RoleName");
            ViewBag.RegisteredUserTypeId = new SelectList(db.RegisteredUserTypes, "RegisteredUserTypeId", "RegisteredUserTypeName", registeredUser.RegisteredUserTypeId);
            return View(registeredUser);
        }

        // GET: RegisteredUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisteredUser registeredUser = db.RegisteredUsers.Find(id);
            if (registeredUser == null)
            {
                return HttpNotFound();
            }
            return View(registeredUser);
        }

        // POST: RegisteredUsers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
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
