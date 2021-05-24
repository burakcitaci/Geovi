using System;
using System.Collections.Generic;
using System.Text;

namespace Geovi.Net.Model
{
   public class GeoviDataBy : List<GeoviData>
   {
      public string FilterBy;

      public string FilterName { get; set; }
      public GeoviDataBy(string filterBy) : base()
      {
         this.FilterBy = filterBy;
         this.FilterName = filterBy;
      }
   }
}
