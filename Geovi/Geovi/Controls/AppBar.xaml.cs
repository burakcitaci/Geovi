using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geovi.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Geovi.Controls
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class AppBar : ContentView
   {

      public string GeoviLogoImage { get; set; }
      public string HeaderIconAdd { get; set; }
      public AppBar()
      {
         GeoviLogoImage = "logo_header.png";
         HeaderIconAdd = "outline_library_add_white_24dp.png";


         InitializeComponent();
      }
   }
}