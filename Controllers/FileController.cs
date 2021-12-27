using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;

namespace Dertrix.Controllers
{
    public class FileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: File
        public ActionResult Index(int id)
        {
            try
            {
                var fileToRetrieve = db.Files.Find(id);
                return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("~/ErrorHandler.html");
            }

        }
    }
}