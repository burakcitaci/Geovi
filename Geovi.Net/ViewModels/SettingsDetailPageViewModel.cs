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
using Geovi.Net.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Geovi.Net.ViewModels
{
   public class SettingsDetailPageViewModel : BasePageViewModel, ISettingsDetailPageViewModel
   {
      private Settings settings;
      public Settings Settings
      {
         get
         {
            return this.settings;
         }
         set
         {
            this.settings = value;
            OnPropertyChanged(nameof(Settings));
         }
      }
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

      private GeoviProject geoviProject; 
      private GeoviProject GeoviProject
      {
         get
         {
            return geoviProject;
         }
         set
         {
            geoviProject = value;
            OnPropertyChanged(nameof(GeoviProject));
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

      private string selectedFieldName;
      public string SelectedFieldName
      {
         get
         {
            return this.selectedFieldName;
         }
         set
         {
            this.selectedFieldName = value;
            this.Settings.FieldName = value;
            OnPropertyChanged(nameof(SelectedFieldName));
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
            this.Settings.SelectedColorName = value;
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
      private GeoviService GeoviService
      {
         get
         {
            return this.GeoviProject.Where(x => x.ParentName == this.Title).FirstOrDefault();
         }
      }

      public Settings DefaultSettings
      {
         get
         {
            Settings settings = new Settings();
            settings.BasemapName = "Light";
            settings.FieldName = string.Empty;
            settings.SelectedColorName = "Red";
            return settings;
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

      private BasemapWrapper selectedBasemap;
      public BasemapWrapper SelectedBasemap
      {
         get
         {
            return this.selectedBasemap;
         }
         set
         {
            this.selectedBasemap = value;
            this.Settings.BasemapName = selectedBasemap.Basemap.Name;
            OnPropertyChanged(nameof(SelectedBasemap));
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

      public ICommand SaveChangesCommand
      {
         get; set;
      }

      private INavigationService NavigationService;

      public SettingsDetailPageViewModel(INavigationService navigationService)
      {
         this.NavigationService = navigationService;

         GoBackCommand = new RelayCommand(this.GoBackCommandFunc);

         SaveChangesCommand = new RelayCommand(this.SaveChangesCommandFunc);
      }


      private void GoBackCommandFunc()
      {
         this.NavigationService.Pop();
      }

      private void SaveChangesCommandFunc()
      {
         using (var dbContext = new CoreDbContext())
         {
            var list = dbContext.GeoviProjects.ToList();
            GeoviProject geoviData = dbContext.GeoviProjects
               .Include(x=>x.Settings)
               .Where(x => x.GeoviProjectID == this.GeoviProject.GeoviProjectID)
               .FirstOrDefault();
            foreach(var dt in dbContext.GeoviProjects)
            {
               if (dt.GeoviProjectID == this.GeoviProject.GeoviProjectID)
               {
                  geoviData = this.GeoviProject;
                  geoviData.Settings = this.Settings;
                  dbContext.SaveChanges();

               }
            }
            
         }
      }
      public override void OnPagePushing(params object[] parameters)
      {
         try
         {
            if (parameters != null && parameters.Length != 0)
            {
               GeoviProject geoviDatas = parameters[0] as GeoviProject;
               this.GeoviProject = geoviDatas;
               this.Title = geoviDatas.Name;
               this.Settings = this.GeoviProject.Settings;
               if (this.Settings == null)
               {
                  this.Settings = this.DefaultSettings;
               }
               this.SelectedColorName = this.Settings.SelectedColorName;
               GetFields();
            }
         }
         catch(Exception ex)
         {

         }
        
      }

      private async  void GetFields()
      {

         Uri serviceUri = new Uri(GeoviService.ServiceUrl.AbsoluteUri);
         // Initialize a new feature layer
         ServiceFeatureTable myFeatureTable = new ServiceFeatureTable(serviceUri);
         await myFeatureTable.LoadAsync();

         FeatureTable featureTable = ((FeatureTable)myFeatureTable);
         this.Fields = featureTable.Fields.Select(x => x.Name).ToList();


      }
   }
}
