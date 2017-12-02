using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ElmahMvc.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            //SqlConnection s = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
            //s.Open();
            var x = ApplicationManager.GetApplicationManager().GetRunningApplications();
            ViewBag.HostingEnvironmentID = HostingEnvironment.ApplicationID;
            ViewBag.HostEnvironmentSiteName = HostingEnvironment.SiteName;
            ViewBag.PThreadId = Thread.CurrentThread.ManagedThreadId;
            await Task.Run(() => { Thread.Sleep(3000); });
            ViewBag.Context = System.Web.HttpContext.Current == null;
            ViewBag.ThreadId = Thread.CurrentThread.ManagedThreadId;
            ViewBag.AppDoamin = AppDomain.CurrentDomain.FriendlyName;
            ViewBag.AppDoaminId = AppDomain.CurrentDomain.Id;
            
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