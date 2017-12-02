using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAM.Controllers
{
    public class ImageHandleController : Controller
    {
        // GET: ImageHandle
        [Route("{name}.png")]
        public ActionResult GetImage()
        {
            return new EmptyResult();
        }
    }
}