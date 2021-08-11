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
    public class BrandService
    {
        
        string url = Constanst.GetUrl("/vehicle");
        public async Task<IList<MarcaVehiculo>> ObtenerMarcas()
        {
            url += "/brand";
            try
            {
                var client = new HttpClient();
                var json = await client.GetStringAsync(url);
                var response = JsonConvert.DeserializeObject<Brand>(json);
                IList<MarcaVehiculo> Marca = response.MarcaVehiculo;

                return Marca;
            }
            catch (Exception ex)
            {
                //Handle exception here
            }
            return null;
        }
    }
}

