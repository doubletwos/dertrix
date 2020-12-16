using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;
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
            var posts = db.Posts.Include(p => p.Org).Include(p => p.PostTopic);
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
            return PartialView("~/Views/Shared/PartialViewsForms/_AddPost.cshtml");
        }
        //  GET: Posts/DisplayPanel
        [ChildActionOnly]
        public ActionResult PostDisplayPanel()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);
            var posts = db.Posts.Include(p => p.Org).Include(p => p.PostTopic);
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