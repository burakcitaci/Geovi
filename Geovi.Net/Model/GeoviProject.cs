using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Geovi.Net.Model
{
   public class GeoviProject : ObservableCollection<GeoviService>
   {
      [Key]
      public int GeoviProjectID { get; set; }

      public string Name { get; set; }

      public Settings Settings { get; set; }

      public ObservableCollection<GeoviService> GeoviServices => this;

      public GeoviProject(string name, ObservableCollection<GeoviService> geoviServices) :
         base(geoviServices)
      {
         this.Name = name;
      }

      public GeoviProject() { }
   }
}
