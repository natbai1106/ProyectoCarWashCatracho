using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PRMOVIL2CARWASH.Utils;
using System.Diagnostics;

namespace PRMOVIL2CARWASH.Models
{
    public class Vehicles
    {
        [JsonProperty("vehiculos")]
        public IList<Vehiculo> Vehiculos { get; set; }

        [JsonProperty("respuesta")]
        public Response Response { get; set; }

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
