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
      #region Observables
      ObservableCollection<GeoviProject> GeoviProjects { get; set; }

      ObservableCollection<string> GeoviDataByTitle { get; set; }

      #endregion

      #region Strings
      string Title { get; }
      string SelectedGeoviData { get; set; }
      #endregion

      #region Commands
      ICommand GeoviDataDeleteCommand { get;  set; }

      ICommand GeoviDataSelectedCommand { get;  set; }

      ICommand GeoviGoToDetailCommand { get; set; }
      #endregion

      #region DataService
      IGeoviDataService GeoviDataService { get; set; }

      INavigationService INavigationService { get; set; }
      #endregion
   }
}
