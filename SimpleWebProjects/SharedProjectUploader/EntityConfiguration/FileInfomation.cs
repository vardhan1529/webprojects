using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SharedProjectUploader.EntityConfiguration
{
    public class FileInformation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Category { get; set; }

        public bool IsAbsolete { get; set; }

        public int UploadedUserId { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        public virtual Category CategoryInfo { get; set; }

        public virtual FileData FileData { get; set; }
    }

    public class FileInformationEntityConfiguration : EntityTypeConfiguration<FileInformation>
    {
        public FileInformationEntityConfiguration()
        {
            this.ToTable("FileInformation").HasKey(m => m.Id);
            this.HasRequired(m => m.UserInfo).WithMany(m => m.UploadedFiles).HasForeignKey(m => m.UploadedUserId);
            this.HasRequired(m => m.CategoryInfo).WithMany(m => m.UploadedFiles).HasForeignKey(m => m.Category);
        }
    }
}