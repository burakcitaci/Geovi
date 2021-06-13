using Geovi.Net.IViewModels;
using Geovi.Net.Services;
using Geovi.Net.ViewModels;
using Geovi.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Geovi
{
   public partial class App : Application
   {
      public IServiceProvider ServiceProvider { get; private set; }

      public App()
      {
         InitializeComponent();
         Device.SetFlags(new string[] { "Shapes_Experimental", "Brush_Experimental" });

         var services = new ServiceCollection();

         services.AddSingleton<INavigationService, NavigationService>();
         services.AddSingleton<IGeoviDataService, GeoviDataService>();
         //services.AddSingleton<ISettingsDataService, SettingsDataService>();
         services.AddTransient<IGeoviMainPageViewModel, GeoviMainPageViewModel>();
         services.AddTransient<ISettingsPageViewModel, SettingsPageViewModel>();
         services.AddTransient<ISettingsDetailPageViewModel, SettingsDetailPageViewModel>();
         services.AddTransient<IGeoviDetailPageViewModel, GeoviDetailPageViewModel>();

         ServiceProvider = services.BuildServiceProvider();
         MainPage = new AppShell();
      }

      protected override void OnStart()
      {
      }

      protected override void OnSleep()
      {
      }

      protected override void OnResume()
      {
      }
   }
}
