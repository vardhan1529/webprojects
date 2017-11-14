using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using Elmah;
using System.Xml;

namespace SharedProjectUploader.ElmahConfiguration
{

    public class ErrorLog1
    {
        public Guid ErrorId { get; set; }
        public string Application { get; set; }

        public string Host { get; set; }

        public string Type { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public int StatusCode { get; set; }

        public DateTime TimeUtc { get; set; }

        public int Sequence { get; set; }

        public string AllXml { get; set; }
    }

    public class ErrorLogContext : DbContext
    {
        public ErrorLogContext() : base("ErrorLogConnection") { }

        public DbSet<ErrorLog1> ErrorLog1 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorLog1>().ToTable("ELMAH_Error").HasKey(m => m.ErrorId).Property(m => m.Sequence).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

        }
    }

    public class CustomElmahErrorLog : Elmah.SqlErrorLog
    {
        public CustomElmahErrorLog(string connection) : base(connection)
        {
        }

        public CustomElmahErrorLog(IDictionary config) : base(config)
        {
        }

        public override string Log(Elmah.Error error)
        {
            var r = new XmlDocument();
            var e = r.CreateElement("error");
            e.SetAttribute("application", error.ApplicationName);
            e.SetAttribute("host", error.HostName);
            e.SetAttribute("type", error.Type);
            e.SetAttribute("message", error.Message);
            e.SetAttribute("source", error.Source);
            e.SetAttribute("detail", error.Detail);
            e.SetAttribute("time", error.Time.ToString("yyyy-MM-ddThh:mm:ss.fffffff"));
            r.AppendChild(e);
            r.SelectSingleNode("descendant::error").AppendChild(r.CreateElement("serverVariables"));
            var re = r.SelectSingleNode("descendant::serverVariables");
            foreach (var sv in error.ServerVariables.AllKeys)
            {
                XmlElement rei = r.CreateElement("item");
                rei.SetAttribute("name", sv);
                XmlElement rev = r.CreateElement("value");
                rev.SetAttribute("string", error.ServerVariables.Get(sv));
                rei.AppendChild(rev);
                re.AppendChild(rei);
            }
            r.SelectSingleNode("descendant::error").AppendChild(r.CreateElement("cookies"));
            var rc = r.SelectSingleNode("descendant::cookies");
            foreach (var sv in error.ServerVariables.AllKeys)
            {
                XmlElement rci = r.CreateElement("item");
                rci.SetAttribute("name", sv);
                XmlElement rcv = r.CreateElement("value");
                rcv.SetAttribute("string", error.ServerVariables.Get(sv));
                rci.AppendChild(rcv);
                rc.AppendChild(rci);
            }
            var c = new ErrorLogContext();
            c.ErrorLog1.Add(new ErrorLog1()
            {
                ErrorId = Guid.NewGuid(),
                Application = this.ApplicationName,
                Message = error.Message,
                StatusCode = error.StatusCode,
                TimeUtc = error.Time,
                Type = error.Type,
                User = error.User,
                Host = error.HostName,
                Source = error.Source,
                AllXml = r.OuterXml
            });

            c.SaveChanges();
            return "successful";
        }

        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
        {
            //base.GetErrors(pageIndex, pageSize, errorEntryList);
            var c = new ErrorLogContext();

            foreach (var entry in c.ErrorLog1.OrderByDescending(m => m.Sequence).Skip(pageIndex * pageSize).Take(pageSize).ToList())
            {
                errorEntryList.Add(new ErrorLogEntry(this,
                    entry.ErrorId.ToString(),
                    new Error()
                    {
                        ApplicationName = this.ApplicationName,
                        Message = entry.Message,
                        StatusCode = entry.StatusCode,
                        Time = entry.TimeUtc,
                        Type = entry.Type,
                        User = entry.User,
                        HostName = entry.Host,
                        Detail = entry.AllXml
                    }));
            }
            return c.ErrorLog1.Count();
        }

        public override ErrorLogEntry GetError(string id)
        {
            return base.GetError(id);
        }
    }
}