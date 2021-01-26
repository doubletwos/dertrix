using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;
using Dertrix.ViewModels;

namespace Dertrix.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if(Session["IsParent/Guardian"] != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var posts = db.Posts.Where(x => x.OrgId == i).Include(p => p.Org).Include(p => p.PostTopic);
            return View(posts.ToList());
        }


        public ActionResult PostsTable() 
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var posts = db.Posts.Where(x => x.OrgId == i).Include(p => p.Org).Include(p => p.PostTopic);
            return View(posts.ToList());
        }

        public ActionResult EditPost(int? Id)
        {
            if(Id != 0)
            {
                var edtpost = db.Posts
                    .Include(p => p.PostTopic)
                    .Where(x => x.PostId == Id).FirstOrDefault();
                Post Post = db.Posts.Find(Id);
                var edtpost1 = new Post
                {
                    PostId = edtpost.PostId,
                    PostTopicId = edtpost.PostId,
                    OrgId = edtpost.OrgId,
                    PostSubject = edtpost.PostSubject,
                    PostCreatorId = edtpost.PostCreatorId,
                    CreatorFullName = edtpost.CreatorFullName,
                    PostCreationDate = edtpost.PostCreationDate,
                    PostExpirtyDate = edtpost.PostExpirtyDate,
                    PostContent = edtpost.PostContent
                };
                ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName", Post.PostTopicId);
                return PartialView("~/Views/Shared/PartialViewsForms/_EditPost.cshtml", edtpost1);

            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditPost.cshtml");
        }



        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName");
            return View();
        }
        //  GET: Posts/CreatePost
        [ChildActionOnly]
        public ActionResult AddPost()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }



            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName");
            return PartialView("~/Views/Shared/PartialViewsForms/_AddPost.cshtml" );
        }


        [ChildActionOnly]
        public ActionResult AddPost1()
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








        //  GET: Posts/DisplayPanel
        [ChildActionOnly]
        public ActionResult PostDisplayPanel()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var posts = db.Posts.Where(x => x.OrgId == i).Include(p => p.Org).Include(p => p.PostTopic);
            return PartialView("~/Views/Shared/PartialViewsForms/_PostDisplayPanel.cshtml", posts);
        }


        // POST: Posts/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            var RegisteredUserId = Convert.ToInt32(Session["RegisteredUserId"]);
            var OrgId = Convert.ToInt32(Session["OrgId"]);
            post.PostCreatorId = RegisteredUserId;
            post.OrgId = OrgId;
            post.CreatorFullName = db.RegisteredUsers.Where(x => x.RegisteredUserId == RegisteredUserId).Select(x => x.FullName).FirstOrDefault();
            post.PostCreationDate = DateTime.Now;
            if (!(ModelState.IsValid) || ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", post.OrgId);
            ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName", post.PostTopicId);
            return View(post);
        }



        // POST: Posts/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create1( AddNewPostViewModel viewmodel)
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
                //return RedirectToAction("Index");
            }

            // Send Post as email if Send as Email is True
            if(viewmodel.Post.SendAsEmail == true)
            {
                var send = SendTestEmail(viewmodel.Post.PostContent, viewmodel.Post.PostSubject);

            }

            //selected org list
            var selectedgroups = viewmodel.OrgGroups.Where(x => x.IsSelected == true).Select(x => x.OrgGroupId).ToList();
            var selectedgroupid = new List<int>(selectedgroups);

      

            return View(viewmodel);
        }


        public JsonResult SendTestEmail(string postcontent, string postsubject)
        {
            string Body = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/HtmlPage.html"));
            Body = Body.Replace("#OrganisationName#", Session["OrgName"].ToString());
            Body = Body.Replace("var(--white)", Session["regUserOrgNavBar"].ToString());
            Body = Body.Replace("#Body#", postcontent);
            Body = Body.Replace("#Subject#", postsubject);

            bool result = false;
            result = SendEmail("epiphany04203@hotmail.com", postsubject, Body);
            //result = SendEmail("wilsonwales@gmail.com", postsubject, Body);

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








































        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", post.OrgId);
            ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName", post.PostTopicId);
            return View(post);
        }
        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreatorFullName = db.RegisteredUsers.Where(x => x.RegisteredUserId == post.PostCreatorId).Select(x => x.FullName).FirstOrDefault();
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName", post.OrgId);
            ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName", post.PostTopicId);
            return View(post);
        }
        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        // POST: Posts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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