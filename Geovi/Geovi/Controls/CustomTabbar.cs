using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Geovi.Controls
{
   public class CustomTabbar : TabBar
   {
      public TabbarView TabBarView { get; private set; }

      public CustomTabbar()
      {
         TabBarView = new TabbarView();

         TabBarView.HeightRequest = 100;
      }
   }
}
