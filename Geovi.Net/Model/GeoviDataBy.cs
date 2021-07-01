using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Geovi.Net.Model
{
   public class GeoviDataBy : ObservableCollection<GeoviData>
   {
      [System.ComponentModel.DataAnnotations.Key]
      public int ID { get; set; }
      public string FilterBy { get; set; }
      public string FilterName { get; set; }
      public GeoviDataBy(string filterBy) : base()
      {
         this.FilterBy = filterBy;
         this.FilterName = filterBy;
      }

      public GeoviDataBy() { }
   }
}
