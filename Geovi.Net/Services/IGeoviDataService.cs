using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Geovi.Net.Services
{
   public interface IGeoviDataService
   {
      ObservableCollection<GeoviService> GetAll();

      ObservableCollection<GeoviProject> GetBy(string parameter);

      ObservableCollection<string> GetGeoviDataTitles();

      ObservableCollection<GeoviProject> GetAllBy();
   }
}
