using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
   }
}
