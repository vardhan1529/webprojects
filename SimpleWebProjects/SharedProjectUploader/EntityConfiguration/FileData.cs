using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SharedProjectUploader.EntityConfiguration
{
    public class FileData
    {
        public int Id { get; set; }

        public byte[] Data { get; set; }

        public virtual FileInformation FileInformation { get; set; }
    }

    public class FileDataEntityConfiguration : EntityTypeConfiguration<FileData>
    {
        public FileDataEntityConfiguration()
        {
            HasKey(m => m.Id).ToTable("FileData");;
            Property(m => m.Data).IsRequired();
            HasRequired(m => m.FileInformation).WithRequiredDependent(m => m.FileData);
        }
    }
}