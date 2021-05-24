using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geovi.Net.IViewModels
{
   public interface IGeoviMainPageViewModel
   {
      string Title { get; }

      IEnumerable<GeoviDataBy> GeoviDatas { get; set; }

      IEnumerable<string> GeoviDataByTitle { get; set; }

      string SelectedGeoviData { get; set; }
      string DeleteIcon { get; }

      string FavoriteIcon { get; }
   }
}
