using Geovi.Net.IViewModels;
using Geovi.Net.Model;
using Geovi.Net.Services;
using Geovi.Net.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using Esri.ArcGISRuntime.Data;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Geovi.Net.ViewModels
{
   public class SettingsDetailPageViewModel : BasePageViewModel, ISettingsDetailPageViewModel
   {
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

      private GeoviDataBy geoviDatas; 
      private GeoviDataBy GeoviDatas
      {
         get
         {
            return geoviDatas;
         }
         set
         {
            geoviDatas = value;
            OnPropertyChanged(nameof(GeoviDatas));
         }
      }

      private List<string> fields;
      public List<string> Fields
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

      private List<string> colorNames;
      public List<string> ColorNames
      {
         get
         {
            if(colorNames == null)
            {
               colorNames = new List<string>();
               foreach (string colorName in Utils.Utils.NameToColor.Keys)
               {
                  colorNames.Add(colorName);
               }
            }
            return colorNames;
         }
      }

      private string selectedColorName;
      public string SelectedColorName
      {
         get
         {
            return selectedColorName;
         }
         set
         {
            selectedColorName = value;
            PickedColor = Utils.Utils.NameToColor[value];
            OnPropertyChanged(nameof(SelectedColorName));
         }
      }

      private Color pickedColor = Color.Transparent;
      public Color PickedColor
      {
         get
         {
            return pickedColor;
         }
         set
         {

            pickedColor = value;
            OnPropertyChanged(nameof(PickedColor));
         }
      }
      private GeoviData GeoviData
      {
         get
         {
            return this.GeoviDatas.Where(x => x.ParentName == this.Title).FirstOrDefault();
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

      private Color editedColor;

      public Color EditedColor
      {
         get => editedColor;
         set { editedColor = value; OnPropertyChanged(nameof(EditedColor)); }
      }

      public ICommand GoBackCommand 
      {
         get;set;
      }

      private INavigationService NavigationService;

      public SettingsDetailPageViewModel(INavigationService navigationService)
      {
         this.NavigationService = navigationService;

         GoBackCommand = new RelayCommand(this.GoBackCommandFunc);
      }


      private void GoBackCommandFunc()
      {
         this.NavigationService.Pop();
      }

      public override void OnPagePushing(params object[] parameters)
      {
         if (parameters != null && parameters.Length != 0)
         {
            GeoviDataBy geoviDatas = parameters[0] as GeoviDataBy;
            this.GeoviDatas = geoviDatas;
            this.Title = geoviDatas.FilterName;
            GetFields();
         }
      }

      private async  void GetFields()
      {

         Uri serviceUri = new Uri(GeoviData.ServiceUrl.AbsoluteUri);
         // Initialize a new feature layer
         ServiceFeatureTable myFeatureTable = new ServiceFeatureTable(serviceUri);
         await myFeatureTable.LoadAsync();

         FeatureTable featureTable = ((FeatureTable)myFeatureTable);
         this.Fields = featureTable.Fields.Select(x => x.Name).ToList();


      }
   }
}
