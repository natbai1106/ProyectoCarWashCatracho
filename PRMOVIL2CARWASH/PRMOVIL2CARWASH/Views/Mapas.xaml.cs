using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using Plugin.Geolocator;

namespace PRMOVIL2CARWASH.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapas : ContentPage
    {
        public Mapas()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //var localizacion = CrossGeolocator.Current;
            //if (localizacion != null)
            //{
            //    localizacion.PositionChanged += Localizacion_PositionChanged;
            //    await localizacion.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
            //    var posicion = await localizacion.GetPositionAsync();
            //    var centromapa = new Position(posicion.Latitude, posicion.Longitude);
            //    mapas.MoveToRegion(new MapSpan(centromapa, 1, 1));
            //}
            //ver la localizacion
            var location = CrossGeolocator.Current;

            if (location.IsGeolocationEnabled)//VALIDA QUE EL GPS ESTA ENCENDIDO
            {
                //toma los datos de la posicion
                var position = await location.GetPositionAsync();
                var ubicacion = new Position(position.Latitude, position.Longitude);
                mapas.MoveToRegion(MapSpan.FromCenterAndRadius(ubicacion, Distance.FromKilometers(1)));
            }

            else
            {
                await DisplayAlert("Gps Desactivado", "Por favor, Active su GPS/Ubicacion", "OK");
            }
        }
        private void Localizacion_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var centromapa = new Position(e.Position.Latitude, e.Position.Longitude);
            //mapas.MoveToRegion(MapSpan.FromCenterAndRadius(centromapa, Distance.FromKilometers(1)));
            mapas.MoveToRegion(new MapSpan(centromapa, 1, 1));
        }
    }
}