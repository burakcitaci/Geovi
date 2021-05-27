using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Geovi.Converters
{
   public class SwipeItemConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if(parameter == null)
         {
            return "Delete '" + value.ToString() + "'!";
         }
         else
         {
            return "ADD '" + value.ToString() + "' to favorites";
         }
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return value;
      }
   }
}
