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

        [JsonProperty("idModeloVehiculos")]
        public int IdModelo
        {
            get; set;
        }

        [JsonProperty("modelo")]
        public string NombreModelo
        {
            get; set;
        }

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
}
