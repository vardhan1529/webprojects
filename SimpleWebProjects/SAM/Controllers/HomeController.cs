
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
using SAM.Models;
using System.Data.SqlClient;

namespace SAM.Controllers
{
    //[Authorize]
    [CustomActionFilter]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            //For testing use this data
            var items = Menu.GroupMenu(new List<Menu>() { new Menu() { Id = 1, Name = "P1" },
               new Menu() { Id = 2, PId = 1, Name = "C1P1"  }
                , new Menu() { Id = 3, PId = 1, Name = "C2P1"  },
               new Menu() { Id = 4,  Name = "P2"  },
               new Menu() { Id = 5, PId = 4, Name = "C1P2"  },
               new Menu() { Id = 6, PId = 5, Name = "C1C1P2"  }
        });

            return View(items);
        }

        //The HttpContext item is becoming null when configureAwait is used.
        public async Task<string> ContextItemCheck()
        {
            var x = HttpContext.Request;
            System.Threading.Thread.Sleep(1000);
            Task<string> t = new Task<string>(() =>
            {
                var y = HttpContext.Request;
                return "false";
            });
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

        public ActionResult ErrorTestAction()
        {
            throw new NotImplementedException();
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    var m = filterContext.Exception.Message;
        //    if (filterContext.Exception.GetType() == typeof(SqlException))
        //    {
        //        filterContext.Result = new RedirectResult("~/SqlError");
        //    }
        //    else
        //    {
        //        filterContext.Result = RedirectToAction("Index", "Home");
        //    }
        //}
    }
}