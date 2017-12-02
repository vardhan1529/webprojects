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
            throw new Exception("test");
            var x = new SharedProjectsContext();
            IEnumerable<FileInformation> y1 = x.FileInformation.Where(m => !m.IsAbsolete);
            y1 = y1.OrderBy(m => m.Id).ToList();
            var y2 = x.FileInformation.Where(m => !m.IsAbsolete);
            y2 = y2.OrderBy(m => m.Id);
            foreach(var l in y2)
            {
                var m = l.Id;
            }
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