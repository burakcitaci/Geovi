using Geovi.Net.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;

namespace Geovi.Net.DBContext
{
   public class CoreDbContext : DbContext
   { 
      public DbSet<GeoviProject> GeoviProjects { get; set; }
      //public DbSet<GeoviData> Geovis { get; set; }
      //public DbSet<Settings> Settings { get; set; }

      public CoreDbContext()
      {
         try
         {
            this.Database.EnsureCreated();
         }
         catch(Exception ex)
         {

         }
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         string dbPath = Path.Combine(FileSystem.AppDataDirectory, "blog.db");
         //optionsBuilder.UseSqlite($"Filename={dbPath}").EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information);
         optionsBuilder.UseSqlite($"Data Source={dbPath};");
         base.OnConfiguring(optionsBuilder);
      }
     

      //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      //{
      //   string dbPath = Path.Combine(FileSystem.AppDataDirectory, "blog.db");
      //   optionsBuilder.UseSqlite($"Filename={dbPath}").EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information);
      //  // optionsBuilder.UseSqlite("Server=localhost;Database=GEO;Trusted_Connection=True;");
      //}

      //protected override void OnModelCreating(ModelBuilder modelBuilder)
      //{
        
      //}

   }
}
