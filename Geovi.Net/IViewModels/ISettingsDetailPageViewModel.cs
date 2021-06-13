using Geovi.Net.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Geovi.Net.IViewModels
{
   public interface ISettingsDetailPageViewModel
   {
      ICommand GoBackCommand { get; set; }

      string Title { get; set; }
      //ICommand GoBackCommand { get; }
      //Match Match { get; set; }
      void OnPagePushing(params object[] parameters);
   }
}
