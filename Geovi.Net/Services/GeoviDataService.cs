using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Geovi.Net.DBContext;

namespace Geovi.Net.Services
{
   public class GeoviDataService : IGeoviDataService
   {
      ObservableCollection<GeoviDataBy> geoviDatasBy;
      public GeoviDataService()
      {
        
         using (var context = new CoreDbContext())
         {
            var datasFromDb = context.GeoviDatas.ToList();
            if(datasFromDb == null || datasFromDb.Count == 0)
            {
               geoviDatasBy = Utils.Utils.GeoviDatas;
            }
            else
            {
               geoviDatasBy = new ObservableCollection<GeoviDataBy>(datasFromDb);
               context.GeoviDatas.AddRange(geoviDatasBy);
               context.SaveChanges();
            }
         };
      }
      public ObservableCollection<GeoviData> GetAll()
      {
         return ((ObservableCollection<GeoviData>)(from geovi in geoviDatasBy
                 from data in geovi
                 select data));
      }

      public ObservableCollection<GeoviDataBy> GetBy(string parameter)
      {
         return new ObservableCollection<GeoviDataBy>(geoviDatasBy.Where(x => x.FilterBy == parameter));
      }

      public ObservableCollection<GeoviDataBy> GetAllBy()
      {
         return geoviDatasBy;
      }

      public ObservableCollection<string> GetGeoviDataTitles()
      {
         var t = geoviDatasBy.Select(x => x.FilterName);
         var observable = new ObservableCollection<string>(t);
         return observable;
      }
   }
}
