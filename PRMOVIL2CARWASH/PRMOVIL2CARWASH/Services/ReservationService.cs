using Acr.UserDialogs;
using MonkeyCache.FileStore;
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
    public class ReservationService
    {
        string url = Constanst.GetUrl("/vehicle");
        public async Task<IList<Vehiculo>> GetResponseVehiclessAsync(bool IsConnected,int idUser)
        {
            url += "/all/"+idUser;
            try
            {
                if (IsConnected &&   !Barrel.Current.IsExpired(key: url))
                {
                    await Task.Yield();
                    UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(5));
                    return Barrel.Current.Get<IList<Vehiculo>>(key: url);
                }

                var client = new HttpClient();
                var json = await client.GetStringAsync(url);
                var response = JsonConvert.DeserializeObject<ResponseVehicles>(json);
                IList<Vehiculo> vehicles = response.Vehiculos;
                //Saves the cache   pass it a timespan for expiration
                Barrel.Current.Add(key: url, data: vehicles, expireIn: TimeSpan.FromDays(1));

                return vehicles;
            }
            catch (Exception ex)
            {
                //Handle exception here
            }
            return null;
        }
    }
}
