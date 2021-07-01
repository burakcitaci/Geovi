using Geovi.Net.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace Geovi.Net.DBContext
{
   public class CoreDbContext : DbContext
   { 
      public DbSet<GeoviDataBy> GeoviDatas { get; set; }

      public CoreDbContext()
      {
         this.Database.EnsureCreated();
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         try
         {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "blog.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
         }
         catch(Exception ex)
         {

         }
      }
   }
}
