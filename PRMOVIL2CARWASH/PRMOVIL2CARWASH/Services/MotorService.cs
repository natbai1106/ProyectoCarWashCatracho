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
    class MotorService
    {
        string url = Constanst.GetUrl("/vehicle");
        public async Task<IList<Combustible>> ObtenerGas()
        {
            url += "/gas";
            try
            {
                var client = new HttpClient();
                var json = await client.GetStringAsync(url);
                var response = JsonConvert.DeserializeObject<MotorType>(json);
                IList<Combustible> MotorGas = response.Combustible;

                return MotorGas;
            }
            catch (Exception ex)
            {
                //Handle exception here
            }
            return null;
        }
    }
}
