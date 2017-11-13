using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharedProjectUploader.EntityConfiguration;

namespace SharedProjectUploader.Controllers
{
    public class HomeController : Controller
    {
        public object SharedProjectContext { get; private set; }

        public ActionResult Index()
        {
            var x = new SharedProjectsContext();
            var y = x.FileInformation.Include("CategoryInfo").ToList();
            var z = y[0].UserInfo;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}