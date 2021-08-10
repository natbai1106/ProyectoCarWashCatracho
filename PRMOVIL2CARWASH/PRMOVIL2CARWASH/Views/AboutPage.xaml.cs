using PRMOVIL2CARWASH.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(this);
        }

        protected override void OnAppearing()
        {
            spNombre.Text = App.CurrentUser().Nombre;

            base.OnAppearing();
        }
    }
}