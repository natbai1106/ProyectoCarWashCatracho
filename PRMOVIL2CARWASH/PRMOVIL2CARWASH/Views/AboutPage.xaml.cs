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

        private async void btnCambioAceite_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new CambioAceite());
        }

        private async void btnReservar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservacion());

        }

        private async void btnListaReservar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservaciones());
        }
    }
}