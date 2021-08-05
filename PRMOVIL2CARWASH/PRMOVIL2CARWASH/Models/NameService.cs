using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PRMOVIL2CARWASH.Models
{
    public class NameService
    {
        HttpClient ApiServicio;

        [JsonProperty("idPrecios")]
        public int IdServicios {  get; set;  }

        [JsonProperty("precio")]
        public double precio { get; set; }

        [JsonProperty("nombre_servicio")]
        public string NombreServicio{  get; set; }

        [JsonProperty("tipo_vehiculo")]
        public string TipoVehiculo { get; set; }

        //public ObservableCollection<Service> GetNameService()
        //{
        //    var vNombre = new ObservableCollection<Service>()
        //    {
        //        new Service (){ IdServicios = 1, NombreServicio = "Lavado Genral Prueba"},
        //        new Service (){ IdServicios = 2, NombreServicio = "Cambio de aceite"},
        //        new Service (){ IdServicios = 3, NombreServicio = "Lavado Completo"}
        //    };
        //    return vNombre;
        //}

        public NameService()
        {
            ApiServicio = new HttpClient();
        }


        public const string Url = "http://173.249.21.6/v1/price/all/1";
        public async Task<ObservableCollection<Service>> ObtenerServicios()
        {

            try
            {
                var responseMarca = ApiServicio.GetStringAsync(Url).Result;
                Console.WriteLine("VALOR DE LA VARIABLE RESPONSE" + responseMarca);
                ObservableCollection<Service> taskService = JsonConvert.DeserializeObject<ObservableCollection<Service>>(responseMarca);
                Console.WriteLine("VALOR DE LA VARIABLE TASRESULT" + taskService);
                return taskService;
            }
            catch (Exception e)
            {
                throw;
                Debug.WriteLine("Error", "Error " + e.Message, "Ok");
            }

        }

    }
}
