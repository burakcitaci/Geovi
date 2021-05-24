using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Geovi.Converters
{
   public class FFImageSourceConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         try
         {
            var emb = new EmbeddedResourceImageSource("Geovi.Images." + value?.ToString(),
           typeof(FFImageSourceConverter)
           .GetTypeInfo()
           .Assembly);

            return emb;
         }
         catch(Exception ex)
         {

            return null;
         }
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return value;
      }
   }
}
