using System;
using PRMOVIL2CARWASH.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigUsuario : ContentPage
    {
        public ConfigUsuario()
        {
            InitializeComponent();
            BindingContext = new UsersViewModel(this);
            imgFotoUsuario.Source = ImageSource.FromUri(new Uri("https://images.vexels.com/media/users/3/204554/isolated/lists/53193fd7db3d2618ab56635e69e64515-pequenas-hojas-de-frutos-rojos.png"));

            //imgFotoUsuario.Source = ImageSource.FromFile("direccion.png");
        }

        private void btnGuardarCambios_Clicked(object sender, EventArgs e)
        {

        }

        private void btnLimpiarPerfilUsuario_Clicked(object sender, EventArgs e)
        {

        }
    }
}