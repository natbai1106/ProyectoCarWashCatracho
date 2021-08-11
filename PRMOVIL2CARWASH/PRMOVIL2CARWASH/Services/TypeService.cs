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
    class TypeService
    {

        string url = Constanst.GetUrl("/vehicle");
        public async Task<IList<TipoVehiculo>> ObtenerTipos(int idTVehiculo)
        {
            url += "/type/" + idTVehiculo;
            try
            {
                var client = new HttpClient();
                var json = await client.GetStringAsync(url);
                var response = JsonConvert.DeserializeObject<TypeVehicle>(json);
                IList<TipoVehiculo> TipoAuto = response.TipoVehiculo;

                return TipoAuto;
            }
            catch (Exception ex)
            {
                //Handle exception here
            }
            return null;
        }
    }
}
