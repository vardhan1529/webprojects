using Elmah;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ElmahMvc.Handlers
{
    public class CustomElmahFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectResult("~/AjaxError");
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Error");

                    //if (filterContext.Exception.GetType() == typeof(SqlException))
                    //{
                    //    filterContext.Result = new RedirectResult("~/SqlError");
                    //}
                    //else
                    //{
                    //    filterContext.Result = new RedirectResult("~/Error");
                    //}
                }
            }
        }
    }
}