using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Geovi.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SettingsPage : ContentPage
   {
      public string HeaderIconLaunch { get; set; }

      public SettingsPage()
      {
         HeaderIconLaunch = "outline_add_task_black_20.png";
         InitializeComponent();
      }
   }
}