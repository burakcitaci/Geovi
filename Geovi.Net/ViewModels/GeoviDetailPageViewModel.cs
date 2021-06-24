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
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

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

      public ICommand OpenSettingsCommand { get; set; }

      public ICommand ChangeLayoutCommand { get; set; }
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

      private string layoutTitle = string.Empty;
      public string LayoutTitle
      {
         get
         {
            return this.layoutTitle;
         }
         set
         {
            this.layoutTitle = value;
            OnPropertyChanged(LayoutTitle);
         }
      }

      private bool isVisibleChanged;
      public bool IsVisibleChanged
      {
         get
         {
            return isVisibleChanged;
         }
         set
         {
            isVisibleChanged = value;
            this.IsVisibleChanged1 = !value;
            OnPropertyChanged(nameof(IsVisibleChanged));
         }
      }

      private bool isSettingsVisible = false;
      public bool IsSettingsVisible
      {
         get
         {
            return this.isSettingsVisible;
         }
         set
         {
            isSettingsVisible = value;
            OnPropertyChanged(nameof(IsSettingsVisible));
         }
      }
      private bool isVisibleChanged1 = true;
      public bool IsVisibleChanged1
      {
         get
         {
            return isVisibleChanged1;
         }
         set
         {
            isVisibleChanged1 = value;
            OnPropertyChanged(nameof(IsVisibleChanged1));
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
         ChangeLayoutCommand = new RelayCommand(this.ChangeLayoutCommandFunc);
         OpenSettingsCommand = new RelayCommand(this.OpenSettingsCommandFunc);
         this.ServiceFeatureTables = new List<ServiceFeatureTable>();
         
      }

      private void ChangeLayoutCommandFunc()
      {
         this.LayoutTitle = Guid.NewGuid().ToString().Substring(5);
         this.IsVisibleChanged = !this.IsVisibleChanged;
      }

      private void OpenSettingsCommandFunc()
      {
        
         this.IsSettingsVisible = !this.IsSettingsVisible;
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
         myFeatureLayer.Renderer = GetRendererForTable(myFeatureTable);
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
         Color color = this.ColorFromHSL();
         Random rnd = new Random();
         switch (table.GeometryType)
         {
            case GeometryType.Point:
            case GeometryType.Multipoint:
               return new SimpleRenderer(new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Blue, 4));

            case GeometryType.Polygon:
               
               SimpleLineSymbol lineSymbol1 = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, color, rnd.Next(0, 5));
               return new SimpleRenderer(new SimpleFillSymbol(SimpleFillSymbolStyle.Null, System.Drawing.Color.Transparent
                  , lineSymbol1));
            case GeometryType.Envelope:
               SimpleLineSymbol lineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid,Color.Red, rnd.Next(0,5));
               return new SimpleRenderer(new SimpleFillSymbol(SimpleFillSymbolStyle.Null, System.Drawing.Color.Transparent
                  , lineSymbol));

            case GeometryType.Polyline:
               return new SimpleRenderer(new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, color, rnd.Next(0, 5)));
         }

         return null;
      }

      private Color ColorFromHSL()
      {
         Random rnd = new Random();
         int r = 75;
         int g = rnd.Next(0, 256);
         int b = rnd.Next(10, 156);
         int a = 1;

         return Color.FromRgb( r, g, b);
      }
   }

   public class MapLoadedEventArgs
   {
      public MapLoadedEventArgs(string text) { Text = text; }
      public string Text { get; } // readonly
   }
}
