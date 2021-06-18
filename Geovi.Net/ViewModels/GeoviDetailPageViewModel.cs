using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Geovi.Net.IViewModels;
using Geovi.Net.Model;
using Geovi.Net.Services;
using Geovi.Net.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Geovi.Net.ViewModels
{
   public class GeoviDetailPageViewModel : BasePageViewModel, IGeoviDetailPageViewModel
   {
      public delegate void MapLoadedEventHandler(object e, MapLoadedEventArgs args);

      public event MapLoadedEventHandler MapLoaded;

      private ObservableCollection<string> fields = new ObservableCollection<string>();
      public ObservableCollection<string> Fields
      {
         get
         {
            return fields;
         }
         set
         {
            fields = value;
            OnPropertyChanged(nameof(Fields));
         }
      }
      public ICommand GoBackCommand { get; set; }
      public INavigationService NavigationService { get; set; }
      private string title = string.Empty;
      public string Title
      {
         get
         {
            return this.title;
         }
         set
         {
            title = value;
            OnPropertyChanged(nameof(Title));
         }
      }
      public GeoviData GeoviData { get; set; }
      public GeoviDataBy GeoviDatas { get; set; }

      private Map esriMap;
      public Map EsriMap
      {
         get
         {
            return esriMap;
         }
         set
         {
            esriMap = value;
            OnPropertyChanged(nameof(EsriMap));
         }
      }

      public List<ServiceFeatureTable> ServiceFeatureTables { get; set; }
      public GeoviDetailPageViewModel(INavigationService navigationService)
      {
         this.NavigationService = navigationService;

         GoBackCommand = new RelayCommand(this.GoBackCommandFunc);
         this.ServiceFeatureTables = new List<ServiceFeatureTable>();
         
      }

      private void GoBackCommandFunc()
      {
         this.NavigationService.Pop();
      }

      public override void OnPagePushing(params object[] parameters)
      {
         if (parameters != null && parameters.Length != 0)
         {
            this.GeoviDatas = parameters[0] as GeoviDataBy;
            this.Title = this.GeoviDatas.FilterName;
            this.EsriMap = new Map(Basemap.CreateOpenStreetMap());
            this.EsriMap.Loaded += EsriMap_Loaded;
            //this.EsriMap.LoadAsync();
            //GeoviData geoviDatas = parameters[0] as GeoviData;
            //this.Title = geoviDatas.Title;
            //this.GeoviData = geoviDatas;
         }

      }

      private void EsriMap_Loaded(object sender, EventArgs e)
      {
        
         foreach(var data in this.GeoviDatas)
         {
            this.LoadServices(data);
         }
        
      }

      private async void LoadServices(GeoviData geoviData)
      {
         Uri serviceUri = new Uri(geoviData.ServiceUrl.AbsoluteUri);

         // Initialize a new feature layer
         ServiceFeatureTable myFeatureTable = new ServiceFeatureTable(serviceUri);

         await myFeatureTable.LoadAsync();
         FeatureLayer myFeatureLayer = new FeatureLayer(myFeatureTable);
         //Default oder custom renderer
         //myFeatureLayer.Renderer = this.GetRendererForTable(myFeatureTable);
         // Add the feature layer to the Map
         //myMap.OperationalLayers.Add(myFeatureLayer);
         // Add created layer to the map
         this.EsriMap.OperationalLayers.Add(myFeatureLayer);
         this.EsriMap.InitialViewpoint = new Viewpoint(myFeatureTable.Extent.GetCenter(), 3000);
         this.ServiceFeatureTables.Add(myFeatureTable);
        
         if (MapLoaded != null && this.ServiceFeatureTables.Count == 1)
         {
            MapLoaded(this.ServiceFeatureTables.First().Extent, null);
         }
      }

      private Renderer GetRendererForTable(FeatureTable table)
      {
         switch (table.GeometryType)
         {
            case GeometryType.Point:
            case GeometryType.Multipoint:
               return new SimpleRenderer(new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Blue, 4));

            case GeometryType.Polygon:
            case GeometryType.Envelope:
               SimpleLineSymbol lineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Red, 1.0);
               return new SimpleRenderer(new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.Chartreuse, lineSymbol));

            case GeometryType.Polyline:
               return new SimpleRenderer(new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.YellowGreen, 12));
         }

         return null;
      }
   }

   public class MapLoadedEventArgs
   {
      public MapLoadedEventArgs(string text) { Text = text; }
      public string Text { get; } // readonly
   }
}
