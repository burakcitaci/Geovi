using Geovi.Net.IViewModels;
using Microsoft.Extensions.DependencyInjection;
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
   public partial class SettingsDetailPage : ContentPage
   {
      public SettingsDetailPage()
      {
         InitializeComponent();
         BindingContext = ((App)App.Current).ServiceProvider.GetRequiredService<ISettingsDetailPageViewModel>();

      }
   }
}