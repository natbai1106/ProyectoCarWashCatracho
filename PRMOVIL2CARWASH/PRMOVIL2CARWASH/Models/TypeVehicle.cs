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
    public class TypeVehicle
    {
        HttpClient clientTipoVehiculo;

        [JsonProperty("idTipoVehiculos")]
        public int IdTipoVehicle
        {
            get; set;
        }
        [JsonProperty("tipo_vehiculo")]
        public string NombreTipoVehicle
        {
            get; set;
        }

        public TypeVehicle()
        {
            clientTipoVehiculo = new HttpClient();
        }

        //public string Url = Constanst.GetUrl("/vehicle/brand");
        public async Task<ObservableCollection<TypeVehicle>> ObtenerTipoVehiculo()
        {
            try
            {
                var responseTipoVehiculo = await clientTipoVehiculo.GetStringAsync(Constanst.GetUrl("/vehicle/brand"));
                ObservableCollection<TypeVehicle> taskTipoVehiculo = JsonConvert.DeserializeObject<ObservableCollection<TypeVehicle>>(responseTipoVehiculo);
                return taskTipoVehiculo;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
