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
    public class RegisterVehicles
    {
        HttpClient APIVehiculo;

        [JsonProperty("idVehiculos")]
        public int IdVehiculos { get; set; }

        [JsonProperty("numeroPlaca")]
        public string NumeroPlaca { get; set; }

        [JsonProperty("anio")]
        public int Anio { get; set; }

        [JsonProperty("fotoRuta")]
        public string RutaFoto { get; set; }

        [JsonProperty("observacion")]
        public string Observacion { get; set; }

        [JsonProperty("marca")]
        public string MarcaVehiculos { get; set; }

        //[JsonProperty("idUsuario")]
        //public int IdUsuario { get; set; }

        [JsonProperty("modelo")]
        public string ModeloVehiculos { get; set; }

        [JsonProperty("tipoCombustible")]
        public string TipoCombustible { get; set; }

        public RegisterVehicles()
        {
            APIVehiculo = new HttpClient();
        }



        //public const string Url = "http://173.249.21.6/v1/vehicle/all/1";
        //public async Task<ObservableCollection<RegisterVehicles>> GetVehicles()
        //{
        //    try
        //    {                
        //        var responseVehiculo = await APIVehiculo.GetStringAsync(Constanst.GetUrl("/vehicle/all/1"));
        //        ObservableCollection<RegisterVehicles> taskVehicle = JsonConvert.DeserializeObject<ObservableCollection<RegisterVehicles>>(responseVehiculo);
        //        return taskVehicle;
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}
    }
}
