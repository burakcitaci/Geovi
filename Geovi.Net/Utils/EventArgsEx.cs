using Esri.ArcGISRuntime.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geovi.Net.Utils
{
   class EventArgsEx
   {
   }

   public class MapLoadedEventArgs
   {
      public MapLoadedEventArgs(string text) { Text = text; }
      public string Text { get; } // readonly
   }

   public class LayerCheckedEventArgs
   {
      public LayerCheckedEventArgs(bool layerChecked)
      {
         this.Checked = layerChecked;
      }

      public bool Checked { get; set; }
   }
}
