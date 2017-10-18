using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAM.Filters
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        private Guid id;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            id = Guid.NewGuid();
            var request = filterContext.HttpContext.Request;
            if (!filterContext.IsChildAction && HttpContext.Current.Items["Id"] == null)
            {
                HttpContext.Current.Items.Add("Id", id);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var logModel = new LogModel(id);
            if (filterContext.IsChildAction)
            {
                logModel.ParentId = new Guid(HttpContext.Current.Items["Id"].ToString());
            }

            LogInfo.AddLog(logModel);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }
    }

    //For temporary testing purpose
    public class LogInfo
    {
        static LogInfo()
        {
            Logs = new List<LogModel>();
        }
        private static List<LogModel> Logs { get; set; }
        public static void AddLog(LogModel data)
        {
            Logs.Add(data);
        }
        public static List<LogModel> GetLogs()
        {
            return Logs;
        }
    }

    public class LogModel
    {
        public LogModel() { }
        public LogModel(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
    }

}