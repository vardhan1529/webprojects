using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElmahMvc.Game
{
    [DbConfigurationType(typeof(MyConfiguration))]
    public class GameContext : DbContext
    {
        public GameContext():base("SpaceContext")
        {

        }
        public DbSet<GameInfo> Game { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameInfo>().HasKey(m => m.Id);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }

    public class MyConfiguration : DbConfiguration
    {
        public MyConfiguration()
        {
            AddInterceptor(new StringTrimmerInterceptor());
        }
    }
}