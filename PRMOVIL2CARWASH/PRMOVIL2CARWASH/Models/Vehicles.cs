using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PRMOVIL2CARWASH.Utils;

namespace PRMOVIL2CARWASH.Models
{
    public class Vehicles
    {
        [JsonProperty("idVehiculos")]
        public int IdVehiculo { get; set; }

        [JsonProperty("numeroPlaca")]
        public string NumeroPlaca { get; set; }

        [JsonProperty("anio")]
        public int Anio { get; set; }

        [JsonProperty("fotoRuta")]
        public byte[] RutaFoto { get; set; }

        [JsonProperty("observacion")]
        public string Observacion { get; set; }

        [JsonProperty("idMarcaVehiculos")]
        public int IdMarcaVehiculos { get; set; }

        [JsonProperty("idUsuario")]
        public int IdUsuario { get; set; }

        [JsonProperty("idModeloVehiculos")]
        public int IdModeloVehiculos { get; set; }

        [JsonProperty("idTipoCombustible")]
        public int IdTipoCombustible { get; set; }

        HttpClient cliente;
        HttpResponseMessage requestMessage;
        string url = Constanst.GetUrl("/vehicles/add");

        public Vehicles()
        {
            cliente = new HttpClient();
        }
        public int RegisterVehicles()
        {
            var m = JsonConvert.SerializeObject(this);
            return 0;
        }
    }
}
