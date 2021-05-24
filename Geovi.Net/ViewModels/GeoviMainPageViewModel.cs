using Geovi.Net.IViewModels;
using Geovi.Net.Model;
using Geovi.Net.Services;
using Geovi.Net.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;

namespace Geovi.Net.ViewModels
{
   public class GeoviMainPageViewModel : BasePageViewModel, IGeoviMainPageViewModel
   {
      public string Title { get => "HelloWorld"; }

      private IEnumerable<GeoviDataBy> geoviDatas;
      public IEnumerable<GeoviDataBy> GeoviDatas 
      {
         get => geoviDatas;
         set
         {
            geoviDatas = value;
            OnPropertyChanged(nameof(GeoviDatas));
         }
      }

      private IEnumerable<string> geoviDataByTitle;
      public IEnumerable<string> GeoviDataByTitle
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
               GeoviDatas = geoviDataService.GetAllBy().Where(x => x.FilterName == SelectedGeoviData).ToList();
         });

         //GeoviDatas = geoviDataService.GetAllBy();
         GeoviDataByTitle = geoviDataService.GetGeoviDataTitles();
      }
   }
}
