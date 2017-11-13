using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SharedProjectUploader.EntityConfiguration
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public virtual ICollection<FileInformation> UploadedFiles { get; set; }
    }

    public class CategoryEnityConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryEnityConfiguration()
        {
            this.ToTable("Category").HasKey(m => m.Id);
            Property(m => m.ParentId).IsOptional();
        }
    }
}