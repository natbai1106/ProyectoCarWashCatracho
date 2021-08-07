using Newtonsoft.Json;
using PRMOVIL2CARWASH.Utils;
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

        public NameService()
        {
            ApiServicio = new HttpClient();
        }

        public async Task<ObservableCollection<NameService>> ObtenerServicio()
        {
            try
            {
                var responseService = await ApiServicio.GetAsync(Constanst.GetUrl("/price/all/1"));
                // ObservableCollection<NameService> taskService = JsonConvert.DeserializeObject<ObservableCollection<NameService>>(responseService);
                //return taskService;
                return new ObservableCollection<NameService>();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
