using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;
using Dertrix.ViewModels;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;

namespace Dertrix.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult AllPosts()
        {
            try
            {
                if ((int)Session["IsAdmin"] == 1 || Session["IsTester"] != null)
                { 
                }

                    if (Request.Browser.IsMobileDevice == true)
                {
                    return RedirectToAction("WrongDevice", "Orgs");
                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                if (Session["IsParent/Guardian"] != null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                var posts = db.Posts
                    .Where(x => x.OrgId == i)
                    .Include(p => p.Org)
                    .Include(p => p.PostTopic);

                return View(posts.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        public ActionResult PostsTable()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

            var usersposts = (from psts in db.Posts
                              join pstgrp in db.OrgSchPostGrps on psts.PostId equals pstgrp.OrgPostId
                              join rug in db.RegisteredUsersGroups on pstgrp.OrgGroupId equals rug.OrgGroupId
                              join ru in db.RegisteredUsers on rug.RegisteredUserId equals ru.RegisteredUserId
                              where ru.RegisteredUserId == RegisteredUserId
                              where psts.OrgId == i
                              select psts)
                              .Distinct()
                              .ToList();

            return View(usersposts);
        }

        public ActionResult EditPost(int? Id)
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
                {
                    return RedirectToAction("WrongDevice", "Orgs");

                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);

                var edtpost = db.Posts
                       .Include(p => p.PostTopic)
                       .Where(x => x.PostId == Id)
                       .Where(x => x.OrgId == i)
                       .FirstOrDefault();


                var grp = db.OrgGroups.Where(c => c.OrgId == i).ToList();

                var posttopics = db.PostTopics.ToList();

                var editpostviewmodel = new EditPostViewModel
                {
                    PostId = edtpost.PostId,
                    PostTopicId = edtpost.PostTopicId,
                    OrgId = edtpost.OrgId,
                    PostCreatorId = edtpost.PostCreatorId,
                    CreatorFullName = edtpost.CreatorFullName,
                    PostCreationDate = edtpost.PostCreationDate,
                    PostExpirtyDate = edtpost.PostExpirtyDate,
                    PostContent = edtpost.PostContent,
                    SendAsEmail = edtpost.SendAsEmail,
                    PostSubject = edtpost.PostSubject,

                    OrgGroups = grp.Select(x => new OrgGroup()
                    {
                        OrgGroupId = x.OrgGroupId,
                        OrgId = x.OrgId,
                        GroupName = x.GroupName,
                        IsSelected = x.IsSelected
                    }).ToList()
                };

                foreach (var group in editpostviewmodel.OrgGroups)
                {
                    int groupCount = editpostviewmodel.OrgGroups.Count();
                }
                foreach (var item in editpostviewmodel.OrgGroups)
                {
                    var isselected = db.OrgSchPostGrps
                        .Where(x => x.OrgGroupId == item.OrgGroupId)
                        .Where(x => x.OrgPostId == Id)
                        .Where(X => X.OrgId == i)
                        .Select(x => x.OrgSchPostGrpId)
                        .Count();

                    if (isselected == 0)
                    {
                        item.IsSelected = false;
                    }
                    else
                    {
                        item.IsSelected = true;
                    }

                }
                ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName", edtpost.PostTopicId);
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
                return PartialView("~/Views/Shared/PartialViewsForms/_EditPost.cshtml", editpostviewmodel);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }


        public ActionResult DownloadPostPdf(int? id)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);

                //Get Post 
                var postbody = db.Posts.Where(x => x.PostId == id)
                    .Where(x => x.OrgId == i)
                    .Select(x => x.PostContent)
                    .FirstOrDefault();

                //Get Post Subject 
                var postsubject = db.Posts.Where(x => x.PostId == id)
                    .Where(x => x.OrgId == i)
                    .Select(x => x.PostSubject)
                    .FirstOrDefault();

                PdfDocument pdf = new PdfDocument();
                PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
                htmlLayoutFormat.IsWaiting = false;
                PdfPageSettings setting = new PdfPageSettings();
                setting.Size = PdfPageSize.A4;
                string htmlCode = postbody;
                Thread thread = new Thread(() => { pdf.LoadFromHTML(htmlCode, false, setting, htmlLayoutFormat); });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();

                var filePath = Server.MapPath("~/Files/UploadedFiles/");
                var fileName = postsubject + ".pdf";

                filePath = filePath + Path.GetFileName(fileName);
                pdf.SaveToFile(filePath);
                return File(filePath, "application/pdf", postsubject + ".pdf");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        // GET: Posts/EmptyUploadedFiles
        public ActionResult EmptyUploadedFiles()
        {
            try
            {
                var filecount = Server.MapPath("~/Files/UploadedFiles/").Count();
                var filePath = Server.MapPath("~/Files/UploadedFiles/");
                string[] filePath1 = Directory.GetFiles(filePath);

                if (filecount > 0)
                {
                    foreach (var file in filePath1)
                    {
                        Console.WriteLine(Path.GetFileName(file));
                        System.IO.File.Delete((Path.Combine(filePath, file)));

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }


        [ChildActionOnly]
        public ActionResult AddPost1()
        {
            try
            {
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);
                var post = new Post();
                // Get all the groups from the database
                var grp = db.OrgGroups.Where(c => c.OrgId == i).ToList();
                // Get all the posttopics from the database
                var posttopics = db.PostTopics.ToList();
                // Initialize the view model
                var viewmodel = new AddNewPostViewModel
                {
                    Post = post,
                    PostTopics = posttopics,
                    OrgGroups = grp.Select(x => new OrgGroup()
                    {
                        OrgGroupId = x.OrgGroupId,
                        OrgId = x.OrgId,
                        GroupName = x.GroupName
                    }).ToList()
                };
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", post.OrgId);
                ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName", post.PostTopicId);
                return PartialView("~/Views/Shared/PartialViewsForms/_AddPost1.cshtml", viewmodel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }




        //  GET: Posts/DisplayPanel
        [ChildActionOnly]
        public ActionResult PostDisplayPanel()
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);

                var usersposts =  (from psts in db.Posts
                                  join pstgrp in db.OrgSchPostGrps on psts.PostId equals pstgrp.OrgPostId
                                  join rug in db.RegisteredUsersGroups on pstgrp.OrgGroupId equals rug.OrgGroupId
                                  join ru in db.RegisteredUsers on rug.RegisteredUserId equals ru.RegisteredUserId
                                  where ru.RegisteredUserId == RegisteredUserId
                                  where psts.OrgId == i
                                  select psts)
                                  .Distinct()
                                  .ToList();










                //var posts = db.Posts.Where(x => x.OrgId == i).Include(p => p.Org).Include(p => p.PostTopic);
                return PartialView("~/Views/Shared/DisplayViews/_PostDisplayPanel.cshtml", usersposts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }



        // POST: Posts/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(AddNewPostViewModel viewmodel)
        {
            try
            {
                var rr = Session["OrgId"].ToString();
                int i = Convert.ToInt32(rr);
                var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
                var SessionId = Convert.ToInt32(Session["SessionId"]);

                viewmodel.Post.PostCreatorId = RegisteredUserId;
                viewmodel.Post.OrgId = i;
                viewmodel.Post.CreatorFullName = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
                viewmodel.Post.PostCreationDate = DateTime.Now;


                // Adding / Saving the Post
                if (!(ModelState.IsValid) || ModelState.IsValid)
                {
                    db.Posts.Add(viewmodel.Post);
                    db.SaveChanges();

                    // UPON CREATING A POST - LOG THE EVENT 
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_SubjectId = viewmodel.Post.PostId.ToString(),
                        Org_Event_SubjectName = viewmodel.Post.PostSubject,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString(),
                        Org_Events_Types = Org_Events_Types.Created_Post
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();

                    var grps = viewmodel.OrgGroups.Select(x => x.OrgGroupId).ToList();
                    var grpstolist = new List<int>(grps);

                    foreach (var grp in grps)
                    {
                        // GET VALUE OF IS-SELECTED
                        var isselected = viewmodel.OrgGroups.Where(x => grp == x.OrgGroupId).Select(x => x.IsSelected).FirstOrDefault();
                        if (isselected == true)
                        {
                            var orgPostGrps = new OrgSchPostGrp()
                            {
                                OrgGroupId = grp,
                                OrgId = i,
                                OrgPostId = viewmodel.Post.PostId,

                            };
                            db.OrgSchPostGrps.Add(orgPostGrps);
                            db.SaveChanges();
                        }
                    }
                }

             
                // Send Post as email if Send as Email is True
                if (viewmodel.Post.SendAsEmail == true)
                {
                    var send = SendTestEmail(viewmodel.Post.PostContent, viewmodel.Post.PostSubject);

                }
                //selected org list
                var selectedgroups = viewmodel.OrgGroups.Where(x => x.IsSelected == true).Select(x => x.OrgGroupId).ToList();
                var selectedgroupid = new List<int>(selectedgroups);

                return RedirectToAction("Index", "Orgs", new { id = i });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(viewmodel);
            }
        }


        public JsonResult SendTestEmail(string postcontent, string postsubject)
        {

            string Body = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/HtmlPage.html"));
            Body = Body.Replace("#OrganisationName#", Session["OrgName"].ToString());
            Body = Body.Replace("var(--white)", Session["regOrgBrandButtonColour"].ToString());
            Body = Body.Replace("#Body#", postcontent);
            Body = Body.Replace("#Subject#", postsubject);

            bool result = false;
            //result = SendEmail("epiphany04203@hotmail.com", postsubject, Body);
            result = SendEmail("wilsonwales@gmail.com", postsubject, Body);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public bool SendEmail(string toEmail, string postsubject, string Body)
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
                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, postsubject, Body);
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



        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            try
            {
                if (Request.Browser.IsMobileDevice == true && Session["IsTester"] == null)
                {
                    return RedirectToAction("WrongDevice", "Orgs");

                }
                if (Session["OrgId"] == null)
                {
                    return RedirectToAction("Signin", "Access");
                }
                var sess = Session["OrgId"].ToString();
                int i = Convert.ToInt32(sess);



                    // LOOP THRU LIST OF RECORD IN TABLE AND REMOVE
                    var postgrps = db.OrgSchPostGrps
                        .Where(x => x.OrgPostId == post.PostId)
                        .Where(x => x.OrgId == i)
                        .Select(x => x.OrgSchPostGrpId)
                        .ToList();

                    var orgposttolist = new List<int>(postgrps);

                    foreach (var recrd in postgrps)
                    {
                        var removercrd = db.OrgSchPostGrps
                            .Where(x => x.OrgSchPostGrpId == recrd)
                            .Where(x => x.OrgId == i)
                            .Select(x => x.OrgSchPostGrpId)
                            .FirstOrDefault();

                        OrgSchPostGrp orgpostgrp = db.OrgSchPostGrps.Find(removercrd);
                        db.OrgSchPostGrps.Remove(orgpostgrp);
                    }


                // LOOP THRU LIST OF GROUPS PROVIDED
                var grps = post.OrgGroups.Select(x => x.OrgGroupId).ToList();
                var grpstolist = new List<int>(grps);

                foreach (var grp in grps)
                {
                    // GET VALUE OF IS-SELECTED
                    var isselected = post.OrgGroups
                        .Where(x => grp == x.OrgGroupId)
                        .Select(x => x.IsSelected)
                        .FirstOrDefault();

                    if (isselected == true)
                    {
                        var orgPostGrps = new OrgSchPostGrp()
                        {
                            OrgGroupId = grp,
                            OrgId = i,
                            OrgPostId = post.PostId
                        };
                        db.OrgSchPostGrps.Add(orgPostGrps);
                        db.SaveChanges();
                    }
                }
                if (!(ModelState.IsValid) || ModelState.IsValid)
                {
                    db.Entry(post).State = EntityState.Modified;
                    db.SaveChanges();

                    // UPON EDITING A POST  - LOG THE EVENT 
                    var orgeventlog = new Org_Events_Log()
                    {
                        Org_Event_SubjectId = post.PostId.ToString(),
                        Org_Event_SubjectName = post.PostSubject,
                        Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                        Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                        Org_Event_Time = DateTime.Now,
                        OrgId = Session["OrgId"].ToString(),
                        Org_Events_Types = Org_Events_Types.Edited_Post
                    };
                    db.Org_Events_Logs.Add(orgeventlog);
                    db.SaveChanges();
                    return RedirectToAction("AllPosts");
                }
                ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName", post.PostTopicId);
                ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", post.OrgId);
                return View(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }



        // POST: Posts/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Post post = db.Posts.Find(id);
                db.Posts.Remove(post);
                db.SaveChanges();

                // UPON DELETING A POST - LOG THE EVENT 
                var orgeventlog = new Org_Events_Log()
                {
                    Org_Event_SubjectId = id.ToString(),
                    Org_Event_SubjectName = post.PostSubject,
                    Org_Event_TriggeredbyId = Session["RegisteredUserId"].ToString(),
                    Org_Event_TriggeredbyName = Session["FullName"].ToString(),
                    Org_Event_Time = DateTime.Now,
                    OrgId = Session["OrgId"].ToString(),
                    Org_Events_Types = Org_Events_Types.Deleted_Post
                };
                db.Org_Events_Logs.Add(orgeventlog);
                db.SaveChanges();
                return RedirectToAction("AllPosts");
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