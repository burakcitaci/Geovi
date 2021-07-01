using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Mapping;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static Geovi.Net.Utils.Delegates;
using Xamarin.Forms;
using Geovi.Net.Model;

namespace Geovi.Net.Utils
{
   public class Utils
   {
      public static ObservableCollection<BasemapWrapper> Basemaps
      {
         get
         {
            var dict = new ObservableCollection<BasemapWrapper>();
            Basemap basemap = Basemap.CreateLightGrayCanvasVector();
            BasemapWrapper basemapWrapper = new BasemapWrapper();
            basemapWrapper.IconPath = "LightCanvas.png";
            basemapWrapper.DisplayName = "Light";
            basemapWrapper.Basemap = basemap;
            dict.Add(basemapWrapper);
            basemapWrapper = new BasemapWrapper();
            basemapWrapper.DisplayName = "Imagery";
            basemapWrapper.IconPath = "Imagery.png";
            basemap = Basemap.CreateImageryWithLabelsVector();
            basemapWrapper.Basemap = basemap;
            dict.Add(basemapWrapper);
            
            basemapWrapper = new BasemapWrapper();
            basemapWrapper.DisplayName = "Dark";
            basemapWrapper.IconPath = "DarkGrayCanvas.png";
            basemap = Basemap.CreateDarkGrayCanvasVector();
            basemapWrapper.Basemap = basemap;
            dict.Add(basemapWrapper);
            
            basemapWrapper = new BasemapWrapper();
            basemap = Basemap.CreateOpenStreetMap();
            basemapWrapper.DisplayName = "Streets";
            basemapWrapper.IconPath = "OpenStreetMap.png";
            basemapWrapper.Basemap = basemap;
            dict.Add(basemapWrapper);
            return dict;
         }
      }

      public static Dictionary<string, Color> NameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Blue", Color.Blue }, { "Fuchsia", Color.Fuchsia },
            { "Gray", Color.Gray }, { "Green", Color.Green },
            { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
            { "Navy", Color.Navy }, { "Olive", Color.Olive },
            { "Purple", Color.Purple }, { "Red", Color.Red },
            { "Silver", Color.Silver }, { "Teal", Color.Teal },
            { "White", Color.White }, { "Yellow", Color.Yellow }
        };

      public static ObservableCollection<GeoviDataBy> GeoviDatas = new ObservableCollection<GeoviDataBy>
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
               },
                   new GeoviData()
               {
                  Title = "Hello World",
                  ServiceUrl = new Uri("https://services2.arcgis.com/jUpNdisbWqRpMo35/ArcGIS/rest/services/Verwaltungsgrenzen_RLP/FeatureServer/3"),
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

   public class BasemapWrapper
   {
      public string DisplayName { get; set; }

      public string IconPath { get; set; }

      public Basemap Basemap { get; set; }
   }

   public class FeatureTableWrapper
   {
      public string TableName { get; set; }

      public Dictionary<string, string> KeyValues { get; set; }

      public string Key { get; set; }
      public string Value
      {
         get
         {
            
            return String.Join(Environment.NewLine, KeyValues.Select(x => String.Format("{0}={1}", x.Key, x.Value)));
         }
      }
     

      public override string ToString()
      {
         return base.ToString();
      }
   }

   public class QuickSettingsWrapper : ObservableCollection<ServiceFeatureTable>
   {
      public ServiceFeatureTable ServiceFeatureTable { get; set; }
      
      public event LayerCheckedEventHandler LayerChecked;
      LayerCheckedEventArgs args = new LayerCheckedEventArgs(false);
      private bool layerChecked = true;
      public bool IsLayerChecked
      {
         get
         {
            return this.layerChecked;
         }
         set
         {
            if(LayerChecked != null && layerChecked != value)
            {
               args.Checked = value;
               LayerChecked(this.ServiceFeatureTable.Layer, args);
            }
            layerChecked = value;
            //this.ServiceFeatureTable.Layer.Name
            //OnPropertyChanged(nameof(IsLayerChecked));
         }
      }

      public string LayerName
      {
         get
         {
            return this.ServiceFeatureTable.Layer.Name;
         }
      }
   }
}
