using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using Elmah;

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
            return base.Log(error);
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
    }
}