using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAM.Handlers
{
    public class CustomErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var m = filterContext.Exception.Message;
            if (filterContext.Exception.GetType() == typeof(SqlException))
            {
                filterContext.Result = new RedirectResult("~/SqlError");
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Error");
            }

            filterContext.ExceptionHandled = true;
        }
    }
}