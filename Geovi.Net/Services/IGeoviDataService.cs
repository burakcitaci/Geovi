using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geovi.Net.Services
{
   public interface IGeoviDataService
   {
      IEnumerable<GeoviData> GetAll();

      IEnumerable<GeoviDataBy> GetBy(string parameter);

      IEnumerable<string> GetGeoviDataTitles();
      IEnumerable<GeoviDataBy> GetAllBy();
   }
}
