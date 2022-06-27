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
    [RoutePrefix("")]
    public class PostTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PostTopics
        [Route("PostTopics")]
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
                    return RedirectToRoute(new { controller = "Access",  action = "Signin", });
                }
                if ((int)Session["OrgId"] != 23)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(db.PostTopics.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
        }

        [ChildActionOnly]
        public ActionResult AddPostTopic()
        {
            try
            {
                return PartialView("~/Views/Shared/PartialViewsForms/_AddPostTopic.cshtml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }

        public ActionResult EditPostTopic(int Id)
        {
            try
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }



        // POST: PostTopics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostTopic postTopic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PostTopics.Add(postTopic);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View(postTopic);
            }
            return View(postTopic);

        }

        // POST: PostTopics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostTopic postTopic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(postTopic).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }
            return new HttpStatusCodeResult(204);
        }

  


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        PostTopic postTopic = db.PostTopics.Find(id);
        //        db.PostTopics.Remove(postTopic);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return Redirect("~/ErrorHandler.html");
        //    }
        //}
    }
}