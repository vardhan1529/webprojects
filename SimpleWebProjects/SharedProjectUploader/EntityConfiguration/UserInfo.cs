using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace SharedProjectUploader.EntityConfiguration
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int EmployeeId { get; set; }

        public virtual ICollection<FileInformation> UploadedFiles { get; set; }
    }

    public class UserInfoEntityConfiguration : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoEntityConfiguration()
        {
            this.ToTable("UserInfo").HasKey(m => m.Id);
            this.Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}