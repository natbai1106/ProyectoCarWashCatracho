using PRMOVIL2CARWASH.Utils;
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
    public partial class PerfilUsuario : ContentPage
    {
        public PerfilUsuario()
        {
            InitializeComponent();

        }

        protected  override void OnAppearing()
        {
            lblNombreCompleto.Text =  App.CurrentUser().Nombre;
            lblCorreo.Text= App.CurrentUser().Correo;
            lblTelefono.Text= App.CurrentUser().Telefono;

            base.OnAppearing();
        }

        private async void btnEditarPerfil_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigUsuario());
        }

        private async void btnCambiarContraseña_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CambioContra(Constanst.TYPE_CHANGE,""));
        }
    }
}