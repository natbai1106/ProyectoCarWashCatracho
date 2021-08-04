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
        public int IdVehiculos { get; set; }

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
        string url = Constanst.GetUrl("/vehicle");

        public Vehicles()
        {
            cliente = new HttpClient();
        }
        public async Task<int> RegisterVehicle()
        {
            var data = JsonConvert.SerializeObject(this);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            requestMessage = await cliente.PostAsync(string.Concat(url, "/add"), content);
            if (requestMessage.IsSuccessStatusCode)
            {
                var respuesta = JsonConvert.DeserializeObject<Response>(requestMessage.Content.ToString());
                if (respuesta.Status.Equals("ok"))
                {
                    return Constanst.REQUEST_OK;
                }
                else
                    return Constanst.REQUEST_ERROR;
            }
            else
                return Constanst.REQUEST_ERROR;
        }
    }
}
