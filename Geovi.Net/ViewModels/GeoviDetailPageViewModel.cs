using Geovi.Net.IViewModels;
using Geovi.Net.Model;
using Geovi.Net.Services;
using Geovi.Net.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Geovi.Net.ViewModels
{
   public class GeoviDetailPageViewModel : BasePageViewModel, IGeoviDetailPageViewModel
   {
      public ICommand GoBackCommand { get; set; }

      public INavigationService NavigationService { get; set; }
      private string title = string.Empty;
      public string Title
      {
         get
         {
            return this.title;
         }
         set
         {
            title = value;
            OnPropertyChanged(nameof(Title));
         }
      }
      public GeoviData GeoviData { get; set; }
      public GeoviDataBy GeoviDatas { get; set; }

      public GeoviDetailPageViewModel(INavigationService navigationService)
      {
         this.NavigationService = navigationService;

         GoBackCommand = new RelayCommand(this.GoBackCommandFunc);
      }

      private void GoBackCommandFunc()
      {
         this.NavigationService.Pop();
      }

      public override void OnPagePushing(params object[] parameters)
      {
         if (parameters != null && parameters.Length != 0)
         {
            this.GeoviDatas = parameters[0] as GeoviDataBy;
            //GeoviData geoviDatas = parameters[0] as GeoviData;
            //this.Title = geoviDatas.Title;
            //this.GeoviData = geoviDatas;
         }

      }
   }
}
