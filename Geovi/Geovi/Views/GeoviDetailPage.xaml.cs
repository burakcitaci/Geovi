using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Geovi.Net.IViewModels;
using Geovi.Net.Utils;
using Geovi.Net.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Sharpnado.Shades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Geovi.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class GeoviDetailPage : ContentPage
   {
      public GeoviDetailPageViewModel ViewModel
      {
         get
         {
            return this.BindingContext as GeoviDetailPageViewModel;
         }
      }

      public string LayerLaunchIconPath { get; set; }

      //public string OpenStreetMapPath { get; set; }
      public GeoviDetailPage()
      {
         LayerLaunchIconPath = "outline_layers_black_36dp.png";
         //OpenStreetMapPath = "OpenStreetMap.png";
         InitializeComponent();
         BindingContext = ((App)App.Current).ServiceProvider.GetRequiredService<IGeoviDetailPageViewModel>();
         this.ViewModel.MapLoaded += ViewModel_MapLoaded;
         GraphicsOverlay graphicsOverlay = new GraphicsOverlay();
         SimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, Color.Red, 5);
         graphicsOverlay.Renderer = new SimpleRenderer(simpleMarkerSymbol);
         MyMapView.GraphicsOverlays.Add(graphicsOverlay);
      }

      private async void ViewModel_MapLoaded(object e, MapLoadedEventArgs args)
      {
         
         await this.MyMapView.SetViewpointGeometryAsync(((Envelope)e),10);
      }

      private void Map_Loaded(object sender, EventArgs e)
      {
         
      }

      private void MyMapView_NavigationCompleted(object sender, EventArgs e)
      {

      }

      private async void MyMapView_GeoViewTapped(object sender, Esri.ArcGISRuntime.Xamarin.Forms.GeoViewInputEventArgs e)
      {
         this.MyMapView.GraphicsOverlays[0].Graphics.Clear();
         
         QueryParameters queryParameters = new QueryParameters();
         queryParameters.Geometry = e.Location;
         string sum = string.Empty;
         List<CalloutDefinition> calloutDefinitions = new List<CalloutDefinition>();
         //List<Feature> features = new List<Feature>();
         StringBuilder stringBuilder = new StringBuilder();
         this.ViewModel.Fields.Clear();
         foreach (var table in this.ViewModel.ServiceFeatureTables)
         {
            FeatureTable arcGISFeatureTable = table as FeatureTable;
            FeatureLayer layer = table.Layer as FeatureLayer;
            layer.ClearSelection();
            if (this.ViewModel.EsriMap.OperationalLayers.Contains(layer))
            {
               
               string[] outputFields = { "*" };
               FeatureQueryResult fqr = await table.QueryFeaturesAsync(queryParameters, QueryFeatureFields.LoadAll);
               Feature feature = fqr.FirstOrDefault();
               if (feature != null)
               {
                  stringBuilder.Append(feature.Attributes.First().Value + Environment.NewLine);
                  //features.Add(feature);
                   layer.SelectFeature(feature);
                  StringBuilder sb = new StringBuilder();
                  FeatureTableWrapper wrapper = new FeatureTableWrapper();
                  wrapper.TableName = layer.FeatureTable.TableName;
                  wrapper.KeyValues = new Dictionary<string, string>();
                  foreach (var att in feature.Attributes)
                  {
                     if (!wrapper.KeyValues.ContainsKey(att.Key))
                     {
                        wrapper.KeyValues.Add(att.Key, att.Value.ToString());
                     }
                  }
                  this.ViewModel.Fields.Add(wrapper);
               }
            }
         }
         this.MyMapView.GraphicsOverlays[0].Graphics.Add(new Esri.ArcGISRuntime.UI.Graphic(e.Location));
         CalloutDefinition callout = new CalloutDefinition(this.ViewModel.GeoviProject.Name, stringBuilder.ToString());
         Point point = new Point(e.Location.X, e.Location.Y);
         this.MyMapView.ShowCalloutAt(e.Location, callout);

      }

      private void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
      {
         //this.entryStack.IsVisible = !this.labelStack.IsVisible;
      }

      private void ClickGestureRecognizer_Clicked_1(object sender, EventArgs e)
      {
         //this.settingsWindow.IsVisible = !this.settingsWindow.IsVisible;
      }
   }
}