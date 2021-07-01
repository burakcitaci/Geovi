using System;
using System.Collections.Generic;
using System.Text;

namespace Geovi.Net.Utils
{
   public class Delegates
   {
      public delegate void MapLoadedEventHandler(object e, MapLoadedEventArgs args);

      public delegate void LayerCheckedEventHandler(object e, LayerCheckedEventArgs args);

   }
}
