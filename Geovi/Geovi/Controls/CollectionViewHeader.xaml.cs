using Geovi.Net.IViewModels;
using Geovi.Net.Model;
using Geovi.Net.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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

      private GeoviMainPageViewModel viewModel;
      private GeoviMainPageViewModel ViewModel
      {
         get
         {
            if(viewModel == null)
            {
               viewModel = (GeoviMainPageViewModel)((App)App.Current)
                  .ServiceProvider
                  .GetRequiredService<IGeoviMainPageViewModel>();

            }
            return viewModel;
         }
      }
      public string HeaderIconAdd { get; set; }
      public string HeaderIconDelete { get; set; }
      public string HeaderIconLaunch { get; set; }

      public string HeaderIconStar { get; set; }
      public CollectionViewHeader()
      {
         HeaderIconAdd = "outline_add_task_black_20.png";
         HeaderIconDelete = "outline_delete_black_36dp.png";
         HeaderIconLaunch = "outline_launch_black_36dp.png";
         HeaderIconStar = "outline_grade_black_36dp.png";
         InitializeComponent();
         this.BindingContext = this.ViewModel;
         //this.headerLabel.Text = "TitleText";
      }

      private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
      {
         GeoviProject geoviDatas = this.BindingContext as GeoviProject;

         //int index = ViewModel.GeoviDatas.ToList().FindIndex(x=>x.FilterName == geoviDatas.FilterName);
         //this.ViewModel.GeoviDatas.RemoveAt(index);
         this.ViewModel.GeoviProjects.Remove(geoviDatas);
         this.ViewModel.GeoviDataByTitle.Remove(geoviDatas.Name);
      }
   }
}