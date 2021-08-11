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
            BindingContext = new UpdateUserViewModel(this);

        }

        private void btnGuardarCambios_Clicked(object sender, EventArgs e)
        {

        }

        private void btnLimpiarPerfilUsuario_Clicked(object sender, EventArgs e)
        {

        }

       
    }
}