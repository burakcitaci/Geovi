using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbStart
{

   public class GeoviDbContext : DbContext
   {
      public DbSet<GeoviDataBy> GeoviDatas { get; set; }
      public DbSet<GeoviData> Geovis { get; set; }
      public DbSet<Settings> Settings { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         //string dbPath = Path.Combine(FileSystem.CacheDirectory, "blog.db");
         //optionsBuilder.UseSqlite($"Filename={dbPath}").EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information);
         optionsBuilder.UseSqlServer("Server=localhost;Database=GEOVI;Trusted_Connection=True;", 
            b=>b.MigrationsAssembly("Geovi"));
      }

   }

   public class GeoviData
   {
      [System.ComponentModel.DataAnnotations.Key]
      public int ID { get; set; }
      public string Title { get; set; }

      public Uri ServiceUrl { get; set; }

      //public ServiceType ServiceType { get; set; }

      public string LayerName { get; set; }

      public string Description { get; set; }

      public string ParentName { get; set; }

      public GeoviData() { }

   }

   public class GeoviDataBy : List<GeoviData>
   {
      [System.ComponentModel.DataAnnotations.Key]
      public int ID { get; set; }
      public string FilterBy { get; set; }
      public string FilterName { get; set; }

      public List<GeoviData> Liste { get; set; }
      public Settings Settings { get; set; }

      //public ObservableCollection<GeoviData> Liste { get; set; }
      public GeoviDataBy(string filterBy, List<GeoviData> geovis) : base(geovis)
      {
         this.FilterBy = filterBy;
         this.FilterName = filterBy;
      }

      public GeoviDataBy() { }
   }

   public class Settings
   {
      [System.ComponentModel.DataAnnotations.Key]
      public int ID { get; set; }
      public string SelectedColorName { get; set; }

      public string FieldName { get; set; }

      public string BasemapName { get; set; }

      public Settings() { }
   }
}
