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
    public class Modelos
    {
        HttpClient clientModelos;

        [JsonProperty("modelo_vehiculo")]
        public IList<ModeloVehiculo> ModeloVehiculo { get; set; }

        [JsonProperty("respuesta")]
        public Response Response { get; set; }

        public Modelos()
        {
            clientModelos = new HttpClient();
        }

        public async Task<ObservableCollection<Modelos>> ObtenerModelos()
        {
            try
            {
                var responseModelos = await clientModelos.GetStringAsync(Constanst.GetUrl("/vehicle/model/1"));
                ObservableCollection<Modelos> taskModelos = JsonConvert.DeserializeObject<ObservableCollection<Modelos>>(responseModelos);
                return taskModelos;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

    public class ModeloVehiculo
    {

        [JsonProperty("idModeloVehiculos")]
        public int IdModeloVehiculos { get; set; }

        [JsonProperty("modelo")]
        public string Modelo { get; set; }

        [JsonProperty("idTipoVehiculos")]
        public int IdTipoVehiculos { get; set; }
    }
}
