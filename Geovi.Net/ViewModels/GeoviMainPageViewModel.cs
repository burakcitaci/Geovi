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
      public ICommand WfsDataSelectedCommand { get; private set; }
      public GeoviMainPageViewModel(INavigationService navigationService, IGeoviDataService geoviDataService)
      {
         GeoviDatas = geoviDataService.GetAllBy();
         WfsDataSelectedCommand = new RelayCommand(parameter =>
         {
            SelectedGeoviData = parameter != null ? parameter.ToString() : null;
            if (SelectedGeoviData == null)
               GeoviDatas = geoviDataService.GetAllBy();
            else
               GeoviDatas = (ObservableCollection<GeoviDataBy>)geoviDataService.GetBy(SelectedGeoviData);
         });

         //GeoviDatas = geoviDataService.GetAllBy();
         GeoviDataByTitle = (ObservableCollection<string>)geoviDataService.GetGeoviDataTitles();
      }
   }
}
