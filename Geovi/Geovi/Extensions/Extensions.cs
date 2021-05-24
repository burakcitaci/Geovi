using Geovi.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace Geovi.Extensions
{

   public static class Extensions
   {
      public static PagesEnum GetCurrentPage(this Shell shell)
      {
         string str = shell.CurrentState.Location.OriginalString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

         return (PagesEnum)Enum.Parse(typeof(PagesEnum), str);
      }

      public static T GetValue<T>(this ResourceDictionary dictionary, string key)
      {
         object value;

         dictionary.TryGetValue(key, out value);

         return (T)value;
      }

      public static Color GetColour(this object value)
      {
         if (value == null)
            return Color.Transparent;
         if (value.ToString() == "")
            return Color.Transparent;
         if (value is Color color)
            return color;
         else if (value is DynamicResource resource)
            return Xamarin.Forms.Application.Current.Resources.GetValue<Color>(resource.Key);
         else if (value is string code)
            return Color.FromHex(code);
         else
            return Color.Transparent;
      }

      public static string GetDescription(this System.Enum value)
      {
         FieldInfo fi = value.GetType().GetField(value.ToString());
         DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
         if (attributes.Length > 0)
            return attributes[0].Description;
         else
            return value.ToString();
      }
   }
}
