using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Geovi.Net.ViewModels
{
   public class BasePageViewModel : INotifyPropertyChanged, IBasePageViewModel
   {
      public virtual void OnPagePushing(params object[] parameters) { }
      public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
