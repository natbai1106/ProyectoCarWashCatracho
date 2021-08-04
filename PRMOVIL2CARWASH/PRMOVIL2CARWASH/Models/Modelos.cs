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
        HttpClient client;

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
        public List<Modelos> GetModelos()
        {
            var vModelos = new List<Modelos>()
            {
                new Modelos (){ IdModelo = 1, NombreModelo = "Honda Civic"},
                new Modelos (){ IdModelo = 2, NombreModelo = "Toyota Hilux"},
                new Modelos (){ IdModelo = 3, NombreModelo = "Mazda 6"}
            };
            return vModelos;
        }

        public Modelos()
        {
            client = new HttpClient();
        }
        public const string Url = "http://173.249.21.6/v1/vehicle/model/1";
        public async Task<ObservableCollection<Modelos>> ObtenerModelos()
        {
            try
            {
                var response = client.GetStringAsync(Url).Result;
                Console.WriteLine("VALOR DE LA VARIABLE RESPONSE" + response);
                ObservableCollection<Modelos> taskresult = JsonConvert.DeserializeObject<ObservableCollection<Modelos>>(response);
                Console.WriteLine("VALOR DE LA VARIABLE TASRESULT" + taskresult);
                return taskresult;
            }
            catch (Exception e)
            {
                throw;
                Debug.WriteLine("Error", "Error " + e.Message, "Ok");
            }



        }

    }
}
