using Newtonsoft.Json;
using PRMOVIL2CARWASH.Models;
using PRMOVIL2CARWASH.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PRMOVIL2CARWASH.Services
{
    class ModelService
    {

        string url = Constanst.GetUrl("/vehicle");
        public async Task<IList<ModeloVehiculo>> ObtenerModelos(int idModel)
        {
            url += "/model/"+ idModel;
            try
            {
                var client = new HttpClient();
                var json = await client.GetStringAsync(url);
                var response = JsonConvert.DeserializeObject<Modelos>(json);
                IList<ModeloVehiculo> Modelo = response.ModeloVehiculo;

                return Modelo;
            }
            catch (Exception ex)
            {
                //Handle exception here
            }
            return null;
        }
    }
}
