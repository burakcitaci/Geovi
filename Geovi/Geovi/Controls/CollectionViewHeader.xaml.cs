using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Geovi.Controls
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class CollectionViewHeader : ContentView
   {
      public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                                        propertyName: "TitleText",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(CollectionViewHeader),
                                                        defaultValue: "",
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: TitleTextPropertyChanged);
      private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
      {
         var control = (CollectionViewHeader)bindable;
         control.TitleText = newValue.ToString();
      }

      public string Title { get => this.TitleText; }
      public string TitleText
      {
         get => (string)GetValue(TitleTextProperty);
         set
         {
            if(this.headerLabel != null)
            {
               this.headerLabel.Text = value;
            }
            SetValue(TitleTextProperty, value);
         }
      }
      public string HeaderIconAdd { get; set; }
      public string HeaderIconDelete { get; set; }
      public CollectionViewHeader()
      {
         HeaderIconAdd = "outline_add_task_black_20.png";
         HeaderIconDelete = "outline_delete_forever_black_20.png";
         InitializeComponent();
         //this.headerLabel.Text = "TitleText";
      }

      private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
      {
         var bc = this.BindingContext;
      }
   }
}