using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PRMOVIL2CARWASH
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarAutos : ContentPage
    {
        public RegistrarAutos()
        {
            InitializeComponent();
        }

        private void PiModeloAutos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void btnFotoAutos_Clicked(object sender, EventArgs e)
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
                this.imgFotoAuto.Source = ImageSource.FromStream(() => { return fotoTomada.GetStream(); });
            }
        }
    }
}