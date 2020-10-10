using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zeus.Models;

namespace Zeus.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }


            var posts = db.Posts.Include(p => p.Org).Include(p => p.PostTopic);
            return View(posts.ToList());
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
        public ActionResult CreatePost()
        {
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Index", "Access");
            }



            ViewBag.OrgId = new SelectList(db.Orgs, "OrgId", "OrgName");
            ViewBag.PostTopicId = new SelectList(db.PostTopics, "PostTopicId", "PostTopicName");

            return PartialView("_CreatePost");

        }


        //  GET: Posts/DisplayPanel
        [ChildActionOnly]
        public ActionResult PostDisplayPanel()
        {
            var rr = Session["OrgId"].ToString();
            int i = Convert.ToInt32(rr);

            var posts = db.Posts.Include(p => p.Org).Include(p => p.PostTopic);


            return PartialView("_PostDisplayPanel" , posts);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,PostTopicId,OrgId,PostSubject,PostCreatorId,CreatorFullName,PostCreationDate,PostExpirtyDate")] Post post)
        {
            if (ModelState.IsValid)
            {
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
