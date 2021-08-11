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
    public class ResponseVehicles
    {
        
        [JsonProperty("vehiculos")]
        public IList<Vehiculo> Vehiculos { get; set; }

        [JsonProperty("respuesta")]
        public Response Respuesta { get; set; }


    }

    public class Vehiculo
    {

        [JsonProperty("idVehiculos")]
        public int IdVehiculos { get; set; }

        [JsonProperty("numeroPlaca")]
        public string NumeroPlaca { get; set; }

        [JsonProperty("anio")]
        public int Anio { get; set; }

        [JsonProperty("fotoRuta")]
        public string FotoRuta { get; set; }

        [JsonProperty("observacion")]
        public string Observacion { get; set; }

        [JsonProperty("marca")]
        public string Marca { get; set; }

        [JsonProperty("modelo")]
        public string Modelo { get; set; }

        [JsonProperty("idTipoVehiculos")]
        public int IdTipoVehiculos { get; set; }

        [JsonProperty("nombreVehiculo")]
        public string NombreVehiculo { get; set; }

        [JsonProperty("tipoCombustible")]


        public string TipoCombustible { get; set; }
    }
}
