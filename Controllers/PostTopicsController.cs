﻿using System;
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
            if (Request.Browser.IsMobileDevice == true)
            {
                return RedirectToAction("WrongDevice", "Orgs");
            }
            if (Session["OrgId"] == null)
            {
                return RedirectToAction("Signin", "Access");
            }
            if ((int)Session["OrgId"] != 23)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.PostTopics.ToList());
        }

        [ChildActionOnly]
        public ActionResult AddPostTopic()
        {
            return PartialView("~/Views/Shared/PartialViewsForms/_AddPostTopic.cshtml");
        }

        public ActionResult EditPostTopic(int Id)
        {
            if (Id != 0)
            {
                var edtPostTopic = db.PostTopics.Where(x => x.PostTopicId == Id).FirstOrDefault();
                var edtPostTopic1 = new PostTopic
                {
                    PostTopicId = edtPostTopic.PostTopicId,
                    PostTopicName = edtPostTopic.PostTopicName
                };
                return PartialView("~/Views/Shared/PartialViewsForms/_EditPostTopic.cshtml", edtPostTopic1);
            }
            return PartialView("~/Views/Shared/PartialViewsForms/_EditPostTopic.cshtml");
        }



        // POST: PostTopics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostTopic postTopic)
        {
            if (ModelState.IsValid)
            {
                db.PostTopics.Add(postTopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postTopic);
        }

        // POST: PostTopics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostTopic postTopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postTopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postTopic);
        }

  
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