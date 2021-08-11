using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PRMOVIL2CARWASH.Utils;

namespace PRMOVIL2CARWASH.Models
{
    public class Service
    {
        [JsonProperty("servicios")]
        public IList<Servicio> Servicios { get; set; }

        [JsonProperty("respuesta")]
        public Response Respuesta { get; set; }

        HttpClient cliente;
        HttpResponseMessage requestMessage;
        string url = Constanst.GetUrl("/services");

        public Service()
        {
            cliente = new HttpClient();
        }
    }

    public class Servicio
    {

        [JsonProperty("idPrecios")]
        public int IdPrecios { get; set; }

        [JsonProperty("idServicios")]
        public int IdServicios { get; set; }

        [JsonProperty("precio")]
        public int Precio { get; set; }

        [JsonProperty("nombre_servicio")]
        public string NombreServicio { get; set; }

        [JsonProperty("tipo_vehiculo")]
        public string TipoVehiculo { get; set; }
    }

}
