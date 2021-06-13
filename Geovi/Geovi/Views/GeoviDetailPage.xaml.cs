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
   public partial class GeoviDetailPage : ContentPage
   {
      public GeoviDetailPage()
      {
         InitializeComponent();
         BindingContext = ((App)App.Current).ServiceProvider.GetRequiredService<IGeoviDetailPageViewModel>();
      }
   }
}