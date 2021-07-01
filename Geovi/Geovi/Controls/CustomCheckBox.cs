using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Geovi.Controls
{
   public class CustomCheckBox : CheckBox
   {
      public string Text
      {
         get => (string)GetValue(TextProperty);
         set => SetValue(TextProperty, value);
      }
      public bool IsSelected
      {
         get => (bool)GetValue(IsSelectedProperty);
         set => SetValue(IsSelectedProperty, value);
      }

      public static readonly BindableProperty TextProperty =
         BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomCheckBox), "", BindingMode.OneWay, propertyChanged: (bindable, o, n) =>
         {
            
         });


      public static readonly BindableProperty IsSelectedProperty =
         BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(CustomCheckBox), false, BindingMode.TwoWay, propertyChanged: (bindable, o, n) =>
         {
            CustomCheckBox view = bindable as CustomCheckBox;

            if (view.IsChecked)
            {
              
               view.SetDynamicResource(CustomCheckBox.BackgroundColorProperty, "Red");
            }
            else
            {
              
               view.SetDynamicResource(CustomCheckBox.BackgroundColorProperty, "DetailColour");
            }
         });
   }
}
