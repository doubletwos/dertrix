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
    public class PostTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PostTopics
        public ActionResult Index()
        {
            return View(db.PostTopics.ToList());
        }

        // GET: PostTopics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTopic postTopic = db.PostTopics.Find(id);
            if (postTopic == null)
            {
                return HttpNotFound();
            }
            return View(postTopic);
        }

        // GET: PostTopics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostTopicId,PostTopicName")] PostTopic postTopic)
        {
            if (ModelState.IsValid)
            {
                db.PostTopics.Add(postTopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postTopic);
        }

        // GET: PostTopics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTopic postTopic = db.PostTopics.Find(id);
            if (postTopic == null)
            {
                return HttpNotFound();
            }
            return View(postTopic);
        }

        // POST: PostTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostTopicId,PostTopicName")] PostTopic postTopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postTopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postTopic);
        }

        // GET: PostTopics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTopic postTopic = db.PostTopics.Find(id);
            if (postTopic == null)
            {
                return HttpNotFound();
            }
            return View(postTopic);
        }

        // POST: PostTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostTopic postTopic = db.PostTopics.Find(id);
            db.PostTopics.Remove(postTopic);
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
