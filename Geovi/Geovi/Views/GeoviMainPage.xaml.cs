using Geovi.Net.IViewModels;
using Geovi.Net.Model;
using Geovi.Net.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Geovi.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class GeoviMainPage : ContentPage
   {
      private GeoviMainPageViewModel viewModel;
      private GeoviMainPageViewModel ViewModel 
      { 
         get
         {
            if(viewModel == null)
            {
               viewModel = (this.BindingContext as GeoviMainPageViewModel);
            }
            return viewModel;
         }
      }

      public ObservableCollection<GeoviDataBy> GeoviDatas;
      public ObservableCollection<string> GeoviDatasByTitle;
      public GeoviData GeoviData;
      public GeoviMainPage()
      {
         InitializeComponent();
         BindingContext = ((App)App.Current).ServiceProvider.GetRequiredService<IGeoviMainPageViewModel>();
         
         GeoviDatas = this.ViewModel.GeoviDatas;
         GeoviDatasByTitle = this.ViewModel.GeoviDataByTitle;

      }

      private void addToFavorite_Invoked(object sender, EventArgs e)
      {
         this.GeoviData = (((SwipeItem)sender).BindingContext as GeoviData);
         int index = this.GeoviDatas.ToList().FindIndex(x => x.FilterName == this.GeoviData.ParentName);
         this.GeoviDatas[index].Remove(this.GeoviData);
         if(this.GeoviDatas[index].Count() == 0)
         {
            this.GeoviDatas.RemoveAt(index);
            this.GeoviDatasByTitle.Remove(this.GeoviData.ParentName);
         }
      }

      private void deleteLayer_Invoked(object sender, EventArgs e)
      {

      }
   }
}