using Geovi.Net.Model;
using Geovi.Net.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Geovi.Net.IViewModels
{
   public interface IGeoviMainPageViewModel
   {
      string Title { get; }

      ObservableCollection<GeoviDataBy> GeoviDatas { get; set; }

      ObservableCollection<string> GeoviDataByTitle { get; set; }

      string SelectedGeoviData { get; set; }
      string DeleteIcon { get; }

      string FavoriteIcon { get; }

      #region Commands
      ICommand GeoviDataDeleteCommand { get;  set; }

      ICommand GeoviDataSelectedCommand { get;  set; }
      #endregion

      #region DataService
      IGeoviDataService GeoviDataService { get; set; }
      #endregion
   }
}
