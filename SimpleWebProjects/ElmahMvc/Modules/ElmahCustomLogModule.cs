//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Elmah;
//using System.Collections;
//using System.Xml;
//using SharedProjectUploader.ElmahConfiguration;

//namespace ElmahMvc.Modules
//{
//    public class ElmahCustomLogModule : ErrorLogModule
//    {

//    }

//    public class CustomErrorLog : ErrorLog
//    {
//        public override string Log(Elmah.Error error)
//        {
//            error.User = "Jayavardhan";          
//            return base.Log(error);
//        }

//        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
//        {
//            //base.GetErrors(pageIndex, pageSize, errorEntryList);
//            var c = new ErrorLogContext();

//            foreach (var entry in c.ErrorLog1.OrderByDescending(m => m.Sequence).Skip(pageIndex * pageSize).Take(pageSize).ToList())
//            {
//                errorEntryList.Add(new ErrorLogEntry(this,
//                    entry.ErrorId.ToString(),
//                    new Error()
//                    {
//                        ApplicationName = this.ApplicationName,
//                        Message = entry.Message,
//                        StatusCode = entry.StatusCode,
//                        Time = entry.TimeUtc,
//                        Type = entry.Type,
//                        User = entry.User,
//                        HostName = entry.Host,
//                        Detail = entry.AllXml
//                    }));
//            }
//            return c.ErrorLog1.Count();
//        }

//        public override ErrorLogEntry GetError(string id)
//        {
//            return base.GetError(id);
//        }
//    }
//}