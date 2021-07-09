using Geovi.Net.IViewModels;
using Geovi.Net.Model;
using Geovi.Net.Services;
using Geovi.Net.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Geovi.Net.Enums;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms;

namespace Geovi.Net.ViewModels
{
   public class GeoviMainPageViewModel : BasePageViewModel, IGeoviMainPageViewModel
   {
      #region Observables
      private ObservableCollection<GeoviProject> geoviProjects;
      public ObservableCollection<GeoviProject> GeoviProjects
      {
         get => geoviProjects;
         set
         {
            geoviProjects = value;
            OnPropertyChanged(nameof(GeoviProjects));
         }
      }

      private ObservableCollection<string> geoviDataByTitle;
      public ObservableCollection<string> GeoviDataByTitle
      {
         get => geoviDataByTitle;
         set
         {
            geoviDataByTitle = value;
            OnPropertyChanged(nameof(GeoviDataByTitle));
         }
      }
      #endregion

      #region Strings
      public string Title { get => "HelloWorld"; }
      private string selectedGeoviData;
      public string SelectedGeoviData
      {
         get => selectedGeoviData;
         set
         {
            selectedGeoviData = value;
            OnPropertyChanged(nameof(SelectedGeoviData));
         }
      }
    
      #endregion
 
      #region Commands
      /// <summary>
      /// 
      /// </summary>
      public ICommand GeoviDataSelectedCommand { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public ICommand GeoviDataDeleteCommand { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public ICommand GeoviGoToDetailCommand { get; set; }

      #endregion
     
      #region Services
      public IGeoviDataService GeoviDataService { get; set; }
      public INavigationService INavigationService { get; set; }
      #endregion

      public GeoviMainPageViewModel(INavigationService navigationService, IGeoviDataService geoviDataService)
      {
        
         GeoviDataService = geoviDataService;
         this.INavigationService = navigationService;
         this.GeoviProjects = geoviDataService.GetAllBy();
         GeoviDataSelectedCommand = new RelayCommand(this.SelectedCommandFunc);
         GeoviGoToDetailCommand = new RelayCommand(this.GoToDetailCommandFunc);
         GeoviDataDeleteCommand = new RelayCommand(this.DeleteCommandFunc);
         //GeoviDatas = geoviDataService.GetAllBy();
         GeoviDataByTitle = (ObservableCollection<string>)geoviDataService.GetGeoviDataTitles();
      }

      /// <summary>
      /// Filters Geovi project selectedi in TabView
      /// </summary>
      /// <param name="parameter"></param>
      private void SelectedCommandFunc(object parameter)
      {
         SelectedGeoviData = parameter != null ? parameter.ToString() : null;
         if (SelectedGeoviData == null)
            GeoviProjects = this.GeoviDataService.GetAllBy();
         else
            GeoviProjects = this.GeoviDataService.GetBy(SelectedGeoviData);
      }

      /// <summary>
      /// Deletes Geovi project
      /// </summary>
      /// <param name="parameter"></param>
      private void DeleteCommandFunc(object parameter)
      {
         if (parameter != null)
         {
            GeoviProject project = parameter as GeoviProject;
            this.GeoviProjects.Remove(project);
            this.GeoviDataByTitle.Remove(project.Name);
         }
      }

      /// <summary>
      /// Navigates to details page of GeoviProject
      /// </summary>
      /// <param name="parameter"></param>
      private void GoToDetailCommandFunc(object parameter)
      {
         this.INavigationService.Push(PagesEnum.GeoviDetailPage, parameter);
      }
   }
}
