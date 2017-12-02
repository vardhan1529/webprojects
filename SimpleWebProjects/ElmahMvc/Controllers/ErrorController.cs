using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ElmahMvc.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public ActionResult General()
        {
            return View("~/Views/Error/Error.cshtml");
        }

        [Route("NotFound")]
        public ActionResult NotFound()
        {
            return View();
        }

        [Route("SqlError")]
        public ActionResult SqlError()
        {
            return View("~/Views/Error/Error.cshtml");
        }

        [Route("AjaxError")]
        public ActionResult AjaxError()
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new JsonResult() { Data = new { Error = true, Message = "An error has occured while processing request" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}