using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Geovi.Net.Model
{
 
   public class GeoviDataByBy : ObservableCollection<GeoviData>
   {
      [System.ComponentModel.DataAnnotations.Key]
      public int ID { get; set; }
      
      public string FilterBy { get; set; }

      public string FilterName { get; set; }

      public Settings Settings { get; set; }

      public ObservableCollection<GeoviData> Liste => this;
      public GeoviDataByBy(string filterBy, ObservableCollection<GeoviData> geovis) : base(geovis)
      {
         this.FilterBy = filterBy;
         this.FilterName = filterBy; 
      }

      public GeoviDataByBy() { }
   }
}
