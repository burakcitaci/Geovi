using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Geovi.Net.Services
{
   public interface IGeoviDataService
   {
      ObservableCollection<GeoviData> GetAll();

      ObservableCollection<GeoviDataBy> GetBy(string parameter);

      ObservableCollection<string> GetGeoviDataTitles();

      ObservableCollection<GeoviDataBy> GetAllBy();
   }
}
