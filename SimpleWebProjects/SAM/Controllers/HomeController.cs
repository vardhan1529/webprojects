
using SAM.Filters;
using SAM.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAM.Controllers
{
    //[Authorize]
    [CustomActionFilter]
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index()
        {
            //HttpClient cl = HttpClientFactory.GetHttpClient();
            //cl.BaseAddress = new Uri("http://localhost:59577/");
            //var x = cl.GetAsync("api/values").Result;
            //var c = ClaimsPrincipal.Current.Claims;
            //var t2 = await t().ConfigureAwait(false);
            //t1.Start();
            var y = t().Result;
            return View();
        }

        public async Task<string> t()
        {
            System.Threading.Thread.Sleep(1000);
            Task<string> t = new Task<string>(() => { return "false"; });
            t.Start();

            return await t.ConfigureAwait(false);
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

        [ChildActionOnly]
        public ActionResult ChildActionPartial()
        {
            return new EmptyResult();
        }
    }
}