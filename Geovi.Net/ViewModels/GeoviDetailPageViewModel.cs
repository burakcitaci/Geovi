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
using static Geovi.Net.Utils.Delegates;

namespace Geovi.Net.ViewModels
{
   public class GeoviDetailPageViewModel : BasePageViewModel, IGeoviDetailPageViewModel
   {
      
      public event MapLoadedEventHandler MapLoaded;

      private ObservableCollection<FeatureTableWrapper> fields = new ObservableCollection<FeatureTableWrapper>();
      public ObservableCollection<FeatureTableWrapper> Fields
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

      public ICommand ChangeBasemapCommand { get; set; }
      public ICommand LayerCheckCommand { get; set; }
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

      public ObservableCollection<BasemapWrapper> Basemaps
      {
         get
         {
            return Utils.Utils.Basemaps;
         }
         set
         {

         }
      }

      private bool layerChecked = true;
      public bool LayerChecked
      {
         get
         {
            return this.layerChecked;
         }
         set
         {
            layerChecked = value;
            OnPropertyChanged(nameof(LayerChecked));
         }
      }
      private ObservableCollection<ServiceFeatureTable> serviceFeatureTables;
      public ObservableCollection<ServiceFeatureTable> ServiceFeatureTables
      {
         get => serviceFeatureTables;
         set
         {
            serviceFeatureTables = value;
            OnPropertyChanged(nameof(ServiceFeatureTable));
         }
      }

      private ObservableCollection<QuickSettingsWrapper> quickSettings;
      public ObservableCollection<QuickSettingsWrapper> QuickSettings
      {
         get => quickSettings;
         set
         {
            quickSettings = value;
            OnPropertyChanged(nameof(QuickSettingsWrapper));
         }
      }

      //public List<ServiceFeatureTable> ServiceFeatureTables { get; set; }
      public GeoviDetailPageViewModel(INavigationService navigationService)
      {
         
         this.NavigationService = navigationService;

         GoBackCommand = new RelayCommand(this.GoBackCommandFunc);
         ChangeLayoutCommand = new RelayCommand(this.ChangeLayoutCommandFunc);
         OpenSettingsCommand = new RelayCommand(this.OpenSettingsCommandFunc);
         ChangeBasemapCommand = new RelayCommand(this.ChangeBasemapCommandFunc);
         LayerCheckCommand = new RelayCommand(this.LayerCheckCommandFunc);
         this.ServiceFeatureTables = new ObservableCollection<ServiceFeatureTable>();
         this.QuickSettings = new ObservableCollection<QuickSettingsWrapper>();
         
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

      private async void ChangeBasemapCommandFunc(object parameter)
      {
         if(parameter != null)
         {
            Basemap basemap = (parameter as BasemapWrapper).Basemap;
            this.EsriMap.Basemap = basemap;
            await this.EsriMap.LoadAsync();
         }
      }

      private async void LayerCheckCommandFunc(object parameter)
      {
         FeatureLayer featureLayer = this.EsriMap.OperationalLayers[0] as FeatureLayer;
         //this.ServiceFeatureTables.Remove(featureLayer.FeatureTable as ServiceFeatureTable);
         this.EsriMap.OperationalLayers.Remove(featureLayer);
      }

      public override void OnPagePushing(params object[] parameters)
      {
         if (parameters != null && parameters.Length != 0)
         {
            this.GeoviDatas = parameters[0] as GeoviDataBy;
            this.Title = this.GeoviDatas.FilterName;
            this.EsriMap = new Map(this.Basemaps.Last().Basemap);
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
         QuickSettingsWrapper wrapper = new QuickSettingsWrapper();
         wrapper.LayerChecked += Wrapper_LayerChecked;
         wrapper.ServiceFeatureTable = myFeatureTable;
         wrapper.IsLayerChecked = true;
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
         this.QuickSettings.Add(wrapper);
        
         if (MapLoaded != null && this.ServiceFeatureTables.Count == 1)
         {
            MapLoaded(this.ServiceFeatureTables.First().Extent, null);
         }
      }

      private void Wrapper_LayerChecked(object e, LayerCheckedEventArgs args)
      {
         try
         {
            if (!args.Checked)
            {
               int index = this.EsriMap.OperationalLayers.IndexOf(e as FeatureLayer);
               
               this.EsriMap.OperationalLayers.RemoveAt(index);
            }
            else
            {
               var layer = ((FeatureLayer)e);
               this.EsriMap.OperationalLayers.Add(layer);
            }
         }
         catch(Exception ex)
         {

         }
         //this.ServiceFeatureTables.Remove(table);
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

  

 
}
