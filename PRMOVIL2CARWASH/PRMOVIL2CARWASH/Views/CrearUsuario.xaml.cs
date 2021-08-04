using Plugin.Media;
using PRMOVIL2CARWASH.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH.Views
{
    public partial class CrearUsuario : ContentPage
    {
        public CrearUsuario()
        {
            InitializeComponent();

            BindingContext = new UsersViewModel(this);

          
            //imgFotoUsuario.Source = ImageSource.FromFile("direccion.png");
        }
    }
}