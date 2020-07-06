using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using WpfOpenCVTest.Dal.Models;

namespace WpfOpenCVTest.Dal
{
public class DatabaseContext :DbContext

    {
        
        public DbSet<JsonConfig> Jsons { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DatabaseContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<JsonConfig>(eb =>
                {
                    if (!Database.IsSqlite())
                    {
                        eb.HasNoKey();
                        eb.Ignore(e => e.Name);
                        eb.ToView("vTestView");
                    }

                    {
                        eb.HasKey(e => e.Id);
                    }
                });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite(@"Data Source=.\SQLITEDB\Test.db");
    }
}
