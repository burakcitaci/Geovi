using Geovi.Net.Enums;
using Geovi.Net.Services;
using Geovi.Net.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Geovi.Services
{
   public class NavigationService : INavigationService
   {
      public void Pop()
      {
         Shell.Current.Navigation.PopAsync();
      }

      public void Push(PagesEnum page, params object[] parameters)
      {
         Shell.Current.GoToAsync(page.ToString());
         Page lastPage = Shell.Current.Navigation.NavigationStack.LastOrDefault();

         if (lastPage != null)
            ((IBasePageViewModel)lastPage.BindingContext).OnPagePushing(parameters);
      }
   }
}
