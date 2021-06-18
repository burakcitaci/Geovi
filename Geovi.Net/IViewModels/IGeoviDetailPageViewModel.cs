using Esri.ArcGISRuntime.Data;
using Geovi.Net.Model;
using Geovi.Net.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Geovi.Net.IViewModels
{
   public interface IGeoviDetailPageViewModel
   {
      //ServiceFeatureTable ServiceFeatureTable { get; set; }
      ICommand GoBackCommand { get; set; }

      INavigationService NavigationService { get; set; }

      GeoviData GeoviData { get; set; }

      GeoviDataBy GeoviDatas { get; set; }
      string Title { get; set; }
      //ICommand GoBackCommand { get; }
      //Match Match { get; set; }
      void OnPagePushing(params object[] parameters);
   }
}
