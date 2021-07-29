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
              imgFotoUsuario.Source = ImageSource.FromUri(new Uri("https://images.vexels.com/media/users/3/204554/isolated/lists/53193fd7db3d2618ab56635e69e64515-pequenas-hojas-de-frutos-rojos.png"));

            //imgFotoUsuario.Source = ImageSource.FromFile("direccion.png");
        }
        
    }
}