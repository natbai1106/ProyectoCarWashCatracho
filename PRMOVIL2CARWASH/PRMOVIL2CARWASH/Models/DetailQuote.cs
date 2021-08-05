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
        //idDetalleCotizacion 
        //cantidad
        //idCotizaciones
        //idServicios
        //idVehiculo
        //idProductos

        [JsonProperty("idDetalleCotizacion")]
        public int IdDetalleCotizacion { get; set; }

        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }

        [JsonProperty("idCotizaciones")]
        public int IdCotizaciones { get; set; }

        [JsonProperty("idServicios")]
        public int IdServicios{ get; set; }

        [JsonProperty("idVehiculo")]
        public int IdVehiculo { get; set; }

        [JsonProperty("idProductos")]
        public int IdProductos { get; set; }

        HttpClient cliente;
        HttpResponseMessage requestMessage;
        string url = Constanst.GetUrl("/services");

        public DetailQuote()
        {
            cliente = new HttpClient();
        }


    }
}
