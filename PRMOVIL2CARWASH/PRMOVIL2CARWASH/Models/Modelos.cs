using Newtonsoft.Json;
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
        //public List<Modelos> GetModelos()
        //{
        //    var vModelos = new List<Modelos>()
        //    {
        //        new Modelos (){ IdModelo = 1, NombreModelo = "Honda Civic"},
        //        new Modelos (){ IdModelo = 2, NombreModelo = "Toyota Hilux"},
        //        new Modelos (){ IdModelo = 3, NombreModelo = "Mazda 6"}
        //    };
        //    return vModelos;
        //}

        public Modelos()
        {
            clientModelos = new HttpClient();
        }

        public const string Url = "http://173.249.21.6/v1/vehicle/model/1";
        public async Task<ObservableCollection<Modelos>> ObtenerModelos()
        {
            try
            {
                var responseModelos =  clientModelos.GetStringAsync(Url).Result;
                
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
