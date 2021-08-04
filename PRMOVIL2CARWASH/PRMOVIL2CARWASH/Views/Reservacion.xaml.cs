using PRMOVIL2CARWASH.ViewModels;
using PRMOVIL2CARWASH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reservacion : ContentPage
    {
        public Reservacion()
        {
            InitializeComponent();

        }

        private async void SwitchMapa_Toggled(object sender, ToggledEventArgs e)
        {
            if (true)
            {
                OnBackButtonPressed();
            }
            


        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Servicio a Domicilio", " Para poder hacer uso del servicio a domicilio es necesario que ingrese su ubicacion, ¿Quieres abrir el Mapa?", "Si", "No");
                if (result) await Navigation.PushAsync(new Mapas()); ;

            });


            return true;
        }
        //private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var viewModel = BindingContext as ServicesViewModels;
        //    var service = e.Item as Service;

        //}



        //private void Handle_SearchButtonPressed(object sender, EventArgs e)
        //{

        //    //var ServicieSearched = Services.Where(c => c.Contains(ServicieSearchBar.Text));
        //    ServicieSearchList.ItemsSource = Services;
        //}
    }
}