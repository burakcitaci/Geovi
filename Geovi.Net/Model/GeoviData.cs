using Geovi.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geovi.Net.Model
{
   public class GeoviData
   {
      public string Title { get; set; }

      public Uri ServiceUrl { get; set; }

      public ServiceType ServiceType { get; set; }

      public string LayerName { get; set; }

      public string Description { get; set; }
   }
}
