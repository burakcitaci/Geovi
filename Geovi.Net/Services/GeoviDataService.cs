using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace Geovi.Net.Services
{
   public class GeoviDataService : IGeoviDataService
   {
      ObservableCollection<GeoviDataBy> geoviDatasBy;
      public GeoviDataService()
      {
         geoviDatasBy = new ObservableCollection<GeoviDataBy>
         {
            new GeoviDataBy("Layer Name")
            {
               
               new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://services2.arcgis.com/jUpNdisbWqRpMo35/arcgis/rest/services/Verwaltungsgrenzen_RLP/FeatureServer/0"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Map",
                  Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam voluptua. ",
                  ParentName="Layer Name"
               },
                new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://services2.arcgis.com/jUpNdisbWqRpMo35/ArcGIS/rest/services/Verwaltungsgrenzen_RLP/FeatureServer/1"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Android",
                  Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam voluptua. ",
                  ParentName="Layer Name"
               },
                 new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://services2.arcgis.com/jUpNdisbWqRpMo35/ArcGIS/rest/services/Verwaltungsgrenzen_RLP/FeatureServer/2"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Android",
                  Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam voluptua. ",
                  ParentName="Layer Name"
               }
            },
            new GeoviDataBy("Layer Title")
            {

               new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://services2.arcgis.com/jUpNdisbWqRpMo35/ArcGIS/rest/services/Bev%c3%b6lkerung_nach_Alter/FeatureServer/0"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Map",
                  ParentName="Layer Title"
               },
                new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://services2.arcgis.com/jUpNdisbWqRpMo35/ArcGIS/rest/services/Bev%c3%b6lkerung_nach_Alter/FeatureServer/1"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Android",
                  ParentName="Layer Title"
               }
            },

            new GeoviDataBy("Lorem Ipsum")
            {

               new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://services2.arcgis.com/jUpNdisbWqRpMo35/ArcGIS/rest/services/EuroGlobalMap_Administrative_Boundaries/FeatureServer/0"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Map",
                  ParentName="Lorem Ipsum"
               },
                new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://services2.arcgis.com/jUpNdisbWqRpMo35/ArcGIS/rest/services/EuroGlobalMap_Administrative_Boundaries/FeatureServer/1"),
                  ServiceType = Enums.ServiceType.FeatueService,
                  LayerName = "Android",
                  ParentName="Lorem Ipsum"
               }
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
