using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SharedProjectUploader.EntityConfiguration
{
    public class SharedProjectsContext : DbContext
    {
        public SharedProjectsContext():base("SharedProjectConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<FileInformation> FileInformation { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<FileData> FileData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserInfoEntityConfiguration());
            modelBuilder.Configurations.Add(new CategoryEnityConfiguration());
            modelBuilder.Configurations.Add(new FileInformationEntityConfiguration());
            modelBuilder.Configurations.Add(new FileDataEntityConfiguration());
        }
    }
}