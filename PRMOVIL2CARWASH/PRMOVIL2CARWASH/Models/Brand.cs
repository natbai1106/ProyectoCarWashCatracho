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
    public class Brand
    {
        HttpClient clientMarca;

        [JsonProperty("marca_vehiculo")]
        public IList<MarcaVehiculo> MarcaVehiculo { get; set; }

        [JsonProperty("respuesta")]
        public Response Respuesta { get; set; }

        public Brand()
        {
            clientMarca = new HttpClient();
        }

        public async Task<ObservableCollection<Brand>> ObtenerMarcas()
        {
            try
            {
                var responseMarca = await clientMarca.GetStringAsync(Constanst.GetUrl("/vehicle/brand"));
                ObservableCollection<Brand> taskMarca = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(responseMarca);
                return taskMarca;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
    public class MarcaVehiculo
    {

        [JsonProperty("idMarcaVehiculos")]
        public int IdMarcaVehiculos { get; set; }

        [JsonProperty("marca")]
        public string Marca { get; set; }
    }
}
