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
   public class SettingsDetailPageViewModel : BasePageViewModel, ISettingsDetailPageViewModel
   {
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

      public ICommand GoBackCommand 
      {
         get;set;
      }

      private INavigationService NavigationService;

      public SettingsDetailPageViewModel(INavigationService navigationService)
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
            GeoviDataBy geoviDatas = parameters[0] as GeoviDataBy;
            this.Title = geoviDatas.FilterName;
         }
      }
   }
}
