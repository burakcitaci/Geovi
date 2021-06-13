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

namespace Geovi.Net.ViewModels
{
   public class GeoviMainPageViewModel : BasePageViewModel, IGeoviMainPageViewModel
   {
      public string Title { get => "HelloWorld"; }

      private ObservableCollection<GeoviDataBy> geoviDatas;
      public ObservableCollection<GeoviDataBy> GeoviDatas 
      {
         get => geoviDatas;
         set
         {
            geoviDatas = value;
            OnPropertyChanged(nameof(GeoviDatas));
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
      public string DeleteIcon { get => "outline_delete_white_24.png"; }

      public string FavoriteIcon { get => "outline_star_rate_white_24.png"; }
      public ICommand GeoviDataSelectedCommand { get; set; }
      public ICommand GeoviDataDeleteCommand { get; set; }
      public IGeoviDataService GeoviDataService { get; set; }
      public ICommand GeoviGoToDetailCommand { get; set; }
      public INavigationService INavigationService { get; set; }
      public GeoviMainPageViewModel(INavigationService navigationService, IGeoviDataService geoviDataService)
      {
         GeoviDataService = geoviDataService;
         this.INavigationService = navigationService;
         GeoviDatas = geoviDataService.GetAllBy();
         GeoviDataSelectedCommand = new RelayCommand(this.SelectedCommandFunc);
         GeoviGoToDetailCommand = new RelayCommand(this.GoToDetailCommandFunc);
         GeoviDataDeleteCommand = new RelayCommand(this.DeleteCommandFunc);
         //GeoviDatas = geoviDataService.GetAllBy();
         GeoviDataByTitle = (ObservableCollection<string>)geoviDataService.GetGeoviDataTitles();
      }

      private void SelectedCommandFunc(object parameter)
      {
         SelectedGeoviData = parameter != null ? parameter.ToString() : null;
         if (SelectedGeoviData == null)
            GeoviDatas = this.GeoviDataService.GetAllBy();
         else
            GeoviDatas = this.GeoviDataService.GetBy(SelectedGeoviData);

      }

      private void DeleteCommandFunc(object parameter)
      {
         if (parameter != null)
         {
            GeoviDataBy data = parameter as GeoviDataBy;
            this.GeoviDatas.Remove(data);
            this.GeoviDataByTitle.Remove(data.FilterName);
         }
      }

      private void GoToDetailCommandFunc(object parameter)
      {
         this.INavigationService.Push(PagesEnum.GeoviDetailPage, parameter);
      }
   }
}
