using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Geovi.Net.IViewModels
{
   public interface ISettingsPageViewModel
   {
      ObservableCollection<GeoviDataBy> GeoviDatas { get; set; }

      IEnumerable<int> ZoomLevels { get; set; }

      int SelectedZoomLevel { get; set; }

      ICommand GoToSettingsDetailCommand { get; set; }


   }
}
