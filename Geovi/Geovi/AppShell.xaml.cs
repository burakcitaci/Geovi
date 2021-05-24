﻿using Geovi.Controls;
using Geovi.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geovi.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Geovi.Views;

namespace Geovi
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class AppShell : Shell
   {
      public AppShell()
      {
         InitializeComponent();
         
         tabBar.Items.Add(new ShellContent { Route = PagesEnum.GeoviMainPage.ToString(), ContentTemplate = new DataTemplate(typeof(GeoviMainPage)) });
         tabBar.Items.Add(new ShellContent { Route = PagesEnum.GeoviMainPage.ToString(), ContentTemplate = new DataTemplate(typeof(GeoviMainPage)) });


      }
      protected override async void OnNavigated(ShellNavigatedEventArgs args)
      {
         base.OnNavigated(args);

         await UpdateTabBarVisibility();
      }

      public async Task<bool> UpdateTabBarVisibility()
      {
         var page = Shell.Current.GetCurrentPage();

         bool isHidden = page == PagesEnum.WfsDetailPage;

         CustomTabbar tabBar = (CustomTabbar)Items.FirstOrDefault();

         if (isHidden)
            await tabBar?.TabBarView.HideTabBarAsync();
         else
            await tabBar?.TabBarView.ShowTabBarAsync();

         return isHidden;
      }
   }
}