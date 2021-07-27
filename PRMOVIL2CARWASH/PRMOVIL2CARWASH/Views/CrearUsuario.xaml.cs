using Plugin.Media;
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
    public partial class CrearUsuario : ContentPage
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }
        private async void btnConfirmar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Validacion());
        }
        private void btnLimpiar_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnFotoUsuario_Clicked(object sender, EventArgs e)
        {
            //var foto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());
            var fotoTomada = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());

            if (fotoTomada != null)
            {
                //variable utilizada para almacenar la foto tomada en formado 
                // arrayImagen = null;
                //BtnGuardar.IsVisible = true;
                //MemoryStream memoryStream = new MemoryStream();// creamos un flujo de memoria temporal

                //fotoTomada.GetStream().CopyTo(memoryStream);
                //arrayImagen = memoryStream.ToArray();

                // se muestra la imagen pero aun no se guarda
                this.imgFotoUsuario.Source = ImageSource.FromStream(() => { return fotoTomada.GetStream(); });
            }
        }
    }
}