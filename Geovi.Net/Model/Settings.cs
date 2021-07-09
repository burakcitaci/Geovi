using Esri.ArcGISRuntime.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xamarin.Forms;

namespace Geovi.Net.Model
{
   public class Settings
   {
      [System.ComponentModel.DataAnnotations.Key]
      public int ID { get; set; }
      public string SelectedColorName { get; set; }

      public string FieldName { get; set; }

      public string BasemapName { get; set; }

      public Settings() { }
   }
}
