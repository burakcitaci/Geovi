using Geovi.Net.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Geovi.Net.Model
{
   public class GeoviData
   {
      [System.ComponentModel.DataAnnotations.Key]
      public int ID { get; set; }
      public string Title { get; set; }

      public Uri ServiceUrl { get; set; }

      public ServiceType ServiceType { get; set; }

      public string LayerName { get; set; }

      public string Description { get; set; }

      public string ParentName { get; set; }

      public GeoviData() { }

   }
}
