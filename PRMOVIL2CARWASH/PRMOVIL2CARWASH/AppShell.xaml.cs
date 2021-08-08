using PRMOVIL2CARWASH.ViewModels;
using PRMOVIL2CARWASH.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(CambioContra), typeof(CambioContra));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //Routing.RegisterRoute(nameof(ConfigUsuario), typeof(ConfigUsuario));
            //Routing.RegisterRoute(nameof(Cotizacion), typeof(Cotizacion));
            Routing.RegisterRoute(nameof(CrearUsuario), typeof(CrearUsuario));
            Routing.RegisterRoute(nameof(Facturacion), typeof(Facturacion));
            Routing.RegisterRoute(nameof(Mapas), typeof(Mapas));
            Routing.RegisterRoute(nameof(CrearAuto), typeof(CrearAuto));
            Routing.RegisterRoute(nameof(Reservaciones), typeof(Reservaciones));
            Routing.RegisterRoute(nameof(Validacion), typeof(Validacion));
            Routing.RegisterRoute(nameof(AcercaDe), typeof(AcercaDe));

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
           // Routing.RegisterRoute(nameof(Validacion), typeof(LoginPage));

            Routing.RegisterRoute(nameof(PerfilUsuario), typeof(PerfilUsuario));
            Routing.RegisterRoute(nameof(CambioAceite), typeof(CambioAceite));
            Routing.RegisterRoute(nameof(ListaCarros), typeof(ListaCarros));


        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
           
        }

        private async void LogoutClicked(object sender, EventArgs e)
        {
            await App.CurrentUser().LogOut();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
