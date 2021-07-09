using Geovi.Net.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
   public class GeoviDBContext : DbContext
   {
      public DbSet<GeoviDataBy> GeoviDatas { get; set; }
      public DbSet<GeoviData> Geovis { get; set; }
      public DbSet<Settings> Settings { get; set; }

      public GeoviDBContext()
      {
         try
         {
            //this.Database.EnsureCreated();
         }
         catch (Exception ex)
         {

         }
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         //string dbPath = Path.Combine(FileSystem.CacheDirectory, "blog.db");
         //optionsBuilder.UseSqlite($"Filename={dbPath}").EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information);
         optionsBuilder.UseSqlServer("Server=localhost;Database=GEO;Trusted_Connection=True;", b=>b.MigrationsAssembly("Geovi"));
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {

      }

   }
}
