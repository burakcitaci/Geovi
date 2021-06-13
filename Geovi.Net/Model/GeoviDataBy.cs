using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Geovi.Net.Model
{
   public class GeoviDataBy : ObservableCollection<GeoviData>
   {
      public string FilterBy;
      private IEnumerable<int> zoomLevels = new List<int> { 1, 2, 3, 4 };

      public IEnumerable<int> ZoomLevels
      {
         get
         {
            return zoomLevels;
         }
         set
         {
            zoomLevels = value;
            //OnPropertyChanged(nameof(ZoomLevels));
         }
      }
      public string FilterName { get; set; }
      public GeoviDataBy(string filterBy) : base()
      {
         this.FilterBy = filterBy;
         this.FilterName = filterBy;
      }
   }
}
