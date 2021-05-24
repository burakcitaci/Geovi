﻿using System;
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
      public string AvatarImage { get; set; }

      public AppBar()
      {
         GeoviLogoImage = "logo_header.png";
         
         InitializeComponent();
      }
   }
}