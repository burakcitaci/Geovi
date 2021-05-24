using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Geovi.Net.ViewModels
{
   public interface IBasePageViewModel : INotifyPropertyChanged
   {
      /// <summary>
      /// Navigation to another Page
      /// </summary>
      /// <param name="parameters"></param>
      void OnPagePushing(params object[] parameters);

      /// <summary>
      /// Property changed
      /// </summary>
      /// <param name="property"></param>
      void OnPropertyChanged(string property);
   }
}
