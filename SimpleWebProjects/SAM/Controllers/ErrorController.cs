using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAM.Controllers
{
    public class ErrorController : Controller
    {
        [Route("NotFound")]
        public ActionResult NotFound()
        {
            return View();
        }

        [Route("Error")]
        public ActionResult InternalServerError()
        {
            return View();
        }

        [Route("ServerDown")]
        public ActionResult SqlError()
        {
            try
            {
                //Open connection and log
            }
            catch
            {
                //Raise a email or other exception
            }
            return View();
        }
    }
}