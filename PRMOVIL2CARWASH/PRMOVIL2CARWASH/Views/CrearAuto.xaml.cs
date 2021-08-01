using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using PRMOVIL2CARWASH.ViewModels;

namespace PRMOVIL2CARWASH.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearAuto : ContentPage
    {
        public CrearAuto()
        {
            InitializeComponent();

            BindingContext = new VehiclesViewModel(this);

            //if (Application.Current.Properties.ContainsKey("Autos"))
            //{
            //    imgFotoAuto.Source = Application.Current.Properties["Autos"].ToString();
            //}
        }

        //private async void btnFotoAutos_Clicked(object sender, EventArgs e)
        //{
        //    if (!CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        await DisplayAlert("Error", "No se soporta al cargar fotos", "OK");
        //        return;
        //    }

        //    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
        //    {
        //        Directory = "Sample",
        //        Name = "MiAuto.jpg"
        //    });

        //    if (file == null)
        //    {
        //        return;
        //    }

        //    imgFotoAuto.Source = file.Path;
        //    Console.WriteLine(file.Path);
        //    Application.Current.Properties["Autos"] = file.Path;
        //}

        //private async void btnGaleriaAutos_Clicked(object sender, EventArgs e)
        //{
        //    if (!CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        await DisplayAlert("Error", "No se soporta al cargar fotos", "OK");
        //        return;
        //    }

        //    var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
        //    {
        //        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
        //    }); ;

        //    if (file == null)
        //    {
        //        return;
        //    }

        //    imgFotoAuto.Source = file.Path;
        //    Console.WriteLine(file.Path);
        //    Application.Current.Properties["Autos"] = file.Path;
        //}
    }
}