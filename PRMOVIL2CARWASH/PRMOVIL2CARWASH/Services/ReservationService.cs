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
       public string MessageError { get; set; }
        HttpClient client;

        public ReservationService()
        {
           client   = new HttpClient();
        }
        string url = Constanst.GetUrl("/vehicle");
        public async Task<IList<Vehiculo>> GetResponseVehiclessAsync(bool IsConnected,int idUser)
        {
            url += "/all/"+idUser;
            try
            {
                if (IsConnected &&   !Barrel.Current.IsExpired(key: url))
                {
                    await Task.Yield();
                    return Barrel.Current.Get<IList<Vehiculo>>(key: url);
                }

                
                var json = await client.GetStringAsync(url);
                var response = JsonConvert.DeserializeObject<ResponseVehicles>(json);
                IList<Vehiculo> vehicles = response.Vehiculos;
                //Saves the cache   pass it a timespan for expiration
                Barrel.Current.Add(key: url, data: vehicles, expireIn: TimeSpan.FromDays(1));

                return vehicles;
            }
            catch (Exception ex)
            {
                MessageError = ex.Message; 
            }
            return null;
        }

        public async Task<IList<Servicio>> GetServicesAsync(bool IsConnected, int idVehiculo,int disponible)
        {
            url = Constanst.GetUrl("/price/all?idTipoVehiculos="+idVehiculo+"&disponible_domicilio="+disponible);
           
            try
            {
                if (IsConnected && !Barrel.Current.IsExpired(key: url))
                {
                    await Task.Yield();
                    return Barrel.Current.Get<IList<Servicio>>(key: url);
                }


                var json = await client.GetStringAsync(url);

                var response = JsonConvert.DeserializeObject<Service>(json);
                if(response.Servicios!=null)
                {
                    IList<Servicio> servicios = response.Servicios;
                    //Saves the cache   pass it a timespan for expiration
                    Barrel.Current.Add(key: url, data: servicios, expireIn: TimeSpan.FromDays(1));

                    return servicios;
                }
                    
            }
            catch (Exception ex)
            {
                MessageError = ex.Message;
            }
            return null;
        }
    }
}
