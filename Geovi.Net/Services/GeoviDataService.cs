using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Geovi.Net.Services
{
   public class GeoviDataService : IGeoviDataService
   {
      IEnumerable<GeoviDataBy> geoviDatasBy;
      public GeoviDataService()
      {
         geoviDatasBy = new List<GeoviDataBy>
         {
            new GeoviDataBy("LayerName")
            {
               
               new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://www.ssport.tv/yayin-akisi"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Map",
                  Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam voluptua. "
               },
                new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://www.ssport.tv/yayin-akisi"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Android",
                  Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam voluptua. "
               }
            },
            new GeoviDataBy("LayerTitle")
            {

               new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://www.ssport.tv/yayin-akisi"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Map"
               },
                new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://www.ssport.tv/yayin-akisi"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Android"
               }
            }
         };
      }
      public IEnumerable<GeoviData> GetAll()
      {
         return (from geovi in geoviDatasBy
                 from data in geovi
                 select data).ToList();
      }

      public IEnumerable<GeoviDataBy> GetBy(string parameter)
      {
         return geoviDatasBy.Where(x => x.FilterBy == "LayerName");
      }

      public IEnumerable<GeoviDataBy> GetAllBy()
      {
         return geoviDatasBy;
      }

      public IEnumerable<string> GetGeoviDataTitles()
      {
         return geoviDatasBy.Select(x => x.FilterName).ToList();
      }
   }
}
