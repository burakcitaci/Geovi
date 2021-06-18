using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Geovi.Net.IViewModels;
using Geovi.Net.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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

      public GeoviDetailPage()
      {
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
         List<Feature> features = new List<Feature>();
         StringBuilder stringBuilder = new StringBuilder();
         foreach(var table in this.ViewModel.ServiceFeatureTables)
         {
            (table.Layer as FeatureLayer).ClearSelection();
            stringBuilder.Append(table.TableName + Environment.NewLine);
            string[] outputFields = { "*" };
            FeatureQueryResult fqr = await table.QueryFeaturesAsync(queryParameters, QueryFeatureFields.LoadAll);
            Feature feature = fqr.FirstOrDefault();
            if(feature != null)
            {
               features.Add(feature);
               (table.Layer as FeatureLayer).SelectFeature(feature);
               StringBuilder sb = new StringBuilder();
               foreach(var att in feature.Attributes)
               {
                  if(att.Value.ToString().Length > 5)
                  {
                     sb.Append(att.Key + " : " + att.Value.ToString().Substring(0, 5) + Environment.NewLine);
                  }        
               }

               this.ViewModel.Fields.Add(sb.ToString());
            }
         }
         this.MyMapView.GraphicsOverlays[0].Graphics.Add(new Esri.ArcGISRuntime.UI.Graphic(e.Location));
         CalloutDefinition callout = new CalloutDefinition("hello", stringBuilder.ToString());
         Point point = new Point(e.Location.X, e.Location.Y);
         this.MyMapView.ShowCalloutAt(e.Location, callout);

      }
   }
}