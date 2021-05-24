using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Geovi.Extensions
{
   public static class AppBarExtension
   {
      public static double Height => 50;

      public static Thickness Padding => new Thickness(0, Height, 0, 0);
   }
}
