using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PRMOVIL2CARWASH.Utils;
using System.Net.Http;

namespace PRMOVIL2CARWASH.Models
{
    public class DetailQuote
    {

        [JsonProperty("DetallesCotizacion")]
        public IList<DetallesCotizacion> DetallesCotizacion { get; set; }

        HttpClient cliente;
        HttpResponseMessage requestMessage;
        string url = Constanst.GetUrl("/services");

        public DetailQuote()
        {
            cliente = new HttpClient();
        }

    }

    public class DetallesCotizacion
    {

        [JsonProperty("idDetalleCotizacionions")]
        public int IdDetalleCotizacionions { get; set; }

        [JsonProperty("idCotizaciones")]
        public int IdCotizaciones { get; set; }

        [JsonProperty("idServicios")]
        public string IdServicios { get; set; }
    }
}
