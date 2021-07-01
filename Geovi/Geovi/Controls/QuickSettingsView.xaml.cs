using Esri.ArcGISRuntime.Mapping;
using Geovi.Net.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Geovi.Controls
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class QuickSettingsView : ContentView
   {
      private GeoviDetailPageViewModel viewModel;
      public GeoviDetailPageViewModel ViewModel
      {
         get
         {
            if(viewModel == null)
            {
               viewModel = this.BindingContext as GeoviDetailPageViewModel;
            }
            return viewModel;
         }
      }
      private int layerCount = 4;
      public string OpenStreetMapPath { get; set; }
      public QuickSettingsView()
      {
         OpenStreetMapPath = "DarkGrayCanvas.png";
         InitializeComponent();
         
         
      }

      protected override void OnBindingContextChanged()
      {
         base.OnBindingContextChanged();   
      }

      private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
      {
         if (layerCount == 0)
         {
            FeatureLayer featureLayer = this.ViewModel.EsriMap.OperationalLayers[0] as FeatureLayer;
            //this.ServiceFeatureTables.Remove(featureLayer.FeatureTable as ServiceFeatureTable);
            this.ViewModel.EsriMap.OperationalLayers.Remove(featureLayer);
         }
         else
         {
            layerCount--;
         }
      }
   }
}