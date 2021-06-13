using Geovi.Net.Enums;
using Geovi.Net.IViewModels;
using Geovi.Net.Model;
using Geovi.Net.Services;
using Geovi.Net.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Geovi.Net.ViewModels
{
   public class SettingsPageViewModel : BasePageViewModel, ISettingsPageViewModel
   {
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

      private IEnumerable<int> zoomLevels = new List<int> { 1, 2, 3, 4 };
      public IEnumerable<int> ZoomLevels
      {
         get
         {
            
            return zoomLevels;
         }
         set
         {
            zoomLevels = value;
            OnPropertyChanged(nameof(ZoomLevels));
         }
      }

      private INavigationService NavigationService;
      public int SelectedZoomLevel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public ICommand GoToSettingsDetailCommand { get; set; }

      public SettingsPageViewModel(INavigationService navigationService, IGeoviDataService geoviDataService)
      {
         this.NavigationService = navigationService;
         GeoviDatas = geoviDataService.GetAllBy();
         GoToSettingsDetailCommand = new RelayCommand(this.GoToSettingsDetailCommandFunc);
      }

      private void GoToSettingsDetailCommandFunc(object parameter)
      {
         this.NavigationService.Push(PagesEnum.SettingsDetailPage, parameter);
      }
   }
}
