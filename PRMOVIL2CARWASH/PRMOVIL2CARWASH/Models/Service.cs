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
        [JsonProperty("idServicios")]
        //Propiedad de la Clase lo del public
        public int IdServicios { get; set; }

        [JsonProperty("nombre_servicio")]
        public string NombreServicio { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("disponible_domicilio")]
        public int DisponibleDomicilio { get; set; }

        HttpClient cliente;
        HttpResponseMessage requestMessage;
        string url = Constanst.GetUrl("/services");

        public Service()
        {
            cliente = new HttpClient();
        }

    }
}
