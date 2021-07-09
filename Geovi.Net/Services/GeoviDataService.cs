using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Geovi.Net.DBContext;
using System.Diagnostics;

namespace Geovi.Net.Services
{
   public class GeoviDataService : IGeoviDataService
   {
      ObservableCollection<GeoviProject> geoviProjects;
      public GeoviDataService()
      {
         try
         {
            using (var context = new CoreDbContext())
            {
               //context.Database.EnsureCreated();
               var con = context.GeoviProjects.Include(x=>x.GeoviServices).ToList();
               //foreach(var c in con)
               //{
               //   //Debug.WriteLine(c.FilterName + " " + c.Liste.Count());
               //}
              
               //context.SaveChanges();
               //var datasFromDb = context.GeoviDatas.ToList();
               //var dd  = context.Geovis.ToList();
               //if (datasFromDb == null || datasFromDb.Count == 0)
               //{

               //   geoviDatasBy = Utils.Utils.GeoviDatas;
               //   context.GeoviDatas.AddRange(geoviDatasBy);
               //   context.SaveChanges();
               //   var tt = context.GeoviDatas.ToList();
               //}
               //else
               {
                  geoviProjects = new ObservableCollection<GeoviProject>(context.GeoviProjects.ToList());
                  //context.GeoviDatas.AddRange(new GeoviDataBy("Oki", Utils.Utils.Datas));
                  //context.SaveChanges();

                  
               }
            };
         }
         catch(Exception ex)
         {

         }
       
      }
      public ObservableCollection<GeoviService> GetAll()
      {
         return ((ObservableCollection<GeoviService>)(from geovi in geoviProjects
                 from data in geovi
                 select data));
      }

      public ObservableCollection<GeoviProject> GetBy(string parameter)
      {
         return new ObservableCollection<GeoviProject>(geoviProjects.Where(x => x.Name == parameter));
      }

      public ObservableCollection<GeoviProject> GetAllBy()
      {
         return geoviProjects;
      }

      public ObservableCollection<string> GetGeoviDataTitles()
      {
         var t = geoviProjects.Select(x => x.Name);
         var observable = new ObservableCollection<string>(t);
         return observable;
      }
   }
}
